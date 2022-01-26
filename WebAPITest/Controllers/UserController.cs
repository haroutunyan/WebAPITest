using Domain.Users;
using Domain.Users.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAPITest.Controllers
{
    [Route("api/[controller]/[action]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ApiController]
    public class UserController : ControllerBase
    {
        readonly IUserManager _manager;

        public UserController(IUserManager manager)
        {
            _manager = manager;
        }

        [HttpGet("/api/[controller]/{id}")]
        public Task<UserModel> Get(int id) => _manager.Get(id);

        [HttpPut]
        public Task Update(UserModel model) => _manager.Update(model);

        [HttpDelete]
        public Task Delete(int id) => _manager.Delete(id);

        [HttpPost]
        public Task Add(UserModel model) => _manager.Create(model);
    }
}
