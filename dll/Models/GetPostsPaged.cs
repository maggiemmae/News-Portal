using System.Collections.Generic;

namespace dal.Models
{
    public class GetPostsPaged
    {
        public IList<Post> Posts { get; set; }

        public int Pages { get; set; }

        public GetPostsPaged(int pages, IList<Post> posts)
        {
            Pages = pages;
            Posts = posts;
        }
    }
}
