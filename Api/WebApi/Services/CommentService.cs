using System.Threading.Tasks;
using System.Linq;
using WebApi.Models;
using WebApi.Repositories;
using WebApi.Responses;
using System.Collections.Generic;

namespace WebApi.Services
{
    public class CommentService : ICommentService
    {
        private readonly IRepository<Comment> _commentRepository;

        public CommentService(IRepository<Comment> commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<IEnumerable<CommentItem>> GetCommentsByPost(int postId)
        {
            var comments = await _commentRepository.GetAll();
            var result = comments.Where(x => x.PostId == postId);
            return result.Select(x => new CommentItem()
            {
                Id = x.Id,
                Body = x.Body,                
                UserEmail = x.Email
            });
        }

        public async Task<CommentItem> Create(int postId, string userName, string email, string body)
        {
            Comment newComment = new Comment()
            {
                Body = body,
                Email = email,
                Name = userName,
                PostId = postId
            };
            var createdComment = await _commentRepository.Create(newComment);

            return new CommentItem()
            {
                Id = createdComment.Id,
                Body = createdComment.Body,
                UserEmail = createdComment.Email
            };
        }

        public async Task Delete(int id)
        {
            await _commentRepository.Delete(id);
        }
    }
}