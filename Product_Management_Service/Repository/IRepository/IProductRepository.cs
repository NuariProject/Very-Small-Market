using Product_Management_Service.Context;
using Product_Management_Service.Models.DTO;
using System.Linq.Expressions;

namespace Product_Management_Service.Repository.IRepository
{
    public interface IProductRepository
    {
        Task<List<ProductDTO>> GetList();
        Task<ProductDTO> GetProduct(Expression<Func<Product, bool>> filter = null);
        Task Create(ProductDTO model);
        Task<ProductDTO> Update(ProductDTO model);
        Task Delete(int id);
    }
}
