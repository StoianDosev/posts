using NUnit.Framework;
using WebApi.Services;
using Moq;
using WebApi.Clients;
using WebApi.Models;
using System.Threading.Tasks;
using WebApi.Repositories;
using System.Linq;
using System.Collections.Generic;

namespace tests
{
    [TestFixture]
    public class PostServiceTests
    {      
        Mock<IPostRepository> postRepo;
        Mock<IRepository<Comment>> commRepo;
        Mock<IRepository<User>> userRepo;  

        [SetUp]
        public void Setup()
        {
            postRepo = new Mock<IPostRepository>();
            commRepo = new Mock<IRepository<Comment>>();
            userRepo = new Mock<IRepository<User>>();
        }

        [Test]
        public async Task GetAllTaskItems_Test()
        {
            // Arrange
            IEnumerable<Post> posts = new List<Post>() { new Post { Id = 1, UserId = 1, Title = "Test1" }, new Post { Id = 2, UserId = 2, Title = "Zanzibar" } };
            postRepo.Setup(p => p.GetAll()).ReturnsAsync(posts);

            IEnumerable<Comment> comments = new List<Comment> { new Comment { Id = 1, PostId = 1 }, new Comment { Id = 2, PostId = 2 } };
            commRepo.Setup(c => c.GetAll()).ReturnsAsync(comments);

            IEnumerable<User> users = new List<User> { new User { Id = 1, UserName = "Pesho" }, new User { Id = 2, UserName = "Gosho" } };
            userRepo.Setup(u=> u.GetAll()).ReturnsAsync(users);    

            PostService sut = new PostService(postRepo.Object, commRepo.Object, userRepo.Object);

            //Act
            var result = await sut.GetPosts("title", "desc");

            // Assert
            Assert.AreEqual("Zanzibar", result.First().Title);
        }

        [Test]
        public async Task GetPostDetails_Test()
        {
            //Arrange
            Post posts = new Post { Id = 1, UserId = 1, Title = "Zanzibar" };
            postRepo.Setup( p => p.GetById(It.IsAny<int>())).ReturnsAsync(posts);

            IEnumerable<Comment> comments = new List<Comment> { new Comment { Id = 1, PostId = 1 }, new Comment { Id = 2, PostId = 2 } };
            commRepo.Setup(c => c.GetAll()).ReturnsAsync(comments);

            
            PostService sut = new PostService(postRepo.Object, commRepo.Object, userRepo.Object);

            //Act
            var result = await sut.GetPostDetails(1);

            //Assert
            Assert.AreEqual("Zanzibar", result.Title);
        }
    }
}