using AutoMapper;
using bll.DTO.User;
using bll.Interfaces;
using dal.Context;
using dal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace bll.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly IConfiguration configuration;
        private readonly IMapper mapper;

        public AccountService(UserManager<User> userManager, RoleManager<ApplicationRole> roleManager, IConfiguration configuration, IMapper mapper)
        {
            this.configuration = configuration;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.mapper = mapper;
        }
        public async Task<string> Login(LoginModel userLogin)
        {
            var user = await userManager.FindByNameAsync(userLogin.UserName);
            var checkPassword = await userManager.CheckPasswordAsync(user, userLogin.Password);

            if (user == null || !checkPassword || (DateTime.Now < user.LockoutEnd)) throw new Exception();

            var userRoles = await userManager.GetRolesAsync(user);
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserName)
            };
            authClaims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

            var token = GetJwtSecurityToken(authClaims);

            return token;

        }

        public async Task<UserDto> Register(RegisterModel userRegister)
        {
            var userExists = await userManager.FindByNameAsync(userRegister.UserName);
            if (userExists != null) {
                throw new Exception("User name is already exist");
            }
            User user = new()
            {
                FirstName = userRegister.FirstName,
                LastName = userRegister.LastName,
                DateOfBirth = userRegister.DateOfBirth,
                UserName = userRegister.UserName,
                Password = userRegister.Password,
            };
            var result = await userManager.CreateAsync(user, userRegister.Password);
            if (!result.Succeeded) {
                throw new Exception("User isn't created");
            }

            await userManager.AddToRoleAsync(user, UserRoles.User);
            return mapper.Map<User, UserDto>(user);
        }

        public async Task<UserDto> RegisterAdmin(RegisterModel userRegister)
        {
            var userExists = await userManager.FindByNameAsync(userRegister.UserName);
            if (userExists != null) {
                throw new Exception("User name is already exist");
            }
            User user = new()
            {
                FirstName = userRegister.FirstName,
                LastName = userRegister.LastName,
                DateOfBirth = userRegister.DateOfBirth,
                UserName = userRegister.UserName,
                Password = userRegister.Password
            };
            var result = await userManager.CreateAsync(user, userRegister.Password);
            if (!result.Succeeded) {
                throw new Exception("User isn't created");
            }

            if (!await roleManager.RoleExistsAsync(UserRoles.Admin) ||
                !await roleManager.RoleExistsAsync(UserRoles.User))
            {
                await roleManager.CreateAsync(new ApplicationRole(UserRoles.Admin));
                await roleManager.CreateAsync(new ApplicationRole(UserRoles.User));
            }

            await userManager.AddToRoleAsync(user, UserRoles.Admin);
            return mapper.Map<User, UserDto>(user);
        }

        public string GetJwtSecurityToken(List<Claim> authClaims)
        {
            var authSigninKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]));
            var token = new JwtSecurityToken(
                issuer: configuration["JWT:Issuer"],
                audience: configuration["JWT:Audience"],
                expires: DateTime.Now.AddMinutes(30),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
