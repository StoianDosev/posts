using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebApi.Models;

namespace WebApi.Repositories
{
    public class PostRepository : JsonRepository<Post>, IPostRepository
    {

        public PostRepository()
        {

        }
        public async Task Update(Post post)
        {
            var jArray = await GetJsonArray();

            foreach (var jPost in jArray)
            {
                if (jPost["id"].Value<int>() == post.Id)
                {
                    jPost["favorite"] = post.Favorite.ToString();
                }
            }

            await Save(jArray);
        }
    }
}