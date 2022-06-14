using System;

namespace dal.Models
{
    public class Comment
    {
        public int CommentId { get; set; }

        public string Text { get; set; }

        public DateTime CreatedDate { get; set; }

        public int? AuthorId { get; set; }

        public string AuthorName { get; set; }

        public User User { get; set; }

        public int PostId { get; set; }

        public Post Post { get; set; }
    }
}
