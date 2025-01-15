using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using microStore.Services.ProductApi.Data;
using microStore.Services.ProductApi.Models;
using microStore.Services.ProductApi.Models.DTO;

namespace microStore.Services.ProductApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext _db;
        private ResponseDTO _response;
        private readonly IMapper _mapper;
        public CategoryController(AppDbContext db, IMapper mapper)
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
                Category category = _db.Categories.First(c => c.Id == id);
                _response.Data = _mapper.Map<CategoryDTO>(category);

            }
            catch (Exception e)
            {

                _response.Success = false;
                _response.Message = e.Message;
            }
            return _response;

        }

        [HttpGet]
        [Route("name/{name}/{level:int}/{parentId:int}")]
        public object GetByName(string name, int level, int parentId)
        {
            try
            {
                IEnumerable<Category> category = _db.Categories.Where(x => x.CategoryLevel == level && x.CategoryParentId == parentId && (x.CategoryName.Contains(name) || x.CategoryName.StartsWith(name) || x.CategoryName.EndsWith(name)));
                if (category != null)
                {
                    _response.Data = _mapper.Map<IEnumerable<CategoryReadDTO>>(category);
                }

            }
            catch (Exception e)
            {

                _response.Success = false;
                _response.Message = e.Message;
            }
            return _response;

        }
        [HttpGet]
        public object Get()
        {
            try
            {
                IEnumerable<Category> category = _db.Categories.ToList();
                _response.Data = _mapper.Map<IEnumerable<CategoryReadDTO>>(category);

            }
            catch (Exception e)
            {

                _response.Success = false;
                _response.Message = e.Message;
            }
            return _response;

        }
        [HttpPost]
        public async Task<object> Post([FromBody] CategoryDTO categoryDTO)
        {
            try
            {
                Category category = _mapper.Map<Category>(categoryDTO);
                if (categoryDTO.ProductIds.Count() > 0)
                {
                    ICollection<Product> products = _db.Products
                                          .Where(p => categoryDTO.ProductIds.Contains(p.Id))
                                          .ToList();

                    category.Products = products;

                }

                _db.Categories.Add(category);

                await _db.SaveChangesAsync();

                _response.Data = category;
                _response.Message = "producto creado";

            }
            catch (Exception e)
            {

                _response.Success = false;
                _response.Message = e.Message;
            }
            return _response;

        }
        [HttpPut]
        public object Put([FromBody] CategoryDTO categoryDTO)
        {
            try
            {
                _db.Categories.Update(_mapper.Map<Category>(categoryDTO));
                _db.SaveChanges();

                _response.Message = "categoria modificado con exito";

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
