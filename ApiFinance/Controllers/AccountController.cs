﻿using ApiFinance.Dtos.Account;
using ApiFinance.Models;
using ApiFinance.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiFinance.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signInManager;


        public AccountController(UserManager<AppUser> userManager, ITokenService tokenService, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var appUser = new AppUser
                { 
                    UserName = registerDto.Username,
                    Email = registerDto.Email,
                };
                // "username": "user001",
                //  "email": "user@example.com",
                //  "password": "Qwerty23_"
                var createdUser = await _userManager.CreateAsync(appUser, registerDto.Password);

                if (createdUser.Succeeded) {
                    var roleResult = await _userManager.AddToRoleAsync(appUser, "USER");
                    if (roleResult.Succeeded)
                    {
                        return Ok(new NewUserDto
                        {
                            UserName = appUser.UserName,
                            Email = appUser.Email,
                            Token = _tokenService.CreateToken(appUser)
                        }) ;
                    }
                    else
                    {
                        return StatusCode(500, roleResult.Errors);
                    }
                }
                else
                {
                    return StatusCode(500, createdUser.Errors);
                }
            } catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost("login")] 
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
              //"username": "investor567",
              //"email": "investor567@example.com",
              //"password": "Qwerty_12"
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.Users.FirstOrDefaultAsync(x=> x.UserName == loginDto.Username);

            if (user == null) return Unauthorized("Invalid username");

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded) return Unauthorized("Username not found and/or password incorrect");

            return Ok(new NewUserDto
            {
                UserName = user.UserName,
                Email = user.Email,
                Token= _tokenService.CreateToken(user)
            });
        }
    }
}
