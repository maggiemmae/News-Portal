using AutoMapper;
using bll.DTO.Comment;
using bll.DTO.Post;
using bll.DTO.User;
using dal.Models;

namespace bll.Maps
{
    public class Profiles : Profile
    {
        public Profiles()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Post, GetPostsDto>().ReverseMap();
            CreateMap<Post, GetPostByIdDto>().ReverseMap();
            CreateMap<Comment, GetCommentDto>().ReverseMap();
        }
    }
}
