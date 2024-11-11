using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using microStore.Services.InventoryApi.Data;
using microStore.Services.InventoryApi.Models.DTO;

namespace microStore.Services.InventoryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly AppDbContext _db;
        private ResponseDTO _response;

        public InventoryController(AppDbContext db)
        {
            _db = db;
            _response = new ResponseDTO();

        }

        [HttpGet]
        [Route("{productId:int}")]
        public object GetByProductId(int productId)
        {
            try
            {
                var inventory = _db.Inventories.Where(x => x.ProductId == productId)
                 .Join(
                    _db.Vendors,
                    inventory => inventory.VendorId,
                    vendor => vendor.VendorId,
                    (inventory, vendor) => new
                    {
                        inventoryId = inventory.InventoryId,
                        quantity = inventory.Quantity,
                        vendorName = vendor.VendorName

                    }).First();
                if (inventory != null)
                {
                    _response.Data = inventory;

                }

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
