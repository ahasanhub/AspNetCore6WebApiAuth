using AspNetCore6WebApiAuth.Auth.Data.Dtos;
using AspNetCore6WebApiAuth.Auth.Data.Models;
using AspNetCore6WebApiAuth.Auth.Services.AuthService;
using AspNetCore6WebApiAuth.Auth.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace AspNetCore6WebApiAuth.Auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private readonly IAuthService _authService;        

        public AuthController(IConfiguration configuration,IUserService userService,IAuthService authService)
        {
            _configuration = configuration;
            _userService = userService;
            _authService = authService;
        }
        [HttpGet("me"),Authorize]
        public IActionResult GetMe()
        {
            //var userName = User?.Identity?.Name;
            //var userName = User.FindFirstValue(ClaimTypes.Name);
            var userName=_userService.GetMyName();
            var email = _userService.GetMyEmail();
            return Ok(new { Name=userName,Email=email});
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDto userDto)
        {            
            var user = await _authService.RegisterUser(userDto);
            return Ok(user);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto login) {            
            var response = await _authService.Login(login);
            if(response.Success)
                return Ok(response);

            return Ok(response.Message);
        }
        [HttpPost("refresh-token")]
        public async Task<ActionResult<string>> RefreshToken()
        {           
            
            var response=await _authService.RefreshToken();
            if (response.Success)
                return Ok(response);
            return Ok(response.Message);
        }
        
    }
}
