using Microsoft.AspNetCore.Mvc;
using LogiTrack.IdentityService.DTOs;
using LogiTrack.IdentityService.Services;

namespace LogiTrack.IdentityService.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        public AuthController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequestDTO request)
        {
            if (request.Email == "manager@test.com" && request.Password == "1234")
            {
                var token = _tokenService.GenerateToken("1", request.Email, "Manager");

                return Ok(new { access_token = token });
            }

            if (request.Email == "driver@test.com" && request.Password == "1234")
            {
                var token = _tokenService.GenerateToken("2", request.Email, "Driver");

                return Ok(new { access_token = token });
            }

            return Unauthorized("Invalid credentials");
        }
    }
}