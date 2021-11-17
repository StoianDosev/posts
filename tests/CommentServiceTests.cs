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
    public class CommentServiceTests
    {
        Mock<IRepository<Comment>> commRepo;

        [SetUp]
        public void SetUp()
        {
            commRepo = new Mock<IRepository<Comment>>();
        }

        [Test]
        public async Task GetCommentsByPost_Test()
        {
            // Arrange
            List<Comment> comments = new List<Comment>()
            {
                new Comment{Body="Test",Email = "test@asd.cxc",Name = "My name",PostId = 10, Id = 1 },
                new Comment{Body="Test2",Email = "test@asd.cxc232",Name = "My name2",PostId = 10, Id = 2 }
            };
            commRepo.Setup(x => x.GetAll()).ReturnsAsync(comments);

            ICommentService sut  = new CommentService(commRepo.Object);

            // Act
            var result = await sut.GetCommentsByPost(10);

            // Assert
            Assert.AreEqual(comments.First(x=>x.Id == 1).Body, result.First(x=>x.Id == 1).Body);
        }

        [Test]
        public async Task CreateComment_Test()
        {
            // Arrange
            var comment = 
                new Comment{Body="Test",Email = "test@asd.cxc",Name = "My name",PostId = 10, Id = 1 };

            commRepo.Setup( x => x.Create(It.IsAny<Comment>())).ReturnsAsync(comment);
            
            ICommentService sut  = new CommentService(commRepo.Object);

            // Act
            var result = await sut.Create(10,"","","Test");

            // Assert
            Assert.AreEqual(comment.Body, result.Body);
        }

        [Test]
        public async Task Delete_Test()
        {
            // Arrange
            var comment = 
                new Comment{Body="Test",Email = "test@asd.cxc",Name = "My name",PostId = 10, Id = 1 };

            int commentId = 0;
            commRepo.Setup( x => x.Delete(It.IsAny<int>())).Callback<int>(x=> commentId = x );
            
            ICommentService sut  = new CommentService(commRepo.Object);

            // Act
            await sut.Delete(10);

            // Assert
            Assert.AreEqual(10, commentId);
        }
    }
}