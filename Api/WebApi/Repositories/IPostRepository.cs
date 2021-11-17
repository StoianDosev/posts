using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Repositories
{
    public interface IPostRepository: IRepository<Post>
    {
        Task Update(Post post);
        
    }
}