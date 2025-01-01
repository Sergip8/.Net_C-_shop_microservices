
using microStore.Services.OrderApi.Models;
using microStore.Services.OrderApi.Data;
using microStore.Services.OrderApi.Models.DTO;
using Microsoft.EntityFrameworkCore;
using microStore.Services.OrderApi.Service.IService;
using EventBusMessages.Events.Contracts;
using MassTransit;

namespace microStore.Services.OrderApi.Service
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _dbContext;
        private readonly ResponseDTO _response;
        private readonly IBus _bus;
        public OrderService(AppDbContext dbContext, IBus bus)
        {
            _bus = bus;
            _response = new ResponseDTO();
            _dbContext = dbContext;
        }

        public async Task<ResponseDTO> CreateOrderAsync(CreateOrderRequest request)
        {
            // Validar datos básicos
            if (request == null || request.OrderDetails == null || request.OrderDetails.Count == 0)
                throw new ArgumentException("La orden debe contener al menos un detalle.");

            var isUserValid = await ValidateUser(request.UserId);

            // Calcular el total de la orden
            if (!isUserValid)
            {
                throw new ArgumentException("El usuario no es valido");
            }
            var productIds = request.OrderDetails.Select(o => new OrderProduct { ProductId = o.ProductId, Quantity = o.Quantity }).ToList();

            var isInventoryValid = await ValidateInventory(productIds);
            var productsNoStock = new List<string>();
            foreach (var i in isInventoryValid)
            {
                var productName = "";
                if (!i.IsValid)
                {
                    foreach (var item in request.OrderDetails)
                    {
                        if (item.ProductId == i.ProductId)
                        {
                            productName = item.ProductName;
                        }
                    }
                    productsNoStock.Add($"no hay suficientes productos {productName} en stock");
                }
            }
            if (productsNoStock.Count > 0)
            {
                _response.Data = productsNoStock;
                _response.Success = false;
                return _response;
            }
            decimal orderTotal = 0m;
            foreach (var detail in request.OrderDetails)
            {
                orderTotal += detail.UnitPrice * detail.Quantity;
            }

            // Aplicar descuento si hay cupón
            if (!string.IsNullOrEmpty(request.CouponCode) && request.Discount.HasValue)
            {
                orderTotal -= orderTotal * (request.Discount.Value / 100m);
            }

            // Crear entidades
            var shipping = new Shipping
            {
                Address = request.ShippingAddress,
                City = request.ShippingCity,
                PostalCode = request.ShippingPostalCode,
                Country = request.ShippingCountry,
                Status = ShippingStatus.Pending,
            };

            var payment = new Payment
            {
                Method = request.PaymentMethod,
                Amount = orderTotal,
                Status = PaymentStatus.Pending
            };

            var orderHeader = new OrderHeader
            {
                UserId = request.UserId,
                Name = request.Name,
                Phone = request.Phone,
                Email = request.Email,
                CouponCode = request.CouponCode,
                Discount = request.Discount,
                Status = OrderStatus.Pending,
                OrderTotal = orderTotal,
                OrderTime = DateTime.UtcNow,
                Payment = payment,
                Shipping = shipping
            };
            try
            {
                _dbContext.OrderHeaders.Add(orderHeader);
                await _dbContext.SaveChangesAsync();
                var orderDetails = request.OrderDetails.Select(p => new OrderDetails
                {
                    OrderHeaderId = orderHeader.OrderHeaderId, // Asegúrate de usar el ID generado
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    UnitPrice = p.UnitPrice,
                    Quantity = p.Quantity
                }).ToList();

                // Agregar las entidades al contexto
                _dbContext.OrderDetails.AddRange(orderDetails);

                // Guardar cambios en la base de datos
                await _dbContext.SaveChangesAsync();
                _response.Data = orderHeader;
                _response.Message = "la orden se ha creado";
                return _response;

            }
            catch
            {
                _response.Data = null;
                _response.Success = false;
                _response.Message = "hubo un error al crear la orden";
                return _response;
            }
        }
        public async Task<ResponseDTO> GetOrdersByUserIdAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                throw new ArgumentException("El UserId no puede ser nulo o vacío.");

            var orders = await _dbContext.OrderHeaders
                .Where(o => o.UserId == userId)
                .Include(o => o.OrderDetails)
                .Include(o => o.Payment)
                .Include(o => o.Shipping)
                .ToListAsync();

            if (orders == null || orders.Count == 0)
            {
                _response.Data = null;
                _response.Message = "no hay ninguna orden";
                return _response;
            }


            var order = orders.Select(o => new OrderResponse
            {
                OrderHeaderId = o.OrderHeaderId,
                UserId = o.UserId,
                Name = o.Name,
                Email = o.Email,
                Phone = o.Phone,
                CouponCode = o.CouponCode,
                Discount = o.Discount,
                Status = o.Status,
                OrderTotal = o.OrderTotal,
                OrderTime = o.OrderTime,
                Payment = new PaymentResponse
                {
                    PaymentId = o.Payment.PaymentId,
                    Method = o.Payment.Method,
                    Amount = o.Payment.Amount,
                    Status = o.Payment.Status,
                    PaidAt = o.Payment.PaidAt
                },
                Shipping = new ShippingResponse
                {
                    ShippingId = o.Shipping.ShippingId,
                    Address = o.Shipping.Address,
                    City = o.Shipping.City,
                    PostalCode = o.Shipping.PostalCode,
                    Country = o.Shipping.Country,
                    Status = o.Shipping.Status,
                    ShippedAt = o.Shipping.ShippedAt,
                    DeliveredAt = o.Shipping.DeliveredAt
                },
                OrderDetails = o.OrderDetails.Select(od => new OrderDetailResponse
                {
                    OrderDetailsId = od.OrderDetailsId,
                    ProductId = od.ProductId,
                    ProductName = od.ProductName,
                    UnitPrice = od.UnitPrice,
                    Quantity = od.Quantity
                }).ToList()
            }).ToList();
            _response.Success = true;
            _response.Data = order;
            _response.Count = order.Count;
            _response.Message = "order list";
            return _response;
        }
        public async Task<bool> ValidateUser(string userId)
        {
            var response = await _bus.Request<ValidateUserRequest, ValidateUserResponse>(
                new ValidateUserRequest { UserId = userId });

            return response?.Message.IsValid ?? false;
        }

        public async Task<List<ValidateInventory>> ValidateInventory(List<OrderProduct> products)
        {
            var response = await _bus.Request<ValidateInventoryRequest, ValidateInventoryResponse>(
                new ValidateInventoryRequest { Products = products });
            return response.Message.InventoryResponse;
        }
    }
}
