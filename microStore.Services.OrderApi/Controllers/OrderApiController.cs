using Microsoft.AspNetCore.Mvc;
using microStore.Services.OrderApi.Models.DTO;
using microStore.Services.OrderApi.Service;
using microStore.Services.OrderApi.Service.IService;

namespace microStore.Services.OrderApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderApiController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderApiController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("order")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
        {
            try
            {
                var orderId = await _orderService.CreateOrderAsync(request);
                return Ok(new { OrderId = orderId });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetOrdersByUserId(string userId)
        {
            try
            {
                var orders = await _orderService.GetOrdersByUserIdAsync(userId);

                //if (orders == null || orders.Count == 0)
                //    return NotFound(new { Message = "No se encontraron órdenes para este usuario." });

                return Ok(orders);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
    }
}
