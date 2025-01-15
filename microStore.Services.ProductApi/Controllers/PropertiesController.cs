using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using microStore.Services.ProductApi.Data;
using microStore.Services.ProductApi.Models;
using microStore.Services.ProductApi.Models.DTO;

namespace microStore.Services.ProductApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertiesController : ControllerBase
    {
        private readonly AppDbContext _db;
        private ResponseDTO _response;
        private readonly IMapper _mapper;
        public PropertiesController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _response = new ResponseDTO();
            _mapper = mapper;
        }

        [HttpGet]
        [Route("id/{id:int}")]
        public object GetById(int id)
        {
            try
            {
                //PropertyType propertyType = _db.PropertyTypes.Where(c => c.PropertyTypeId == id).Include(p => p.Properties).First();
                //_response.Data = _mapper.Map<PropertyTypeDTO>(propertyType);

            }
            catch (Exception e)
            {

                _response.Success = false;
                _response.Message = e.Message;
            }
            return _response;

        }
        [HttpGet]
        [Route("{page:int}/{size:int}")]
        public object GetAll(int page, int size)
        {
            try
            {
                var count = _db.PropertyTypes.Count();
                var propertyTypes = _db.PropertyTypes.Include(x => x.Properties).ThenInclude(x => x.PropertyValues).Skip((page - 1) * size).Take(size); ;
                _response.Data = propertyTypes; //_mapper.Map<IEnumerable<PropertyTypeDTO>>(propertyTypes);
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
        [Route("propertiesResults/{query}")]
        public object GetAll(string query)
        {
            try
            {

                var propertyTypes = _db.Properties
                    .SelectMany(pr => pr.PropertyValues, (property, propertyValues) => new
                    {
                        Id = propertyValues.Id,
                        PropertyName = property.PropertyName +" - "+ propertyValues.PropertyValueName,
                    })
                    .Where(item => item.PropertyName.Contains(query))
                    .ToList();

                return propertyTypes;

            }
            catch (Exception e)
            {
                return "no encontro propriedades";
              
            }
           

        }
    }
}
