using ApiProject.Business.DTO.AccountDtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiProject.Business.Services
{
    public interface IAccountService
    {
        Task RegisterAsync([FromForm] RegisterDto registerDto);
        Task<string> LoginAsync([FromForm] LoginDto loginDto);
    }
}
