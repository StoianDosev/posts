using NUnit.Framework;
using Moq;
using WebApi.Clients;
using WebApi.Models;
using System.Threading.Tasks;
using WebApi.Repositories;
using System.Linq;

namespace test
{
    public class PostRepositoryTests
    {
        Mock<IApiClient> client;

        [SetUp]
        public void Setup()
        {

            client = new Mock<IApiClient>();

        }

        [Test]
        public async Task Get_Test()
        {
            // Arrange
            Mock<IApiClient> client = new Mock<IApiClient>();

            PostRepository sut = new PostRepository();


            Post post = new Post() { Id = 128182019, Body = "test body", Title = "Test title", UserId = 1 };
            
            // Act
            var result = await sut.GetAll();

            // Assert
            Assert.IsTrue(result.Any());
        }

        [Test]
        public async Task Update_Test()
        {
            // Arrange
            PostRepository sut = new PostRepository();
            Post post = new Post() { Id = 1, Body = "test body", Title = "Test title", UserId = 1, Favorite = true };
            
            // Act
            await sut.Update(post);
            var result = await sut.GetAll();

            // Assert
            Assert.AreEqual(post.Favorite, result.First(x => x.Id == 1).Favorite);
        }        

        [Test]
        public async Task Create_Test()
        {
            Mock<IApiClient> client = new Mock<IApiClient>();

            PostRepository sut = new PostRepository();

            Post post = new Post() { Id = 0, Body = "test body", Title = "Test title", UserId = 1, Favorite = true };
            var beforeUpdatePosts = await sut.GetAll();
            int lastId = beforeUpdatePosts.OrderByDescending(x=>x.Id).First().Id;
            await sut.Create(post);
            var posts = await sut.GetAll();
            int id = posts.OrderByDescending(x=>x.Id).First().Id;
            Assert.Greater(id, lastId);
        }
    }
}