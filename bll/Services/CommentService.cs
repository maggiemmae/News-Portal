using AutoMapper;
using bll.DTO.Comment;
using bll.Interfaces;
using dal.Interface;
using dal.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bll.Services
{
    public class CommentService : ICommentService
    {
        private readonly IRepository<Comment> commentRepository;
        private readonly UserManager<User> userManager;

        private readonly IMapper mapper;

        public CommentService(IRepository<Comment> commentRepository, IMapper mapper, UserManager<User> userManager)
        {
            this.commentRepository = commentRepository;
            this.mapper = mapper;
            this.userManager = userManager;

        }
        public async Task<bool> AddCommentAsync(AddCommentDto comment, string userName, int postId)
        {
            var user = await userManager.FindByNameAsync(userName);
            if (user == null || comment == null || postId == 0) return false;
            var item = new Comment()
            {
                Text = comment.Text,
                CreatedDate = DateTime.UtcNow,
                AuthorId = user.Id,
                AuthorName = user.UserName,
                PostId = postId,
            };
            await commentRepository.CreateAsync(item);
            return true;
        }

        public async Task DeleteCommentAsync(int id)
        {
            await commentRepository.DeleteAsync(id);
        }

        public async Task<GetCommentDto> GetCommentByIdAsync(int id)
        {
            var comment = await commentRepository.GetByIdAsync(id);
            if (comment == null) {
                throw new NullReferenceException("Comment not found");
            }
            return mapper.Map<GetCommentDto>(comment);
        }

        public async Task<IEnumerable<GetCommentDto>> GetCommentListAsync()
        {
            return mapper.Map<IEnumerable<GetCommentDto>>(await commentRepository.GetAllAsync());
        }
    }
}
