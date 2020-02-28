using EMartV2.BuisnessLayer.Interfaces;
using EMartV2.BuisnessLayer.Services;
using EMartV2.DataLayer.Interfaces;
using EMartV2.Models.ProductModels;
using NSubstitute;
using Xunit;

namespace EMartv2.Tests
{
    public class ProductTests
    {
        private readonly IProductService _service;
        private readonly IProductRepository _repository = Substitute.For<IProductRepository>();
        public ProductTests()
        {
            _service = new ProductService(_repository);
        }

        [Fact]
        public async void CreateProductAsync_ShouldReturnProduct_WhenDataIsValid()
        {
            // Arrange
            var productName = "Ps4";
            var productTest = new Product { Name = productName };
            _repository.CreateProductAsync(Arg.Any<Product>()).Returns(productTest);
            // Act 
            var product = await _service.CreateProductAsync(productTest);
            // Assert
            Assert.IsType<Product>(product);
            Assert.Equal("Ps4", product.Name);
            Assert.Equal(Arg.Any<int>(), product.Id);
        }

        [Fact]
        public async void GetProductById_ShouldReturnProduct_WhenDataIsValid()
        {
            // Arrange
            var productId = 1;
            var productName = "Computer";
            var productTest = new Product { Id = productId, Name = productName };
            _repository.GetProductByIdAsync(productId).Returns(productTest);
            // Act 
            var product = await _service.GetProductByIdAsync(productId);
            // Assert
            Assert.Equal(productId, product.Id);
            Assert.Equal(productName, product.Name);
        }
    }
}
