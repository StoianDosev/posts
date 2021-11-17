using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Responses;

namespace WebApi.Services
{
    public interface ICommentService
    {
        Task<IEnumerable<CommentItem>> GetCommentsByPost(int postId);
        Task<CommentItem> Create(int postId, string userName, string email, string body);
        Task Delete(int id);
    }
}