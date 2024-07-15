using WebApplication1.DTOs;
using WebApplication1.Models;

namespace WebApplication1.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Products>> GetAllAsync();
        Task<PagedResponseDto<Products>> GetPagedProductsAsync(int pageNumber, int pageSize);
        Task<Products> GetByIdAsync(int id);
        Task<Products> AddAsync(Products product);
        Task<Products> UpdateAsync(Products product);
        Task<bool> DeleteAsync(int id); 
    }
}
