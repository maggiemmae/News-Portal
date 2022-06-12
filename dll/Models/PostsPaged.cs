using System.Collections.Generic;

namespace dal.Models
{
    public class PostsPaged
    {
        public IList<Post> Posts { get; set; }

        public int Pages { get; set; }

        public PostsPaged(int pages, IList<Post> posts)
        {
            Pages = pages;
            Posts = posts;
        }
    }
}
