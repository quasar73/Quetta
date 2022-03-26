﻿using Common.DTO;
using Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost("AuthenticateWithGoogle")]
        public async Task<IActionResult> AuthenticateWithGoogle(GoogleUserDto googleUser)
        {
            var token = await authService.AuthenticateGoogleUserAsync(googleUser);

            if (token == null)
            {
                return Unauthorized();
            }

            return Ok(token);
        }
    }
}
