using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Models;
using WebApi.Responses;

namespace WebApi.Services
{
    public interface IPostService
    {
        Task<IEnumerable<PostItem>> GetPosts(string sortBy, string sortOrder, int pageIndex = 0, int pageSize = 10);
        Task<PostDetails> GetPostDetails(int id);
        Task UpdateFavorite(int id, bool favorite);
        Task Delete(int id);

    }
}