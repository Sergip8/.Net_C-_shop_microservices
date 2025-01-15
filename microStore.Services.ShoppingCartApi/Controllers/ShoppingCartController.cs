using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using microStore.Services.ShoppingCartApi.Models.DTO;
using microStore.Services.ShoppingCartApi.Data;
using microStore.Services.ShoppingCartApi.Models;

using System.Reflection.PortableExecutable;
using microStore.Services.ShoppingCartApi.Service.IService;

namespace microStore.Services.ShoppingCartApi.Controllers
{
    [Route("api/cart")]
    [ApiController]
    public class CartApiController : ControllerBase
    {

        private ResponseDTO _responseDTO;
        private IMapper _mapper;
        private readonly AppDbContext _db;
        private IProductService _productService;
        private ICouponService _couponService;

        public CartApiController(IMapper mapper, AppDbContext db, IProductService productService, ICouponService couponService)
        {
            this._responseDTO = new ResponseDTO();
            _mapper = mapper;
            _db = db;
            _productService = productService;
            _couponService = couponService;
        }
        [HttpPost("CartUpsert")]
        public async Task<ResponseDTO> Upsert(CartDTO cartDTO)
        {
            try
            {
                var cartHeaderDb = await _db.CartHeaders.AsNoTracking().FirstOrDefaultAsync(h => h.UserId == cartDTO.CartHeader.UserId);
                if (cartHeaderDb == null)
                {
                    CartHeader cartHeader = _mapper.Map<CartHeader>(cartDTO.CartHeader);
                    _db.CartHeaders.Add(cartHeader);
                    await _db.SaveChangesAsync();
                    cartDTO.CartDetails.First().CartHeaderId = cartHeader.CartHeaderId;
                    var cartdetails = _mapper.Map<CartDetails>(cartDTO.CartDetails.First());
                    _db.CartDetails.Add(cartdetails);
                    await _db.SaveChangesAsync();
                }
                else
                {
                    var cartDetailsDb = await _db.CartDetails.AsNoTracking().FirstOrDefaultAsync(d => d.ProductId == cartDTO.CartDetails.First().ProductId &&
                                            d.CartHeaderId == cartHeaderDb.CartHeaderId);
                    if (cartDetailsDb == null)
                    {
                        cartDTO.CartDetails.First().CartHeaderId = cartHeaderDb.CartHeaderId;
                        if (cartDTO.CartDetails.Count() > 1)
                        {
                            _db.CartDetails.AddRange(_mapper.Map<IEnumerable<CartDetails>>(cartDTO.CartDetails));
                        }
                        else
                        {
                            _db.CartDetails.Add(_mapper.Map<CartDetails>(cartDTO.CartDetails.First()));
                        }

                        await _db.SaveChangesAsync();
                    }
                    else
                    {
                        cartDTO.CartDetails.First().Count += cartDetailsDb.Count;
                        if (cartDTO.CartDetails.First().Count < 1)
                        {
                            _db.CartDetails.Remove(cartDetailsDb);
                            await _db.SaveChangesAsync();
                        }
                        else
                        {
                            cartDTO.CartDetails.First().CartHeaderId = cartDetailsDb.CartHeaderId;
                            cartDTO.CartDetails.First().CartDetailsId = cartDetailsDb.CartDetailsId;
                            _db.CartDetails.Update(_mapper.Map<CartDetails>(cartDTO.CartDetails.First()));
                            await _db.SaveChangesAsync();

                        }
                    }
                    _responseDTO.Data = cartDTO;
                }
            }
            catch (Exception ex)
            {
                _responseDTO.Success = false;
                _responseDTO.Message = ex.Message;
            }
            return _responseDTO;
        }

        [HttpDelete("CartDelete")]
        public async Task<ResponseDTO> Delete([FromBody] int carDetailsId)
        {
            try
            {
                CartDetails cartDetails = _db.CartDetails.First(cd => cd.CartDetailsId == carDetailsId);
                int totalCountOfCartItems = _db.CartDetails.Where(cd => cd.CartDetailsId == cartDetails.CartDetailsId).Count();
                _db.CartDetails.Remove(cartDetails);
                if (totalCountOfCartItems == 1)
                {
                    var cartHeaderToRemove = await _db.CartHeaders.FirstOrDefaultAsync(ch => ch.CartHeaderId == cartDetails.CartHeaderId);
                    _db.CartHeaders.Remove(cartHeaderToRemove);
                }
                await _db.SaveChangesAsync();
                _responseDTO.Success = true;

            }
            catch (Exception ex)
            {
                _responseDTO.Success = false;
                _responseDTO.Message = ex.Message;
            }
            return _responseDTO;
        }

        [HttpGet("GetUserCart/{userId}")]
        public async Task<ResponseDTO> GetCart(string userId)
        {
            try
            {
                var cartHeader = _db.CartHeaders.First(ch => ch.UserId == userId);
                var cartDetails = _db.CartDetails.Where(cd => cd.CartHeaderId == cartHeader.CartHeaderId);

                CartDTO cart = new()
                {
                    CartHeader = _mapper.Map<CartHeaderDTO>(cartHeader),
                    CartDetails = _mapper.Map<IEnumerable<CartDetailsDTO>>(cartDetails)
                };
                var productsDto = await _productService.GetProductsAsync(cart.CartDetails.Select(p => p.ProductId).ToList());
                foreach (var item in cart.CartDetails)
                {
                    item.Product = productsDto.FirstOrDefault(p => p.ProductId == item.ProductId);
                    cart.CartHeader.CartTotal += (item.Count * item.Product.Price);
                }
                if (!string.IsNullOrEmpty(cart.CartHeader.CouponCode))
                {
                    CouponDTO coupon = await _couponService.GetCoupon(cart.CartHeader.CouponCode);
                    if (coupon != null && cart.CartHeader.CartTotal > coupon.MinAmount)
                    {
                        cart.CartHeader.CartTotal -= coupon.DiscountAmount;
                        cart.CartHeader.Discount = coupon.DiscountAmount;
                    }
                }

                _responseDTO.Success = true;
                _responseDTO.Data = cart;
            }
            catch (Exception ex)
            {
                _responseDTO.Success = false;
                _responseDTO.Message = ex.Message;
            }
            return _responseDTO;
        }
        [HttpPost("ApplyCoupon")]
        public async Task<ResponseDTO> ApplyCoupon([FromBody] CartDTO cartDTO)
        {
            try
            {
                var cartDb = await _db.CartHeaders.FirstAsync(ch => ch.UserId == cartDTO.CartHeader.UserId);
                cartDb.CouponCode = cartDTO.CartHeader.CouponCode;
                _db.CartHeaders.Update(cartDb);
                await _db.SaveChangesAsync();
                _responseDTO.Success = true;

            }
            catch (Exception ex)
            {
                _responseDTO.Success = false;
                _responseDTO.Message = ex.Message;
            }
            return _responseDTO;
        }
        [HttpPost("RemoveCoupon")]
        public async Task<ResponseDTO> RemoveCoupon([FromBody] CartDTO cartDTO)
        {
            try
            {
                var cartDb = await _db.CartHeaders.FirstAsync(ch => ch.UserId == cartDTO.CartHeader.UserId);
                cartDb.CouponCode = "";
                _db.CartHeaders.Update(cartDb);
                await _db.SaveChangesAsync();
                _responseDTO.Success = true;

            }
            catch (Exception ex)
            {
                _responseDTO.Success = false;
                _responseDTO.Message = ex.Message;
            }
            return _responseDTO;
        }
    }

}
