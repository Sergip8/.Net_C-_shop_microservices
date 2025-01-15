using Microsoft.AspNetCore.Mvc;
using microStore.Services.InventoryApi.Data;
using microStore.Services.InventoryApi.Models.DTO;

namespace microStore.Services.InventoryApi.Controllers
{
[Route("api/[controller]")]
[ApiController]
public class VendorController: Controller
{
    private readonly AppDbContext _db;
    private ResponseDTO _response;

    public VendorController(AppDbContext db)
    {
        _db = db;
        _response = new ResponseDTO();
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetVendors()
    {
        var vendors = _db.Vendors.ToList();
        _response.Data = vendors;
        return Ok(vendors);
    }
    
}
}