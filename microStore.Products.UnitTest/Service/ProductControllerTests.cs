using AutoFixture;
using AutoMapper;
using InventoryServiceClient;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using microStore.Services.ProductApi.Contracts;
using microStore.Services.ProductApi.Service.IService;
using microStore.Services.ProductApi.Specificatios;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace microStore.Products.UnitTest.Service
{
    public class ProductControllerTests : TestBase
    {
        private readonly ILogger<ProductApiController> _logger;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IProductService _productService;
        private readonly IProductRepository _productRepository;
        private readonly DefaultHttpContext _context;
        private ProductApiController _sut;

        public ProductControllerTests()
        {
            _logger = Substitute.For<ILogger<ProductApiController>>();
            _mapper = BuildMapper();
            _productRepository = Substitute.For<IProductRepository>();
            _productService = Substitute.For<IProductService>();
            _sut = new ProductApiController(_productService, _logger, _productRepository, _mapper);
        }

        [Fact]
        public async Task GetProducts_ShouldReturnListOfProductDTOs_WhenProductsExist()
        {
            //Arrange
            Fixture fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var productList = fixture.Create<IEnumerable<Product>>();

            _productRepository
                .ListAllAsync()
                .Returns(productList);

            var _productSpecificationParams = fixture.Create<ProductSpecificationParams>();

            //Act
            var result = (OkObjectResult)await _sut.GetProductFilteredList(_productSpecificationParams);

            //Assert
            result.StatusCode.Should().Be(200);
            result.Value.Should().BeOfType<List<ProductDetailsDTOSpe>>();
        }
        [Fact]
        public async Task GetProductFiltered_ReturnsProductWithInventory_UsingNSubstitute()
        {
            // Arrange
            var mockProductRepository = Substitute.For<IProductRepository>();

            // Configurar un producto simulado
            var product = CreateProductFixture();
            mockProductRepository
                .GetEntityWithSpecification(Arg.Any<ISpecification<Product>>())
                .Returns(product);

            // Configurar una respuesta simulada para el inventario
            var mockInventoryResponse = new ProductAvailability
            {
                IsAvailable = true,
                Stock = 50,
                VendorName = "Vendor2"
            };
            mockProductRepository
                .GetInventoryAvailability(product.Id)
                .Returns(mockInventoryResponse);
            var mapper = BuildMapper();
            var controller = new ProductApiController(_productService, _logger, mockProductRepository, mapper);
            // Act
            var result = await controller.GetProductFiltered(product.Id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedProduct = Assert.IsType<ProductDetailsDTOSpe>(okResult.Value);
            Assert.NotNull(returnedProduct.Inventory);
            Assert.True(returnedProduct.Inventory.IsAvailable);
            Assert.Equal(50, returnedProduct.Inventory.Stock);
        }
        [Fact]
        public async Task CreateProduct_ReturnsProduct_UsingNSubstitute()
        {
            // Arrange
            var sutProductRepository = Substitute.For<IProductRepository>();
            // Simula imágenes como IFormFile
            var file1 = Substitute.For<IFormFile>();
            var file2 = Substitute.For<IFormFile>();

            // Configura la primera imagen
            var fileName1 = "image1.jpg";
            var content1 = "File content 1";
            var stream1 = new MemoryStream(Encoding.UTF8.GetBytes(content1));
            file1.OpenReadStream().Returns(stream1);
            file1.FileName.Returns(fileName1);
            file1.Length.Returns(stream1.Length);

            // Configura la segunda imagen
            var fileName2 = "image2.jpg";
            var content2 = "File content 2";
            var stream2 = new MemoryStream(Encoding.UTF8.GetBytes(content2));
            file2.OpenReadStream().Returns(stream2);
            file2.FileName.Returns(fileName2);
            file2.Length.Returns(stream2.Length);

            // Simula el formulario con data, container y archivos
            var formFiles = new List<IFormFile> { file1, file2 };
            var formCollection = Substitute.For<IFormCollection>();
            formCollection.Files.Returns(new FormFileCollection { file1, file2 });
            formCollection["data"].Returns((Microsoft.Extensions.Primitives.StringValues)"{\"name\":\"string\",\"link\":\"string\",\"description\":\"string\",\"current_price\":2000000,\"previous_price\":1400000,\"isAvailable\":true,\"brandId\":2,\"categoryIds\":[1,2,3],\"images\":[{\"imageLabel\":\"string\",\"imageUrl\":\"string\"}],\"propertyIds\":[1,2,3,4,5,6],\"inventory\":{\"quantity\":100,\"retailPrice\":1000000,\"vendorId\":1}}");
            formCollection["container"].Returns((Microsoft.Extensions.Primitives.StringValues)"myContainer");


            var httpContext = Substitute.For<HttpContext>();
            httpContext.Request.Form.Returns(formCollection);

            // Configurar una respuesta simulada para el inventario

            //sutProductRepository.CreateProduct(formCollection);

            var mapper = BuildMapper();
            var controller = new ProductApiController(_productService, _logger, sutProductRepository, mapper);
            // Act
            var result = await controller.Post(formCollection);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);

        }
    }
}
