using Final.EFW.Database.EntityActions;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        // GET: api/<ArticleController>
        [HttpGet]
        public string Get()
        {
            return JsonSerializer.Serialize(ArticleEntity.GetAll());
        }

        // GET api/<ArticleController>/5
        [HttpGet("{id}")]
        public string Get(string id)
        {
            return JsonSerializer.Serialize(ArticleEntity.GetByid(id));
        }

        // POST api/<ArticleController>
        [HttpPost]
        public void Post(string subject, string text, string authorId)
        {
            ArticleEntity.Add(subject, text, authorId);
        }

        // PUT api/<ArticleController>/5
        [HttpPut("{id}")]
        public void Put(string id, string subject, string text)
        {
            ArticleEntity.Update(id, subject, text);
        }

        // DELETE api/<ArticleController>/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            ArticleEntity.Delete(id);
        }
    }
}
