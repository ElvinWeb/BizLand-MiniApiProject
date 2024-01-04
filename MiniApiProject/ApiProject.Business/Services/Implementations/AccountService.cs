using ApiProject.Business.CustomExceptions.UserExceptions;
using ApiProject.Business.DTO.AccountDtos;
using ApiProject.Core.Entites;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ApiProject.Business.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;

        public AccountService(UserManager<User> userManager,
                              RoleManager<IdentityRole> roleManager,
                              SignInManager<User> signInManager,
                              IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }
        public async Task<string> LoginAsync([FromForm] LoginDto loginDto)
        {
            User user = null;

            user = await _userManager.FindByNameAsync(loginDto.UsernameOrEmail);

            if (user is null)
            {
                user = await _userManager.FindByEmailAsync(loginDto.UsernameOrEmail);

                if (user is null) throw new InvalidCredentialsException("Invalid Credentials!");
            }

            var result = await _signInManager.PasswordSignInAsync(user, loginDto.Password, true, false);

            if (!result.Succeeded) throw new InvalidLoginException("error occure during the Login process!");

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim("FullName", user.FullName),
                new Claim(ClaimTypes.Name, user.UserName)
            };
            var roles = await _userManager.GetRolesAsync(user);

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var symmetricSecurutyKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("JWT:Key").Value));
            var signInCreds = new SigningCredentials(symmetricSecurutyKey, SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(
            audience: _configuration.GetSection("JWT:Audience").Value,
            issuer: _configuration.GetSection("JWT:Issuer").Value,
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: signInCreds);

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return token;
        }

        public async Task RegisterAsync([FromForm] RegisterDto registerDto)
        {
            User user = null;

            user = await _userManager.FindByNameAsync(registerDto.UserName);

            if (user is not null) throw new ExistUserException("user already exists!");


            user = await _userManager.FindByEmailAsync(registerDto.Email);

            if (user is not null) throw new ExistUserException("user already exists!");

            user = new User
            {
                FullName = registerDto.FullName,
                UserName = registerDto.UserName,
                Email = registerDto.Email,
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded) throw new InvalidRegisterException("error occure during the Register process!");

            var addedRole = await _userManager.AddToRoleAsync(user, "User");
        }
    }
}
