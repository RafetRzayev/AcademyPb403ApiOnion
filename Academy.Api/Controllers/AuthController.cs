using Academy.Application.Dtos.AuthenticationDtos;
using Academy.Application.Services.Interfaces;
using Academy.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Academy.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly UserManager<AppUser> _userManager;

        public AuthController(UserManager<AppUser> userManager, IAuthService authService)
        {
            _userManager = userManager;
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody]LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.Username);
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                return Unauthorized("Invalid username or password.");
            }
            var roles = await _userManager.GetRolesAsync(user);
            var jwtRequestModel = new JwtRequestModel
            {
                Username = user.UserName ?? throw new Exception("username not found"),
                Email = "sd",// user.Email ?? throw new Exception("email not found"),
                Roles = roles.ToList()
            };
            var token = await _authService.CreateToken(jwtRequestModel);
            
            return Ok(token);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(string username, string password)
        {
            var user = new AppUser { UserName = username };
            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            return Ok("User registered successfully.");
        }
    }
}
