using Final.EFW.Database.EntityActions;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        // GET api/<CommentController>/5
        [HttpGet("{id}")]
        public string Get(string articleId)
        {
            return JsonSerializer.Serialize(CommentEntity.GetByArticleId(articleId));
        }

        // POST api/<CommentController>
        [HttpPost]
        public void Post(string articleId, string userId, string commentText)
        {
            CommentEntity.AddByIds(articleId, userId, commentText); 
        }

        // PUT api/<CommentController>/5
        [HttpPut("{id}")]
        public void Put(string commentId, string commentText)
        {
            CommentEntity.Update(commentId, commentText);
        }

        // DELETE api/<CommentController>/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            CommentEntity.Delete(id);
        }
    }
}
