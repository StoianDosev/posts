namespace WebApi.Requests
{
    public class CreateComment
    {
        public int PostId{get;set;}
        public string UserName{get;set;}
        public string UserEmail{get;set;}
        public string Body{get;set;}
    }
}