using Final.EFW.Database.EntityActions;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // GET: api/<UserController>
        [HttpGet]
        public string Get()
        {
            return JsonSerializer.Serialize(UserEntity.GetAll());
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public string Get(string id)
        {
            return JsonSerializer.Serialize(UserEntity.GetById(id));
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            UserEntity.Delete(id);
        }
    }
}
