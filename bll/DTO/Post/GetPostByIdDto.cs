using bll.DTO.Comment;
using System;
using System.Collections.Generic;

namespace bll.DTO.Post
{
    public class GetPostByIdDto
    {
        public int PostId { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public DateTime CreatedDate { get; set; }

        public string AuthorName { get; set; }

        public IList<GetCommentDto> Comments { get; set; }

    }
}
