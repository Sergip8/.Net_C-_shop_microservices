using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using microStore.Services.ProductApi.Models.DTO;
using microStore.Services.ProductApi.Models;
using microStore.Services.ProductApi.Data;
using AutoMapper;

namespace microStore.Services.ProductApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly AppDbContext _db;
        private ResponseDTO _response;
        private readonly IMapper _mapper;
        public BrandController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _response = new ResponseDTO();
            _mapper = mapper;
        }


        [HttpGet]
        [Route("name/{name}")]
        public object GetByName(string name)
        {
            try
            {
                IEnumerable<Brand> category = _db.Brands.Where(x => x.BrandName.Contains(name) || x.BrandName.StartsWith(name) || x.BrandName.EndsWith(name));
                if (category != null)
                {
                    _response.Data = _mapper.Map<IEnumerable<BrandDTO>>(category);
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
