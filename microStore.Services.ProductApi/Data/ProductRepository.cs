using Google;
using InventoryServiceClient;
using MassTransit;
using MassTransit.Futures.Contracts;
using Microsoft.EntityFrameworkCore;
using microStore.Services.ProductApi.Contracts;
using microStore.Services.ProductApi.Helpers;
using microStore.Services.ProductApi.Models;
using microStore.Services.ProductApi.Models.DTO;
using Newtonsoft.Json;
using Polly;
using System;
using static SerilogTimings.Operation;
using static System.Net.Mime.MediaTypeNames;

namespace microStore.Services.ProductApi.Data
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        private readonly AppDbContext _appContext;
        private readonly InventoryProto.InventoryProtoClient _inventoryClient;
        private readonly UploadImages _uploadImages;

        public ProductRepository(AppDbContext appContext, InventoryProto.InventoryProtoClient inventoryClient, UploadImages uploadImages) : base(appContext)
        {
            _appContext = appContext;
            _inventoryClient = inventoryClient;
            _uploadImages = uploadImages;
        }

        public async Task CreateProduct(IFormCollection form)
        {
            var images = await _uploadImages.UploadFile(form);
            var dto = JsonConvert.DeserializeObject<ProductCreateDTO>(form["data"]);
            var product = new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                Link = dto.Link,
                Current_price = dto.Current_price,
                Previous_price = dto.Previous_price,
                BrandId = dto.BrandId,
                Images = images,
                Categories = _appContext.Categories.Where(c => dto.CategoryIds.Contains(c.Id)).ToList(),
                Properties = _appContext.PropertyValues.Where(p => dto.PropertyIds.Contains(p.Id)).ToList(),

            };

            // Agregar el producto a la base de datos
            _appContext.Products.Add(product);


            // Guardar los cambios
            await _appContext.SaveChangesAsync();

            await CreateInventory(dto.Inventory, product.Id);

        }

        public async Task<ProductAvailability> GetInventoryAvailability(int id)
        {
            return await _inventoryClient.CheckAvailabilityAsync(new ProductRequest
            {
                ProductId = id
            });
        }

        public async Task UpdateProduct(IFormCollection form)
        {
            var dto = JsonConvert.DeserializeObject<ProductUpdateDTO>(form["data"]);
            _uploadImages.DeleteImages(form["container"], dto.ImagesToDelete);

            _appContext.ProductImages.RemoveRange(dto.ImagesToDelete);
            var images = await _uploadImages.UploadFile(form);
            images.ForEach(image => image.ProductId = dto.Id);
            _appContext.ProductImages.AddRange(images);

            var product = _appContext.Products.Include(p => p.Properties).Include(p => p.Categories).FirstOrDefault(p => p.Id == dto.Id);
            if (product != null)
            {
                product.Link = dto.Link;
                product.Name = dto.Name;
                product.Description = dto.Description;
                product.Current_price = dto.Current_price;
                product.Previous_price = dto.Previous_price;
                product.BrandId = dto.BrandId;
                await UpdateProductCategories(product, dto.CategoryIds, _appContext);
                await UpdateProductProperties(product, dto.PropertyIds, _appContext);
            }
            _appContext.Entry(product).State = EntityState.Modified;
            _appContext.SaveChanges();

        }
        private async Task UpdateProductCategories(Product product, List<int> categoryIds, AppDbContext context)
        {
            // Obtener las categorías actualmente relacionadas con el producto
            var existingCategoryIds = product.Categories.Select(c => c.Id).ToList();

            // Identificar las categorías nuevas que no están ya relacionadas
            var newCategoryIds = categoryIds.Except(existingCategoryIds).ToList();

            // Agregar solo las categorías nuevas
            if (newCategoryIds.Any())
            {
                var newCategories = await context.Categories
                    .Where(c => newCategoryIds.Contains(c.Id))
                    .ToListAsync();

                foreach (var category in newCategories)
                {
                    product.Categories.Add(category);
                }
            }
        }
        private async Task UpdateProductProperties(Product product, List<int> propertyIds, AppDbContext context)
        {
            // Obtener las propiedades actualmente relacionadas con el producto
            var existingPropertyIds = product.Properties.Select(p => p.Id).ToList();

            // Identificar las propiedades nuevas que no están ya relacionadas
            var newPropertyIds = propertyIds.Except(existingPropertyIds).ToList();

            // Agregar solo las propiedades nuevas
            if (newPropertyIds.Any())
            {
                var newProperties = await context.PropertyValues
                    .Where(p => newPropertyIds.Contains(p.Id))
                    .ToListAsync();

                foreach (var property in newProperties)
                {
                    product.Properties.Add(property);
                }
            }
        }

        private async Task<CreateInventoryResponse> CreateInventory(InventoryDTORequest createInventory, int productId)
        {
            return await _inventoryClient.CreateProductInventoryAsync(new CreateInventoryRequest
            {
                ProductId = productId,
                Quantity = createInventory.Quantity,
                RetailPrice = Decimal.ToSingle(createInventory.RetailPrice),
                VendorId = createInventory.VendorId,
            });
        }
        //public async Task<Product> GetBySkuAsync(string sku)
        //{
        //    return await _appContext.Set<Product>()
        //        .AsNoTracking()
        //        .FirstOrDefaultAsync(p => p.Sku == sku);
        //}

    }
}
