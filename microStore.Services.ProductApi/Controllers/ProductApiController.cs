using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using microStore.Services.ProductApi.Models.DTO;
using microStore.Services.ProductApi.Data;
using microStore.Services.ProductApi.Models;
using microStore.Services.ProductApi.Models.DTO;
using System.Linq;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Net.Mime.MediaTypeNames;
using microStore.Services.ProductApi.Service.IService;


namespace microStore.Services.ProductApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "ADMIN")]
    public class ProductApiController : ControllerBase
    {
        private readonly AppDbContext _db;
        private ResponseDTO _response;
        private ResponseResultsDTO _results;
        private readonly IMapper _mapper;
        private IInventoryService _inventoryService;
        private ICommentService _commentService;
        public ProductApiController(AppDbContext db, IMapper mapper, IInventoryService inventoryService, ICommentService commentService)
        {
            _db = db;
            _response = new ResponseDTO();
            _results = new ResponseResultsDTO();
            _mapper = mapper;
            _inventoryService = inventoryService;
            _commentService = commentService;
        }

        [HttpGet]
        [Route("{page:int}/{size:int}")]
        public object Get(int page, int size)
        {
            try
            {
                var count = _db.Products.Count();
                //var productsWithBrands = from product in _db.Products
                //                         join brand in _db.Brands on product.BrandId equals brand.BrandId
                //                         join productCategory in _db.ProductsCategories on product.ProductId equals productCategory.ProductId
                //                         join category in _db.Categories on productCategory.CategoryId equals category.CategoryId


                //                         select new
                //                         {
                //                             ProductId = product.ProductId,
                //                             Name = product.Name,
                //                             Price = product.Price,
                //                             CategoryName = product.CategoryName,
                //                             ImageUrl = product.ImageUrl,
                //                             Description = product.Description,
                //                             Brand = brand,// Include Brand object for full details
                //                             Categories = (from pc in _db.ProductsCategories
                //                                           where pc.ProductId == product.ProductId
                //                                           select pc).ToList() // Include each Category object
                //                         };


                var products = _db.Products

                     .Join(
                    _db.Brands,
                    product => product.BrandId,
                    brand => brand.BrandId,
                    (product, brand) => new
                    {
                        product,
                        brand,
                        categories = product.Categories.Select(c => new
                        {
                            c.CategoryId,
                            c.CategoryName

                        }).ToList(),
                        images = product.Images.Select(c => new
                        {
                            c.ImageId,
                            c.ImageUrl,
                            c.ImageLabel

                        }).ToList()

                    }).OrderByDescending(p => p.product.ProductId)
                    .Skip((page - 1) * size).Take(size)
                     .ToList();

                _response.Data = products; //_mapper.Map<IEnumerable<ProductResponseDTO>>(products).Reverse();
                                           // _response.Data = productsWithBrands;
                _response.Count = count;

            }
            catch (Exception e)
            {

                _response.Success = false;
                _response.Message = e.Message;
            }
            return _response;

        }
        [HttpPost]
        [Route("results")]
        public object ProductResults([FromBody] ProductRequestDTO requestDTO)
        {
            //var orderByLambda = GetOrderByLambda(requestDTO.Order);
            try
            {


                var query = _db.Products
                     .Where(x => x.Name.Contains(requestDTO.Query) || x.Name.StartsWith(requestDTO.Query) || x.Name.EndsWith(requestDTO.Query))
                     //.Include(p => p.Properties)

                     .Join(
                    _db.Brands,
                    product => product.BrandId,
                    brand => brand.BrandId,
                    (product, brand) => new
                    {

                        product,
                        brand,
                        categories = product.Categories.Select(c => new
                        {
                            c.CategoryId,
                            c.CategoryName,
                            c.CategoryLevel,
                            c.CategoryParentId

                        }).ToList(),
                        images = product.Images.Select(c => new
                        {
                            c.ImageUrl,
                            c.ImageLabel

                        }).ToList(),

                        properties = product.Properties.Join(
                             _db.Properties,
                        propertiesValue => propertiesValue.PropertyId,
                         properties => properties.PropertyId,
                         (value, property) => new
                         {
                             value.PropertyValueId,
                             value.PropertyValueName,
                             property.PropertyName,

                         }
                            )

                    });

                //.Skip((requestDTO.Page - 1) * requestDTO.Size)
                //.Take(requestDTO.Size).OrderBy(o => o.Name)
                //.OrderBy(o => o.Price)

                if (requestDTO.Filters.Any())
                {
                    foreach (var item in requestDTO.Filters)
                    {
                        if (item.type == "brand")
                        {
                            query = query.Where(c => c.brand.BrandId == item.id);
                        }
                        if (item.type == "property")
                        {
                            query = query.Where(p => p.properties.Any(prop => item.id == prop.PropertyValueId));
                        }
                    }
                }

                if (requestDTO.Order.Field.Equals("name"))
                {

                    if (requestDTO.Order.Direction.ToLower().Equals("asc"))
                    {
                        query = query.OrderBy(o => o.product.Name);
                    }
                    else
                    {
                        query = query.OrderByDescending(o => o.product.Name);
                    }
                }
                if (requestDTO.Order.Field.Equals("price"))
                {
                    if (requestDTO.Order.Direction.ToLower().Equals("asc"))
                    {
                        query = query.OrderBy(o => o.product.Current_price);
                    }
                    else
                    {
                        query = query.OrderByDescending(o => o.product.Current_price);
                    }

                }
                var totalproducts = query.ToList();

                var brands = totalproducts
                      .Select(p => p.brand)
                      .Distinct()
                      .ToList();
                var properties = totalproducts
                     .SelectMany(p => p.properties)
                     .Distinct()
                     .ToList();

                // Extraer las categorías únicas de los productos filtrados
                var categories = totalproducts
                    .SelectMany(p => p.categories)
                    .Distinct()
                    .ToList();
                var count = totalproducts.Count;
                var paginatedProducts = totalproducts
                      .Skip((requestDTO.Page - 1) * requestDTO.Size)
                      .Take(requestDTO.Size)
                      .ToList();

                //_response.Data = _mapper.Map<IEnumerable<ProductDTO>>(Products).Reverse();
                _results.Products = paginatedProducts;
                _results.Count = count;
                _results.Categories = categories;
                _results.Brands = brands;
                _results.Properties = properties;
            }
            catch (Exception e)
            {
                _results.Success = false;
                _results.Message = e.Message;
            }
            return _results;
        }



        [HttpGet]
        [Route("id/{id:int}")]
        public object Get(int id)
        {
            try
            {
                Product Product = _db.Products.First(c => c.ProductId == id);
                _response.Data = _mapper.Map<ProductDTO>(Product);

            }
            catch (Exception e)
            {

                _response.Success = false;
                _response.Message = e.Message;
            }
            return _response;

        }
        [HttpGet]
        [Route("search/{query}")]
        public object Get(string query)
        {
            try
            {
                IEnumerable<Product> Product = _db.Products.Where(x => x.Name.Contains(query) || x.Name.StartsWith(query) || x.Name.EndsWith(query));
                if (Product == null)
                {
                    _response.Success = false;
                    _response.Message = "El producto no existe";
                }
                _response.Data = _mapper.Map<IEnumerable<ProductDTO>>(Product);

            }
            catch (Exception e)
            {

                _response.Success = false;
                _response.Message = e.Message;
            }
            return _response;

        }
        [HttpPost]
        [Route("link")]
        public async Task<ResponseDTO> GetByLink(ProductLinkPage pp)
        {
            try
            {
                dynamic data = new System.Dynamic.ExpandoObject();
                var product = _db.Products.Where(x => x.Link == pp.Link)
                    .Join(
                    _db.Brands,
                    product => product.BrandId,
                    brand => brand.BrandId,
                    (product, brand) => new
                    {

                        product,
                        brand,


                        Images = product.Images.Select(c =>
                        new
                        {
                            c.ImageUrl,
                            c.ImageLabel
                        }).ToList(),

                    }).First();

                var properties = _db.ProductPropertyValues.Where(x => x.ProductId == product.product.ProductId).Select(p =>
                        new
                        {
                            Propertyvalue = p.PropertyValue.PropertyValueName,
                            Properties = _db.Properties.Where(x => x.PropertyId == p.PropertyValue.PropertyId).Select(pt =>
                            new
                            {
                                Propertyname = pt.PropertyName,
                                PropertyType = _db.PropertyTypes.Where(x => x.PropertyTypeId == pt.PropertyTypeId).Select(pp =>
                             new
                             {
                                 pp.PropertyTypeName,
                                 pp.PropertyTypeId,

                             }).First()
                            }).First()

                        });

                var groupedData = properties
    .GroupBy(p => p.Properties.PropertyType.PropertyTypeId)
    .Select(g => new GroupedProperties
    {
        PropertyType = new PropertyTypeDTO
        {
            PropertyTypeName = g.First().Properties.PropertyType.PropertyTypeName,
            PropertyTypeId = g.First().Properties.PropertyType.PropertyTypeId,
        },
        Values = g.Select(p => new Value
        {
            PropertyName = p.Properties.Propertyname,
            PropertyValueName = p.Propertyvalue,
        }).ToList()
    })
    .ToList();
                data.properties = groupedData;

                var inventory = await _inventoryService.GetInventory(product.product.ProductId);
                data.inventory = inventory;

                //var commets = await _commentService.GetCommentsAsync(product.product.ProductId, pp.Page, pp.Size);
                //data.comment = commets;
                data.product = product.product;
                data.brand = product.brand;
                data.images = product.Images;

                if (product == null)
                {
                    _response.Success = false;
                    _response.Message = "El producto no existe";
                }
                _response.Data = data;

            }
            catch (Exception e)
            {

                _response.Success = false;
                _response.Message = e.Message;
            }
            return _response;

        }

        [HttpGet]
        [Route("home-products")]
        public object HomeProducts()
        {
            try
            {
                var product = _db.Products.Join(
                    _db.Brands,
                    product => product.BrandId,
                    brand => brand.BrandId,
                    (product, brand) => new
                    {

                        product,
                        brand,

                        categories = product.Categories.Select(c => new
                        {
                            c.CategoryId,
                            c.CategoryName,
                            c.CategoryLevel,
                            c.CategoryParentId

                        }).ToList(),
                        images = product.Images.Select(c => new
                        {
                            c.ImageUrl,
                            c.ImageLabel

                        }).ToList()


                    }).Take(10).ToList();

                if (product == null)
                {
                    _response.Success = false;
                    _response.Message = "El producto no existe";
                }
                _response.Data = product;

            }
            catch (Exception e)
            {

                _response.Success = false;
                _response.Message = e.Message;
            }
            return _response;
        }

        [HttpPost]
        //[Authorize(Roles = "ADMIN")]
        public async Task<object> Post([FromBody] ProductDTO productDTO)
        {
            using (var transaction = _db.Database.BeginTransaction())
            {
                try
                {
                    var categoryIds = productDTO.Categories.Select(c => c.CategoryId);
                    IEnumerable<Category> categories = _db.Categories
                                           .Where(c => categoryIds.Contains(c.CategoryId))
                                           .ToList();

                    Product product = _mapper.Map<Product>(productDTO);
                    product.Categories = categories;
                    product.Link = product.Name.ToLower().Replace(" ", "-");
                    _db.Products.Add(product);

                    await _db.SaveChangesAsync();
                    foreach (var image in productDTO.Images)
                    {
                        ProductImages img = new()
                        {
                            ImageId = 0,
                            ImageLabel = image.ImageLabel,
                            ImageUrl = image.ImageUrl,

                        };
                        await _db.ProductImages.AddAsync(img);
                        await _db.SaveChangesAsync();

                    }
                    transaction.Commit();

                    _response.Data = product;
                    _response.Message = "producto creado";

                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    _response.Success = false;
                    _response.Message = e.Message;
                }
            }
            return _response;

        }
        [HttpPost("ProductList")]
        public async Task<object> getProductsByIds([FromBody] ProductIdsRequest ids)
        {
            try
            {

                List<Product> products = _db.Products
                                      .Where(p => ids.ProductIds.Contains(p.ProductId))
                                      .ToList();

                _response.Data = products;
                _response.Message = "";

            }
            catch (Exception e)
            {

                _response.Success = false;
                _response.Message = e.Message;
            }
            return _response;

        }
        //[Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPut]
        //[Authorize(Roles = "ADMIN")]
        public async Task<object> Put([FromBody] ProductDTO productDTO)
        {
            try
            {

                //var categoryIds = productDTO.Categories.Select(c => c.CategoryId);
                //IEnumerable<Category> categories = [.. _db.Categories.Where(c => categoryIds.Contains(c.CategoryId))];

                var productDb = await _db.Products.FindAsync(productDTO.Product.ProductId);

                if (productDb != null)
                {
                    productDb.Name = productDTO.Product.Name;
                    productDb.Link = productDTO.Product.Link;
                    productDb.Description = productDTO.Product.Description;
                    productDb.Current_price = productDTO.Product.Current_price;
                    productDb.Previous_price = productDTO.Product.Previous_price;
                    productDb.BrandId = productDTO.Brand.BrandId;
                    //productDb.Categories = categories;

                    _db.Entry(productDb).State = EntityState.Modified;
                    await _db.SaveChangesAsync();

                    _response.Message = "producto modificado con exito";
                    _response.Data = productDb;
                }
                //Product product = new()
                //{
                //    ProductId = productDTO.Product.ProductId,
                //    Name = productDTO.Product.Name,
                //    Link = productDTO.Product.Link,
                //    Description = productDTO.Product.Description,
                //    Current_price = productDTO.Product.Current_price,
                //    Previous_price = productDTO.Product.Previous_price,
                //    BrandId = productDTO.Brand.BrandId,

                //    Categories = categories

                //};

            }
            catch (Exception e)
            {

                _response.Success = false;
                _response.Message = e.Message;
            }
            return _response;

        }
        [HttpDelete]
        [Authorize(Roles = "ADMIN")]
        [Route("delete/{id:int}")]
        public object Delete(int id)
        {
            try
            {
                Product Product = _db.Products.First(c => c.ProductId == id);
                {
                    _response.Success = false;
                    _response.Message = "El cupon no existe";

                }
                _db.Remove(Product);
                _db.SaveChanges();
                if (Product == null)

                    _response.Data = _mapper.Map<ProductDTO>(Product);
                _response.Message = "cupon eliminado con exito";
            }
            catch (Exception e)
            {

                _response.Success = false;
                _response.Message = e.Message;
            }
            return _response;

        }
    }
}
