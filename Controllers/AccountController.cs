using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using resful_project.DTO.Account;
using resful_project.Interfaces;
using resful_project.models;
using resful_project.Services;

namespace resful_project.Controllers
{
    [Route("api/account")]
    [ApiController]

    public class AccountController : ControllerBase
    {
        private readonly ITokenService _tokenservice;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        public AccountController(UserManager<AppUser> userManager, ITokenService tokenservice, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _tokenservice = tokenservice;
            _signInManager = signInManager;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerdto)
        {
            NewUserDTO response = new();
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var appUser = new AppUser()
                {
                    UserName = registerdto.Username,
                    Email = registerdto.Email,
                };
                var createdUser = await _userManager.CreateAsync(appUser, registerdto.Password);
                if (createdUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
                    if (roleResult.Succeeded)
                    {
                        response.Email = appUser.Email;
                        response.UserName = appUser.UserName;
                        response.Token = _tokenservice.CreateToken(appUser);
                        return Ok(response);
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
            }
            catch (Exception exeption)
            {
                return StatusCode(500, exeption);
            }

        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginrequestdto)
        {
            NewUserDTO response = new();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginrequestdto.Username.ToLower());
            if (user == null) return BadRequest("User Not Found!");

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginrequestdto.Password, false);
            if (!result.Succeeded) return Unauthorized("Invalid Username or password");
            response.Email = user.Email;
            response.UserName = user.UserName;
            response.Token = _tokenservice.CreateToken(user);
            return Ok(response);
        }
    }
}