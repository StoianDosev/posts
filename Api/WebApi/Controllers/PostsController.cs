using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi.Requests;
using WebApi.Services;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {

        private readonly IPostService _postService;

        public PostsController(IJsonObjectsInitializer initializer,
        IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _postService.GetPostDetails(id);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(string sortBy, string sortOrder, int pageIndex = 0, int pageSize = 10)
        {
            var result = await _postService.GetPosts(sortBy, sortOrder, pageIndex, pageSize);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdatePost request)
        {
            if (request.Id == 0)
                return NotFound();
            await _postService.UpdateFavorite(request.Id, request.Favorite);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _postService.Delete(id);
            return Ok();
        }
    }
}