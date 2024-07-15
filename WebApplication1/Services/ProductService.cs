using AutoMapper;
using WebApplication1.DTOs;
using WebApplication1.Models;
using WebApplication1.Repositories.Interfaces;
using WebApplication1.Services.Interfaces;

namespace WebApplication1.Services
{
    public class ProductService: IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<PagedResponseDto<ProductDto>> GetPagedProductsAsync(int pageNumber, int pageSize)
        {
            var pagedProducts = await _productRepository.GetPagedProductsAsync(pageNumber, pageSize);
            var pagedResponse = new PagedResponseDto<ProductDto>(
                _mapper.Map<IEnumerable<ProductDto>>(pagedProducts.Data),
                pagedProducts.PageNumber,
                pagedProducts.PageSize,
                pagedProducts.TotalRecords
            );

            return pagedResponse;
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDto> CreateProductAsync(CreateProductDto productDto)
        {
            var product = _mapper.Map<Products>(productDto);
            var createdProduct = await _productRepository.AddAsync(product);
            return _mapper.Map<ProductDto>(createdProduct);
        }

        public async Task<ProductDto> UpdateProductAsync(int id, UpdateProductDto productDto)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return null;
            }

            _mapper.Map(productDto, product);
            var updatedProduct = await _productRepository.UpdateAsync(product);
            return _mapper.Map<ProductDto>(updatedProduct);
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            return await _productRepository.DeleteAsync(id);
        }
    }
}
