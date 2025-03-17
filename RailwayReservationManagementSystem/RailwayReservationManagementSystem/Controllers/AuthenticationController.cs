using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using RailwayReservationManagementSystem.DTO;
using RailwayReservationManagementSystem.Interfaces;
using RailwayReservationManagementSystem.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RailwayReservationManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        //private readonly IAuthentication _authenticationService;
        private readonly IUser _userService;

        public AuthenticationController(IUser userService)
        {
            _userService = userService;
        }

        // POST api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var authResponse = await _userService.LoginAsync(loginRequest.Username, loginRequest.Password);
            return Ok(authResponse);
        }


        // POST api/auth/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
        {
            var user = await _userService.RegisterAsync(registerRequest.Username, registerRequest.Password, registerRequest.Role);
            if (user != null)
            {
                return Ok(new { Message = "User registered successfully!" });
            }

            return BadRequest("Registration failed");

        }

    }

}

