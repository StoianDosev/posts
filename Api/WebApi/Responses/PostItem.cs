namespace WebApi.Responses
{
    public class PostItem
    {
        public int Id { get; set; }        
        public string Title { get; set; }
        public string UserName { get; set; }
        public int CommentsCount { get; set; }
        public bool Favorite { get; set; }
    }
}