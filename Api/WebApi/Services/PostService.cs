using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using WebApi.Models;
using WebApi.Repositories;
using WebApi.Responses;
using System;
using WebApi.Extensions;

namespace WebApi.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IRepository<Comment> _commentRepository;
        private readonly IRepository<User> _userRepository;

        public PostService(IPostRepository postRepository,
            IRepository<Comment> commentRepository,
            IRepository<User> userRepository)
        {
            _postRepository = postRepository;
            _commentRepository = commentRepository;
            _userRepository = userRepository;
        }
        public async Task<IEnumerable<PostItem>> GetPosts(string sortBy, string sortOrder, int pageIndex = 0, int pageSize = 10)
        {
            IEnumerable<Post> posts = await _postRepository.GetAll();
            IEnumerable<Comment> commnets = await _commentRepository.GetAll();
            IEnumerable<User> users = await _userRepository.GetAll();

            IEnumerable<PostItem> result = from post in posts
                                           join comment in commnets
                                           on post.Id equals comment.PostId into comItems
                                           join user in users
                                           on post.UserId equals user.Id
                                           select new PostItem()
                                           {
                                               Id = post.Id,
                                               Title = post.Title,
                                               UserName = user.Name,
                                               Favorite = post.Favorite,
                                               CommentsCount = comItems.Count()
                                           };

            return result.Sort(sortBy, sortOrder).Page(pageIndex, pageSize);
        }

        public async Task<PostDetails> GetPostDetails(int id)
        {
            Post post = await _postRepository.GetById(id);
            IEnumerable<Comment> commnents = await _commentRepository.GetAll();
            IEnumerable<Comment> commentsForPost = commnents.Where(x => x.PostId == post.Id);

            PostDetails postDetails = new PostDetails()
            {
                Id = post.Id,
                Title = post.Title,
                Body = post.Body,
                Comments = commentsForPost
                .Select(x =>
                new CommentItem()
                {
                    Body = x.Body,
                    Id = x.Id,
                    UserEmail = x.Email
                })

            };
            return postDetails;
        }

        public async Task UpdateFavorite(int id, bool favorite)
        {
            await _postRepository.Update(new Post { Id = id, Favorite = favorite });
        }

        public async Task Delete(int id)
        {
            await _postRepository.Delete(id);
        }
    }
}