using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using microStore.Services.CouponApi.Data;
using microStore.Services.CouponApi.Models;
using microStore.Services.CouponApi.Models.DTO;
using System.Collections.Generic;

namespace microStore.Services.CouponApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "ADMIN")]
    public class CouponApiController : ControllerBase
    {
        private readonly AppDbContext _db;
        private ResponseDTO _response;
        private readonly IMapper _mapper;
        public CouponApiController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _response = new ResponseDTO();
            _mapper = mapper;
        }

        [HttpGet]
        [Route("{page:int}/{size:int}")]
        public object Get(int page, int size)
        {
            try
            {
                var count = _db.Coupons.Count();
                IEnumerable<Coupon> coupons = _db.Coupons.Skip((page - 1) * size).Take(size);
                _response.Data = _mapper.Map<IEnumerable<CouponDTO>>(coupons).Reverse();
                _response.Count = count;

            }
            catch (Exception e)
            {

                _response.Success = false;
                _response.Message = e.Message;
            }
            return _response;

        }

        [HttpGet]
        [Route("{id:int}")]
        public object Get(int id)
        {
            try
            {
                Coupon coupon = _db.Coupons.First(c => c.CouponId == id);
                _response.Data = _mapper.Map<CouponDTO>(coupon);

            }
            catch (Exception e)
            {

                _response.Success = false;
                _response.Message = e.Message;
            }
            return _response;

        }
        [HttpGet]
        [Route("SearchCode/{code}")]
        public object Get(string code)
        {
            try
            {
                IEnumerable<Coupon> coupon = _db.Coupons.Where(x => x.CouponCode.Contains(code) || x.CouponCode.StartsWith(code) || x.CouponCode.EndsWith("zil"));
                if (coupon == null)
                {
                    _response.Success = false;
                    _response.Message = "El cupon no existe";
                }
                _response.Data = _mapper.Map<IEnumerable<CouponDTO>>(coupon);

            }
            catch (Exception e)
            {

                _response.Success = false;
                _response.Message = e.Message;
            }
            return _response;

        }
        [HttpGet]
        [Route("code/{code}")]
        public object GetByCode(string code)
        {
            try
            {
                Coupon obj = _db.Coupons.First(u => u.CouponCode.ToLower() == code.ToLower());
                _response.Data = _mapper.Map<CouponDTO>(obj);
            }
            catch (Exception ex)
            {
                _response.Success = false;
                _response.Message = ex.Message;
            }
            return _response;

        }

        [HttpPost]
        public async Task<object> Post([FromBody] CouponDTO couponDTO)
        {
            try
            {
                Coupon coupon = _mapper.Map<Coupon>(couponDTO);
                _db.Coupons.Add(coupon);

                await _db.SaveChangesAsync();
                int lastId = coupon.CouponId;
                coupon.CouponId = lastId;
                _response.Data = coupon;
                _response.Message = "cupon creado";

            }
            catch (Exception e)
            {

                _response.Success = false;
                _response.Message = e.Message;
            }
            return _response;

        }
        [HttpPut]
        public object Put([FromBody] CouponDTO couponDTO)
        {
            try
            {
                _db.Coupons.Update(_mapper.Map<Coupon>(couponDTO));
                _db.SaveChanges();

                _response.Message = "cupon modificado con exito";

            }
            catch (Exception e)
            {

                _response.Success = false;
                _response.Message = e.Message;
            }
            return _response;

        }
        [HttpDelete]
        [Route("delete/{id:int}")]
        public object Delete(int id)
        {
            try
            {
                Coupon coupon = _db.Coupons.First(c => c.CouponId == id);
                {
                    _response.Success = false;
                    _response.Message = "El cupon no existe";

                }
                _db.Remove(coupon);
                _db.SaveChanges();
                if (coupon == null)

                    _response.Data = _mapper.Map<CouponDTO>(coupon);
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
