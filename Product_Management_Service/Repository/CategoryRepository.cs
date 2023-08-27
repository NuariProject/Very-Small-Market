using Microsoft.EntityFrameworkCore;
using Product_Management_Service.Context;
using Product_Management_Service.Repository.IRepository;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Product_Management_Service.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private vsm_db_productContext _db;
        public CategoryRepository(vsm_db_productContext db)
        {

            _db = db;  

        }
        public async Task Create(Category model)
        {
            try
            {
                model.IsDelete = false;
                await _db.Categories.AddAsync(model);
                await _db.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                var model = await _db.Categories.Where(ss => ss.CategoryId == id && !ss.IsDelete).FirstOrDefaultAsync();

                if (model != null)
                {
                    model.IsDelete = true;
                    _db.Categories.Update(model);

                    await _db.SaveChangesAsync();
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Category> GetCategory(Expression<Func<Category, bool>> filter = null)
        {
            IQueryable<Category> query = _db.Categories;
            try
            {
                if (filter != null)
                {
                    query = query.Where(filter);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<Category>> GetList()
        {
            var value = new List<Category>();
            try
            {
                value = await _db.Categories.Where(ss => ss.IsDelete == false).ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }

            return value;
        }

        public async Task<Category> Update(Category model)
        {
            try
            {
                model.IsDelete = false;
                _db.Categories.Update(model);
                await _db.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
            return model;
        }
    }
}
