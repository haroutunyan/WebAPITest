using Domain.Categories;
using Domain.Categories.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAPITest.Controllers
{
    [Route("api/[controller]/[action]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        ICategoryManager _manager;

        public CategoryController(ICategoryManager manager)
        {
            _manager = manager;
        }

        [HttpGet("/api/[controller]/{id}")]
        public Task<CategoryModel> Get(int id) => _manager.Get(id);

        [HttpPut]
        public Task Update(CategoryModel model) => _manager.Update(model);

        [HttpDelete]
        public Task Delete(int id) => _manager.Delete(id);

        [HttpPost]
        public Task Add(CategoryModel model) => _manager.Create(model);
    }
}
