using AutoMapper;
using bll.DTO.User;
using bll.Interfaces;
using dal.Interface;
using dal.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utils.Helpers;

namespace bll.Services
{
    public class UserService : IUserService
    {

        private readonly IMapper mapper;
        private readonly IRepository<User> userRepository;

        public UserService(IRepository<User> userRepository, IMapper mapper)
        {
            this.mapper = mapper;
            this.userRepository = userRepository;
        }

        public async Task<IEnumerable<UserDto>> GetUserListAsync()
        {
            return mapper.Map<IEnumerable<UserDto>>(await userRepository.GetAll());
        }

        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            var user = await userRepository.GetByIdAsync(id);
            if (user == null) {
                throw new Exception("User not found");
            }
            return mapper.Map<User, UserDto>(user);
        }

        public async Task<bool> UpdateUserAsync(UpdateUserDto user)
        {
            var item = await userRepository.GetByIdAsync(user.Id);
            if (item == null) {
                throw new Exception("User not found");
            }
            item.FirstName = user.FirstName;
            item.LastName = user.LastName;
            item.DateOfBirth = user.DateOfBirth;
            item.UserName = user.UserName;
            item.Password = PasswordHelper.HashPassword(user.Password);

            await userRepository.UpdateAsync(item);
            return true;
        }

        public async Task DeleteUserAsync(int id)
        {
            await userRepository.DeleteAsync(id);
        }

        public async Task<bool> BlockUserAsync(int id, int hours)
        {
            var item = await userRepository.GetByIdAsync(id);
            if (item == null) {
                throw new Exception("User not found");
            }
            item.LockoutEnd = DateTimeOffset.Now.AddHours(hours);
            await userRepository.UpdateAsync(item);
            return true;
        }
    }
}
