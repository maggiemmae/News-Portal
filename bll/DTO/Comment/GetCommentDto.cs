using System;

namespace bll.DTO.Comment
{
    public class GetCommentDto
    {
        public int CommentId { get; set; }

        public string Text { get; set; }

        public DateTime CreatedDate { get; set; }

        public string AuthorName { get; set; }

    }
}
