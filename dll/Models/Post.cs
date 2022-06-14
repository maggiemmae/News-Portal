using System;
using System.Collections.Generic;

namespace dal.Models
{
    public class Post
    {
        public int PostId { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public DateTime CreatedDate { get; set; }

        public int? AuthorId { get; set; }

        public string AuthorName { get; set; }

        public User User { get; set; }

        public IList<Comment> Comments { get; set; }

    }
}
