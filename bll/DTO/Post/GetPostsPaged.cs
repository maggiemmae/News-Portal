using System.Collections.Generic;

namespace bll.DTO.Post
{
    public class GetPostsPaged
    {
        public IList<GetPostsDto> Posts { get; set; }

        public int Pages { get; set; }

        public int CurrentPage { get; set; }
    }
}
