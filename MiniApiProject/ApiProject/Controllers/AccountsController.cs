using ApiProject.Business.DTO.AccountDtos;
using ApiProject.Core.Entites;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;

        public AccountsController(UserManager<User> userManager,
                                  RoleManager<IdentityRole> roleManager,
                                  SignInManager<User> signInManager,
                                  IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }
        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Register([FromForm] RegisterDto registerDto)
        {
            User user = null;

            user = await _userManager.FindByNameAsync(registerDto.UserName);

            if (user is not null) return StatusCode(400, "bu user movcuddur!");


            user = await _userManager.FindByEmailAsync(registerDto.Email);

            if (user is not null) return StatusCode(400, "bu emailli user movcuddur!");

            user = new User
            {
                FullName = registerDto.FullName,
                UserName = registerDto.UserName,
                Email = registerDto.Email,
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded) return StatusCode(400, "register prossesinde problem yasandi!");

            var addedRole = await _userManager.AddToRoleAsync(user, "User");

            return Ok("User Register oldu");
        }

        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Login([FromForm] LoginDto loginDto)
        {
            User user = null;

            user = await _userManager.FindByNameAsync(loginDto.UsernameOrEmail);

            if (user is null)
            {
                user = await _userManager.FindByEmailAsync(loginDto.UsernameOrEmail);

                if (user is null) return StatusCode(400, "Invalid Credentials!");
            }

            var result = await _signInManager.PasswordSignInAsync(user, loginDto.Password, true, false);

            if (!result.Succeeded) return StatusCode(400, "login prossesinde problem yasandi!");

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


            return Ok(token);
        }

        #region roleAndAdmin

        //[HttpGet("[action]")]
        //public async Task<IActionResult> CreateAdmin()
        //{
        //    User admin = new User
        //    {
        //        FullName = "Elvin Sarkarov",
        //        UserName = "SuperElvin"
        //    };

        //    var result = await _userManager.CreateAsync(admin, "#Admin1234");
        //    var addedRole = await _userManager.AddToRoleAsync(admin, "SuperAdmin");

        //    return Ok(addedRole);
        //}

        //[HttpGet("[action]")]
        //public async Task<IActionResult> CreateRole()
        //{
        //    var role1 = new IdentityRole("SuperAdmin");
        //    var role2 = new IdentityRole("Admin");
        //    var role3 = new IdentityRole("User");

        //    await _roleManager.CreateAsync(role1);
        //    await _roleManager.CreateAsync(role2);
        //    await _roleManager.CreateAsync(role3);

        //    return Ok("yarandi");
        //}
        #endregion
    }
}
