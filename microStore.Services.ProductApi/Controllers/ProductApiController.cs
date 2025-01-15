using AutoMapper;
using Google.Protobuf.Collections;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using microStore.Services.ProductApi.Models.DTO;
using microStore.Services.ProductApi.Models;
using microStore.Services.ProductApi.Service.IService;
using InventoryServiceClient;
using microStore.Services.ProductApi.Contracts;
using microStore.Services.ProductApi.Specificatios;


namespace microStore.Services.ProductApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "ADMIN")]
    public class ProductApiController : ControllerBase
    {
        private readonly ILogger<ProductApiController> _logger;
        private IProductService _productService;
        private IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ProductApiController(IProductService productService, ILogger<ProductApiController> logger, IProductRepository productRepository, IMapper mapper)
        {
            _productService = productService;
            _logger = logger;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("getAllPaginatedProducts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromQuery] ProductSpecificationParams productParams)
        {
            var spec = new ProductFilterSpecification(productParams);
            var products = await _productRepository.ListWithSpecificationAsync(spec);
            var productsIds = products.Select(x => x.Id).ToList();
            var productIdsRepeated = new RepeatedField<int>();
            productIdsRepeated.AddRange(productsIds);
            var inventory = await _productRepository.GetProductInventoryList(productIdsRepeated);
            var countSpec = new ProductFilterForCount(productParams);
            var totalItems = await _productRepository.CountAsync(countSpec);
            var res = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDetailsDTOSpe>>(products);
            foreach (var product in res)
            {
                product.Inventory = inventory.ProductAvailabilityWithIdList.FirstOrDefault(i => i.ProductId == product.Id);
            }
            var response = new ResponseDTO()
            {
                Data = res,
                Count = totalItems,
                Message = "Success",
            };
            if (res != null) return Ok(response);
            response.Message = "No Products Found";
            return NotFound(response);

        }
        [HttpGet]
        [Route("filterTest")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProductFilteredList([FromQuery] ProductSpecificationParams productParams)
        {
            var spec = new ProductFilterSpecification(productParams);
            var products = await _productRepository.ListWithSpecificationAsync(spec);
            var res = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDetailsDTOSpe>>(products);

            return Ok(res);
        }

        [HttpGet]
        [Route("product/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProductFiltered(int id)
        {
            var spec = new ProductFilterSpecification(id);
            var product = await _productRepository.GetEntityWithSpecification(spec);
            var inventoryResponse = await _productRepository.GetInventoryAvailability(product.Id);

            var res = _mapper.Map<Product, ProductDetailsDTOSpe>(product);
            res.Inventory = _mapper.Map<ProductAvailabilityWithId>(inventoryResponse);
            return Ok(res);
        }

        [HttpPost]
        [Route("results")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ProductResults([FromBody] ProductSpecificationParams productParams)
        {
            var spec = new ProductFilterSpecification(productParams);
            var countSpec = new ProductFilterForCount(productParams);
            var totalItems = await _productRepository.CountAsync(countSpec);
            var products = await _productRepository.ListWithSpecificationAsync(spec);
            var res = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDetailsDTOSpe>>(products);
            var brands = res
                .Select(p => p.Brand)
                .Distinct()
                .ToList();
            var properties = res
                .SelectMany(p => p.Properties.Select(pr => new PropertiesFilter()
                {
                    Id = pr.Id,
                    PropertyName = pr.property.PropertyName,
                    PropertyValueName = pr.PropertyValueName
                }))
                .Distinct()
                .ToList();
            var categories = res
                .SelectMany(p => p.Categories)
                .Distinct()
                .ToList();
            var data = new ProductListDTO()
            {
                Products = _mapper.Map<List<ProductBasicDTO>>(res),
                Brands = brands,
                Properties = properties,
                Categories = categories,
                Count = totalItems
            };
            return Ok(data);
        }

        [HttpGet]
        [Route("id/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            var res = await _productService.GetProductById(id);
            if (res.Data == null)
            {
                _logger.LogWarning("producto con el id {id} no existe", id);
                return NotFound(res);
            }
            else
            {
                return Ok(res);
            }

        }
        [HttpGet]
        [Route("search/{query}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(string query)
        {
            var res = await _productService.GetProductByName(query);
            if (res.Data == null)
            {
                _logger.LogWarning("{query} no obtuvo resultados", query);
                return NotFound(res);
            }
            else
            {
                return Ok(res);
            }

        }
        [HttpGet]
        [Route("details/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByLink(int id)
        {
            var spec = new ProductFilterSpecification(id);
            var product = await _productRepository.GetEntityWithSpecification(spec);
            var inventoryResponse = await _productRepository.GetInventoryAvailability(product.Id);

            var res = _mapper.Map<Product, ProductDetailsDTOSpe>(product);
            res.Inventory = _mapper.Map<ProductAvailabilityWithId>(inventoryResponse);
            return Ok(res);
        }

        [HttpGet]
        [Route("home-products")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> HomeProducts()
        {
            var spec = new ProductFilterSpecification();
            var products = await _productRepository.SampleEntities(spec);
            var res = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDetailsDTOSpe>>(products);
            return Ok(res);
        }

        [HttpPost]
        //[Authorize(Roles = "ADMIN")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Post([FromForm] IFormCollection form)
        {
            await _productRepository.CreateProduct(form);
            return Ok(form["data"]);
        }
        [HttpPost("ProductList")]
        public async Task<object> getProductsByIds([FromBody] ProductIdsRequest ids)
        {
            var res = await _productService.getProductsByIds(ids);
            if (res.Data == null)
            {
                _logger.LogWarning("hubo un error al encontrar los productos");
                return NotFound(res);
            }
            else
            {
                return Ok(res);
            }

        }
        //[Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPut]
        //[Authorize(Roles = "ADMIN")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put([FromForm] IFormCollection form)
        {
            await _productRepository.UpdateProduct(form);
            return Ok(form);

            //var res = await _productService.UpdateProduct(productDTO);
            //if (res.Data == null)
            //{
            //    _logger.LogWarning("hubo un error al actualizar el producto");
            //    return NotFound(res);
            //}
            //else
            //{
            //    return Ok(res);
            //}

        }
        [HttpDelete]
        [Authorize(Roles = "ADMIN")]
        [Route("delete/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var res = await _productService.DeleteProduct(id);
            if (res.Data == null)
            {
                _logger.LogWarning("producto con el id {id} no existe", id);
                return NotFound(res);
            }
            else
            {
                return Ok(res);
            }
        }
        [HttpGet("inventory/{id}")]
        public async Task<IActionResult> GetProductDetails(int id)
        {
            var res = await _productService.GetProductDetails(id);
            if (res.Data == null)
            {
                _logger.LogWarning("producto con el id {id} no existe", id);
                return NotFound(res);
            }
            else
            {
                return Ok(res);
            }
        }
        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile()
        {
            var formCollection = await Request.ReadFormAsync();
            var res = await _productService.UploadFile(formCollection);

            return Ok(res);
        }
    }
}
