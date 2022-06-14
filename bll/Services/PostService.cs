using AutoMapper;
using bll.DTO.Post;
using bll.Interfaces;
using dal.Interface;
using dal.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utils.Exceptions;

namespace bll.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository postRepository;
        private readonly UserManager<User> userManager;

        private readonly IMapper mapper;

        public PostService(IPostRepository postRepository, IMapper mapper, UserManager<User> userManager)
        {
            this.postRepository = postRepository;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        public async Task<bool> AddPostAsync(AddPostViewModel post, string userName)
        {
            var user = await userManager.FindByNameAsync(userName);
            if (user == null || post == null) {
                throw new NotFoundException("Post can't be empty");
            }

            var item = new Post()
            {
                Title = post.Title,
                Text = post.Text,
                CreatedDate = DateTime.UtcNow,
                AuthorId = user.Id,
                AuthorName = user.UserName
            };
            await postRepository.CreateAsync(item);
            return true;
        }

        public async Task DeletePostAsync(int id)
        {
            await postRepository.DeleteAsync(id);
        }

        public async Task<GetPostByIdDto> GetPostByIdAsync(int id)
        {
            var post = await postRepository.GetByIdAsync(id);
            if (post == null) {
                throw new NotFoundException(nameof(Post), id);
            }

            return mapper.Map<GetPostByIdDto>(post);
        }

        public async Task<PagedList<GetPostsDto>> GetPostListAsync(PostParameters postParameters)
        {
            var postsPaged = await postRepository.GetPostsAsync(postParameters);
            var result = new PagedList<GetPostsDto>(mapper.Map<IEnumerable<GetPostsDto>>(postsPaged.Items), postsPaged.TotalCount, postsPaged.CurrentPage, postParameters.PageSize);
            return result;
        }

        public async Task<bool> UpdatePostAsync(UpdatePostDto post)
        {
            var item = await postRepository.GetByIdAsync(post.PostId);
            if (item == null) {
                throw new NotFoundException(nameof(Post), post.PostId);
            }

            item.Title = post.Title;
            item.Text = post.Text;

            await postRepository.UpdateAsync(item);
            return true;
        }
    }
}
