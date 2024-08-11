using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.DTOs;
using WebApplication1.Models;
using WebApplication1.Repositories.Interfaces;

namespace WebApplication1.Repositories
{
    public class ProductRepository: IProductRepository
    {
        private readonly TenantDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductRepository(TenantDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
           
        }

        public async Task<IEnumerable<Products>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<PagedResponseDto<Products>> GetPagedProductsAsync(int pageNumber, int pageSize)
        {
            
            // Retrieve AspUserIdID and AccessType from HttpContext.Items dynamically
            var aspUserIdID = _httpContextAccessor.HttpContext?.Items["AspUserID"]?.ToString() ?? "";
            var accessType = _httpContextAccessor.HttpContext?.Items["AccessType"]?.ToString() ?? "";

            if (string.IsNullOrEmpty(accessType))
            {
                throw new InvalidOperationException("Access type is null or empty");
            }

            // Initial products query with paging
            IQueryable<Products> products = _context.Products;


            switch (accessType)
            {
                case "all-data":
                    // No additional filtering needed
                    break;

                case "my-data":
                    if (string.IsNullOrEmpty(aspUserIdID))
                    {
                        throw new InvalidOperationException("AspUserID is null or empty for 'my-data' access type");
                    }
                    products = products.Where(c => c.CreatedBy == aspUserIdID);
                    break;

                case "no-data":
                    return new PagedResponseDto<Products>(new List<Products>(), pageNumber, pageSize, 0);

                default:
                    throw new InvalidOperationException("Invalid access type");
            }

            var totalRecords = await products.CountAsync();

            var productList = await products.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize).ToListAsync();

            return new PagedResponseDto<Products>(productList, pageNumber, pageSize, totalRecords);
        }

        public async Task<Products> GetByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<Products> AddAsync(Products product)
        {
            product.CreatedAt = DateTime.Now;
            //product.CreatedBy = _httpContextAccessor.HttpContext.Items["AspUserID"].ToString();
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Products> UpdateAsync(Products product)
        {
            product.UpdatedAt = DateTime.Now;
            //product.UpdatedBy = _httpContextAccessor.HttpContext.Items["AspUserID"].ToString();
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return false;
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}