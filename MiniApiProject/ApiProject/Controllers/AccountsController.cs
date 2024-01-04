using ApiProject.Business.CustomExceptions.UserExceptions;
using ApiProject.Business.DTO.AccountDtos;
using ApiProject.Business.Services;
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
        private readonly IAccountService _accountService;

        public AccountsController(IAccountService accountService)
        {    
            _accountService = accountService;
        }
        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Register([FromForm] RegisterDto registerDto)
        {
            try
            {
                await _accountService.RegisterAsync(registerDto);
            }
            catch (ExistUserException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidRegisterException ex)
            {
                return BadRequest(ex.Message);
            }


            return Ok("User Register oldu");
        }

        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Login([FromForm] LoginDto loginDto)
        {
            string token = String.Empty;
            try
            {
                token = await _accountService.LoginAsync(loginDto);
            }
            catch (InvalidCredentialsException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidLoginException ex)
            {
                return BadRequest(ex.Message);
            }

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
