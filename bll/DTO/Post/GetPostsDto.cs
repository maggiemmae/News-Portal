using System;

namespace bll.DTO.Post
{
    public class GetPostsDto
    {
        public int PostId { get; set; }

        public string Title { get; set; }

        public DateTime CreatedDate { get; set; }

        public string AuthorName { get; set; }
    }
}
