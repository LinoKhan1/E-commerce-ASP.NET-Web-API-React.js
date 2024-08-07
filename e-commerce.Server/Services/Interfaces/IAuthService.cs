﻿using e_commerce.Server.DTOs;
using e_commerce.Server.Models;
using Microsoft.AspNetCore.Identity;

namespace e_commerce.Server.Services.Interfaces
{
    public interface IAuthService
    {
        Task<IdentityResult> RegisterUserAsync(RegisterDTO registerDto);
        Task<string> LoginUserAsync(LoginDTO loginDto);
    }
}
