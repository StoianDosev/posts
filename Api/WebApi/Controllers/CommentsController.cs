using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi.Requests;
using WebApi.Services;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _service;

        public CommentsController(ICommentService service)
        {
            _service = service;
        }

        [HttpGet("{postId}")]
        public async Task<IActionResult> Get(int postId)
        {
            var result = await _service.GetCommentsByPost(postId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateComment comment)
        {
            var result = await _service.Create(comment.PostId, comment.UserName, comment.UserEmail, comment.Body);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return Ok();
        }

    }
}