using Microsoft.AspNetCore.Mvc;
using Final.EFW.Database.EntityActions;
using System.Text.Json;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        // GET: api/<TagController>
        [HttpGet]
        public string Get()
        {
            return JsonSerializer.Serialize(TagEntity.GetAllTags());
        }

        // GET api/<TagController>/5
        [HttpGet("{id}")]
        public string Get(string id)
        {
            return JsonSerializer.Serialize(TagEntity.GetById(id));
        }

        // POST api/<TagController>
        [HttpPost]
        public void Post(string userId, string tagName)
        {
            TagEntity.Add(userId, tagName);
        }

        // PUT api/<TagController>/5
        [HttpPut("{id}")]
        public void Put(string id, string tagName)
        {
            TagEntity.UpdateById(id, tagName);
        }

        // DELETE api/<TagController>/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            TagEntity.DeleteById(id);
        }
    }
}
