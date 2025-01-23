using AutoMapper;
using microStore.Services.ProductApi.Models.DTO;
using microStore.Services.ProductApi.Service.IService;
using microStore.Services.ProductApi.Models;
using microStore.Services.ProductApi.Data;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using InventoryServiceClient;
using Grpc.Core;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using microStore.Services.ProductApi.Controllers;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using microStore.Services.ProductApi.Helpers;
using microStore.Services.ProductApi.Specificatios;

namespace microStore.Services.ProductApi.Service
{
    public class ProductService : IProductService
    {
        private readonly InventoryProto.InventoryProtoClient _inventoryClient;
        private readonly AppDbContext _db;
        private ResponseDTO _response;
        private ResponseResultsDTO _results;
        private readonly IMapper _mapper;
        private IInventoryService _inventoryService;
        private ICommentService _commentService;
        private readonly IConfiguration _config;
        private readonly ILogger<ProductApiController> _logger;

        public ProductService(AppDbContext db, IMapper mapper, IInventoryService inventoryService, ICommentService commentService, InventoryProto.InventoryProtoClient inventoryClient, IConfiguration config, ILogger<ProductApiController> logger)
        {
            _db = db;
            _inventoryClient = inventoryClient;
            _response = new ResponseDTO();
            _results = new ResponseResultsDTO();
            _mapper = mapper;
            _inventoryService = inventoryService;
            _commentService = commentService;
            _config = config;
            _logger = logger;
        }

        public async Task<ResponseDTO> DeleteProduct(int productId)
        {
            try
            {
                Product Product = await _db.Products.FirstAsync(c => c.Id == productId);
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

        public async Task<ResponseDTO> GetRelatedProducts(int categoryId)
        {
            try
            {
                var totalCount = _db.Products.Count();
                var products = await _db.Products.Include(p => p.Images).Where(pro => pro.Categories.Any(cat => cat.Id == categoryId))
                    .Take(5).ToListAsync();
                _response.Data = _mapper.Map<IEnumerable<ProductBasicDTO>>(products);
                return _response;
            }
            catch (Exception e)
            {
               _response.Success = false;
               _response.Message = e.Message;
               return _response;
            }
            
         
        }

        public async Task<ResponseDTO> GetHomeProducts()
        {
            try
            {
                var product = await _db.Products.Join(
                    _db.Brands,
                    product => product.BrandId,
                    brand => brand.Id,
                    (product, brand) => new
                    {

                        product,
                        brand,

                        categories = product.Categories.Select(c => new
                        {
                            c.Id,
                            c.CategoryName,
                            c.CategoryLevel,
                            c.CategoryParentId

                        }).ToList(),
                        images = product.Images.Select(c => new
                        {
                            
                            c.ImageUrl,
                            c.ImageLabel

                        }).ToList()


                    }).Take(10).ToListAsync();

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

        public async Task<ResponseDTO> GetPageableProducts(int page, int size)
        {
            try
            {
                var count = await _db.Products.CountAsync();

                var products = await _db.Products

                     .Join(
                    _db.Brands,
                    product => product.BrandId,
                    brand => brand.Id,
                    (product, brand) => new
                    {
                        product,
                        brand,
                        categories = product.Categories.Select(c => new
                        {
                            c.Id,
                            c.CategoryName

                        }).ToList(),
                        images = product.Images.Select(c => new
                        {
                            c.Id,
                            c.ImageUrl,
                            c.ImageLabel

                        }).ToList()

                    }).OrderByDescending(p => p.product.Id)
                    .Skip((page - 1) * size).Take(size)
                     .ToListAsync();

                _response.Data = products;
                _response.Count = count;

            }
            catch (Exception e)
            {

                _response.Success = false;
                _response.Message = e.Message;
            }
            return _response;
        }

        public async Task<ResponseDTO> GetProductById(int productId)
        {
            try
            {
                Product Product = await _db.Products.FirstAsync(c => c.Id == productId);
                _response.Data = _mapper.Map<ProductDTO>(Product);

            }
            catch (Exception e)
            {

                _response.Success = false;
                _response.Message = e.Message;
            }
            return _response;
        }

        /*public async Task<ResponseDTO> GetProductByLink(string link)
        {
            try
            {
                Console.WriteLine(link);
                dynamic data = new System.Dynamic.ExpandoObject();
                var product = await _db.Products.Where(x => x.Link == link)
                    .Join(
                    _db.Brands,
                    product => product.BrandId,
                    brand => brand.Id,
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

                    }).FirstAsync();

                //            var properties = _db.ProductPropertyValues.Where(x => x.ProductId == product.product.Id).Select(p =>
                //                    new
                //                    {
                //                        Propertyvalue = p.PropertyValue.PropertyValueName,
                //                        Properties = _db.Properties.Where(x => x.Id == p.PropertyValue.PropertyId).Select(pt =>
                //                        new
                //                        {
                //                            Propertyname = pt.PropertyName,
                //                            PropertyType = _db.PropertyTypes.Where(x => x.PropertyTypeId == pt.PropertyTypeId).Select(pp =>
                //                         new
                //                         {
                //                             pp.PropertyTypeName, 
                //                             pp.PropertyTypeId,

                //                         }).First()
                //                        }).First()

                //                    });

                //            var groupedData = await properties
                //.GroupBy(p => p.Properties.PropertyType.PropertyTypeId)
                //.Select(g => new GroupedProperties
                //{
                //    PropertyType = new PropertyTypeDTO
                //    {
                //        PropertyTypeName = g.First().Properties.PropertyType.PropertyTypeName,
                //        PropertyTypeId = g.First().Properties.PropertyType.PropertyTypeId,
                //    },
                //    Values = g.Select(p => new Value
                //    {
                //        PropertyName = p.Properties.Propertyname,
                //        PropertyValueName = p.Propertyvalue,
                //    }).ToList()
                //})
                //.ToListAsync();
                //            data.properties = groupedData;
                var inventoryResponse = await _inventoryClient.CheckAvailabilityAsync(new ProductRequest
                {
                    ProductId = product.product.Id
                });
                //var inventory = await _inventoryService.GetInventory(product.product.ProductId);
                data.inventory = inventoryResponse;

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
        }*/

        public async Task<ResponseDTO> GetProductByName(string search)
        {
            try
            {
                IEnumerable<Product> Product = await _db.Products.Where(x => x.Name.Contains(search) || x.Name.StartsWith(search) || x.Name.EndsWith(search)).ToListAsync();
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

        public async Task<ResponseDTO> getProductsByIds(ProductIdsRequest ids)
        {
            try
            {

                List<Product> products = await _db.Products
                                      .Where(p => ids.ProductIds.Contains(p.Id))
                                      .ToListAsync();

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

        public async Task<ResponseDTO> GetProductDetails(int id)
        {
            var product = await _db.Products.FindAsync(id);
            if (product == null) return null;

            // Paso 2: Consultar la disponibilidad en el servicio Inventory usando gRPC
            var inventoryResponse = await _inventoryClient.CheckAvailabilityAsync(new ProductRequest
            {
                ProductId = id
            });

            // Paso 3: Combinar la información
            var productDetails = new
            {
                Product = product,
                Availability = new
                {
                    IsAvailable = inventoryResponse.IsAvailable,
                    Stock = inventoryResponse.Stock,
                    VendorName = inventoryResponse.VendorName
                }
            };
            _response.Data = productDetails;

            return _response;
        }
        public async Task<ResponseResultsDTO> GetProductsResults(ProductRequestDTO requestDTO)
        {
            try
            {
                var query = _db.Products
                     .Where(x => x.Name.Contains(requestDTO.Query) || x.Name.StartsWith(requestDTO.Query) || x.Name.EndsWith(requestDTO.Query))
                     //.Include(p => p.Properties)

                     .Join(
                    _db.Brands,
                    product => product.BrandId,
                    brand => brand.Id,
                    (product, brand) => new
                    {

                        product,
                        brand,
                        categories = product.Categories.Select(c => new
                        {
                            c.Id,
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
                         properties => properties.Id,
                         (value, property) => new
                         {
                             value.Id,
                             value.PropertyValueName,
                             property.PropertyName,

                         }
                            )

                    });

                if (requestDTO.Filters.Any())
                {
                    foreach (var item in requestDTO.Filters)
                    {
                        if (item.type == "brand")
                        {
                            query = query.Where(c => c.brand.Id == item.id);
                        }
                        if (item.type == "property")
                        {
                            query = query.Where(p => p.properties.Any(prop => item.id == prop.Id));
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
                var totalproducts = await query.ToListAsync();

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
        
       

        public async Task<ResponseDTO> StoreProduct(ProductDTO productDTO)
        {
            using (var transaction = _db.Database.BeginTransaction())
            {
                try
                {
                    var categoryIds = productDTO.Categories.Select(c => c.CategoryId);
                    ICollection<Category> categories = _db.Categories
                                           .Where(c => categoryIds.Contains(c.Id))
                                           .ToList();

                    Product product = _mapper.Map<Product>(productDTO);
                    product.Categories = categories;
                    _db.Products.Add(product);

                    await _db.SaveChangesAsync();
                    foreach (var image in productDTO.Images)
                    {
                        ProductImages img = new()
                        {
                            Id = 0,
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

        public async Task<ResponseDTO> UpdateProduct(ProductDTO productDTO)
        {
            try
            {

                //var categoryIds = productDTO.Categories.Select(c => c.CategoryId);
                //IEnumerable<Category> categories = [.. _db.Categories.Where(c => categoryIds.Contains(c.CategoryId))];

                var productDb = await _db.Products.FindAsync(productDTO.Product.Id);

                if (productDb != null)
                {
                    productDb.Name = productDTO.Product.Name;
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

        public async Task<object> UploadFile(IFormCollection form)
        {
            var azureConnectionString = _config["AzureUploadSettings:AzureStorageConnectionString"];
            var azureContainersAllowed = _config["AzureUploadSettings:AzureContainersAllowed"];
            var permittedFileExtensions = _config["AzureUploadSettings:FileUploadTypesAllowed"];
            var fileSizeLimit = _config.GetValue<long>("AzureUploadSettings:MaxFileUploadSize");

            // check that the Azure Storage connection string is available
            if (string.IsNullOrEmpty(azureConnectionString))
            {
                _logger.LogError("Azure Storage connection string is empty or has not been set in config/env variables");

            }

            // check that the Azure Storage container names is available
            if (string.IsNullOrEmpty(azureContainersAllowed))
            {
                _logger.LogError("Azure Storage container names was empty or has not been set in config/env variables");

            }



            var requestedContainer = form["container"].ToString();

            // check container name isn't empty
            if (requestedContainer.Length == 0)
            {
                _logger.LogError("Azure container name is empty");

            }

            // check requested container name is in an allowed set of names
            if (!azureContainersAllowed.Contains(requestedContainer.ToLowerInvariant()))
            {
                _logger.LogError("Invalid Azure container name supplied: {requestedContainer}", requestedContainer);

            }

            var files = form.Files;
            var urls = new List<Uri>();
            foreach (var file in files)
            {

                var fileExtension = Path.GetExtension(file.FileName.ToLowerInvariant());
                var fileNameLengthLimit = 75;

                List<string> basicFileChecks = BasicFileChecks(file, permittedFileExtensions, fileSizeLimit, fileNameLengthLimit, fileExtension);

                if (basicFileChecks is null)
                {
                    try
                    {
                        var azureContainer = new BlobContainerClient(azureConnectionString, requestedContainer);
                        var createResponse = await azureContainer.CreateIfNotExistsAsync();

                        // in case the container doesn't exist
                        if (createResponse != null && createResponse.GetRawResponse().Status == 201)
                        {
                            await azureContainer.SetAccessPolicyAsync(Azure.Storage.Blobs.Models.PublicAccessType.Blob);
                        }

                        // generate a unique upload file name
                        // [original_filename_without_extension]_[8_random_chars].[original_filename_extension]
                        // eg. filename_xgh38tye.jpg
                        var fileName = HttpUtility.HtmlEncode(Path.GetFileNameWithoutExtension(file.FileName)) +
                            "_" + Path.GetRandomFileName().Substring(0, 8) + Path.GetExtension(file.FileName);

                        var blob = azureContainer.GetBlobClient(fileName);
                        //await blob.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots);

                        // set the content type (which may or may not have been provided by the client)
                        var blobHttpHeader = new BlobHttpHeaders();

                        if (file.ContentType != null)
                        {
                            blobHttpHeader.ContentType = file.ContentType;
                        }
                        else
                        {
                            blobHttpHeader.ContentType = fileExtension switch
                            {
                                ".jpg" => "image/jpeg",
                                ".jpeg" => "image/jpeg",
                                ".png" => "image/png",
                                _ => null
                            };
                        }

                        using (var fileStream = file.OpenReadStream())
                        {
                            await blob.UploadAsync(fileStream, blobHttpHeader);
                            urls.Add(blob.Uri);
                        }
                        _logger.LogError("The file '{fileName}' was uploaded", file.FileName);
                    }

                    catch (Exception ex)
                    {
                        _logger.LogError("The file '{fileName}' could not be uploaded: {message}", file.FileName, ex.Message);
                        throw new AppException($"The file '{file.FileName}' could not be uploaded: {ex.Message}");
                    }
                }
                else
                {
                    _logger.LogError("The file '{fileName}' failed basic file checks and could not be uploaded", file.FileName);

                }
            }
            return urls;
        }
        private List<string> BasicFileChecks(IFormFile file, string permittedFileExtensions, long fileSizeLimit, int fileNameLengthLimit = 75, string fileExtension = "unknown")
        {
            var filecheckErrors = new List<string>();

            // check the file has an extension
            if (string.IsNullOrWhiteSpace(fileExtension))
            {
                _logger.LogError("'{fileName}' does not appear to have a file extension", file.FileName);
                filecheckErrors.Add($"'{file.FileName}' does not appear to have a file extension");
            }

            // check the file type is allowed
            if (!permittedFileExtensions.Contains(fileExtension))
            {
                _logger.LogError("Upload of '{fileName}' with file type '{fileExtension}' was not allowed", file.FileName, fileExtension);
                filecheckErrors.Add($"Upload of '{file.FileName}' with file type '{fileExtension}' is not allowed");
            }

            // check file isn't 0 bytes
            if (file.Length < 1)
            {
                _logger.LogError("The file '{fileName}' had a file size of 0 bytes", file.FileName);
                filecheckErrors.Add($"'{file.FileName}' has a file size of 0 bytes");
            }

            // check the file size (in bytes) isn't above the limit
            if (file.Length > fileSizeLimit)
            {
                _logger.LogError("The size of '{fileName}' ({fileSize} bytes) was larger than the current file size limit ({sizeLimit} bytes)", file.FileName, file.Length, fileSizeLimit);
                filecheckErrors.Add($"The size of '{file.FileName}' ({file.Length} bytes) is larger than the current file size limit");
            }

            // check the file name length isn't above the limit
            if (file.FileName.Length > fileNameLengthLimit)
            {
                _logger.LogError("The name of '{fileName}' was too long at {fileNameLength} characters. The current limit is {fileNameLengthLimit} characters", file.FileName, file.FileName.Length, fileNameLengthLimit);
                filecheckErrors.Add($"The name of '{file.FileName}' was too long: {file.FileName.Length}  characters");
            }

            if (filecheckErrors.Count == 0) return null!;

            return filecheckErrors;
        }


    }

}
