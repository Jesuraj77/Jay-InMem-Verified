using EMartV2.BuisnessLayer.Interfaces;
using EMartV2.DataLayer.Interfaces;
using EMartV2.Models.ProductModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EMartV2.BuisnessLayer.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            try
            {
                var productDto = new Product { Id = product.Id, Name = product.Name };
                return await _productRepository.CreateProductAsync(productDto);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            try
            {
                return await _productRepository.GetProductByIdAsync(id);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            try
            {
                return await _productRepository.GetAllProductsAsync();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }
}
