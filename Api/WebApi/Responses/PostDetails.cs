using System.Collections.Generic;

namespace WebApi.Responses
{
    public class PostDetails
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public IEnumerable<CommentItem> Comments { get; set; }
    }
}