using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Product_Management_Service.Context;
using Product_Management_Service.Models.DTO;
using Product_Management_Service.Repository.IRepository;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.PortableExecutable;

namespace Product_Management_Service.Repository
{
    public class ProductRepository : IProductRepository
    {
        private vsm_db_productContext _db;
        private readonly IMapper _mapper;
        public ProductRepository(vsm_db_productContext db, IMapper mapper)
        {
            _mapper = mapper;
            _db = db;

        }
        public async Task Create(ProductDTO model)
        {
            using (var trx = await _db.Database.BeginTransactionAsync())
            {
                try
                {
                    var product = _mapper.Map<Product>(model);
                    var categories = _mapper.Map<List<Category>>(model.CategoryList);

                    product.CreatedDate = DateTime.Now;
                    product.IsDelete = false;

                    await _db.Products.AddAsync(product);
                    await _db.SaveChangesAsync();

                    var productId = product.ProductId;

                    // insert category list
                    CreateCategory(productId, model.CategoryList);

                    trx.Commit();

                }
                catch (Exception)
                {
                    trx.Rollback();
                    throw;
                }
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                var model = await _db.Products.Where(ss => ss.ProductId == id && !ss.IsDelete).FirstOrDefaultAsync();

                if (model != null)
                {
                    model.ModifiedDate = DateTime.Now;
                    model.IsDelete = true;
                    _db.Products.Update(model);

                    await _db.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ProductDTO> GetProduct(Expression<Func<Product, bool>> filter = null)
        {
            var productList = new ProductDTO();
            var categoryList = new List<CategoryDTO>();
            IQueryable<Product> query = _db.Products;

            try
            {
                if (filter != null)
                {
                    var product = await query.Where(filter).FirstOrDefaultAsync();
                    if (product != null)
                    {
                        var categories = await _db.Categories.ToListAsync();
                        var productsCategories = await _db.ProductCategories.ToListAsync();

                        productList = _mapper.Map<ProductDTO>(product);
                        categoryList = _mapper.Map<List<CategoryDTO>>(categories);

                        productList.CategoryList = (from a in productsCategories.Where(ss => ss.ProductId == productList.ProductId)
                                                     join b in categories on a.CategoryId equals b.CategoryId
                                                     select new CategoryDTO
                                                     {
                                                         CategoryId = a.CategoryId,
                                                         Name = b.Name
                                                     }).ToList();
                    }
                    else
                    {
                        productList = null;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return productList;
        }

        public async Task<List<ProductDTO>> GetList()
        {
            var productList = new List<ProductDTO>();
            var categoryList = new List<CategoryDTO>();
            try
            {
                var product  = await _db.Products.Where(ss => ss.IsDelete == false).ToListAsync();
                var categories = await _db.Categories.ToListAsync();
                var productsCategories = await _db.ProductCategories.ToListAsync();

                productList = _mapper.Map<List<ProductDTO>>(product);
                categoryList = _mapper.Map<List<CategoryDTO>>(categories);

                foreach (var item in productList)
                {
                    item.CategoryList = (from a in productsCategories.Where(ss => ss.ProductId == item.ProductId)
                                         join b in categories on a.CategoryId equals b.CategoryId
                                         select new CategoryDTO
                                         {
                                             CategoryId = a.CategoryId,
                                             Name = b.Name
                                         }).ToList();
                }

            }
            catch (Exception)
            {
                throw;
            }

            return productList;
        }

        public async Task CreateCategory(int productId, List<CategoryDTO> categoryList)
        {
            foreach(var item in categoryList)
                {
                var productCategories = new ProductCategory
                {
                    ProductId = productId,
                    CategoryId = item.CategoryId
                };

                await _db.ProductCategories.AddAsync(productCategories);
            }

        }

        public async Task<ProductDTO> Update(ProductDTO model)
        {
            using (var trx = await _db.Database.BeginTransactionAsync())
            {
                try
                {
                    var product = _db.Products.Where(ss => ss.ProductId == model.ProductId && !ss.IsDelete).FirstOrDefault();
                    //product = _mapper.Map<Product>(model);ry>>(model.CategoryList);

                    product.Name = model.Name;
                    product.Description = model.Description;
                    product.Price = model.Price;
                    product.ModifiedDate = DateTime.Now;
                    product.IsDelete = false;

                    _db.Products.Update(product);
                    await _db.SaveChangesAsync();

                    var getData = await _db.ProductCategories.Where(ss => ss.ProductId == model.ProductId).ToListAsync();
                    _db.ProductCategories.RemoveRange(getData);
                    await _db.SaveChangesAsync();

                    // insert category list
                    await CreateCategory(model.ProductId, model.CategoryList);
                    await _db.SaveChangesAsync();

                    trx.Commit();

                }
                catch (Exception)
                {
                    trx.Rollback();
                    throw;
                }
            }

            return model;
        }
    }
}
