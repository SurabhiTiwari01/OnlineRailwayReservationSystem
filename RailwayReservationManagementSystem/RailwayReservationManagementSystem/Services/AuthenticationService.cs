using RailwayReservationManagementSystem.Interfaces;
using RailwayReservationManagementSystem.Repositories;

namespace RailwayReservationManagementSystem.Services
{
    public class AuthenticationService : IAuthentication
    {
        private readonly JwtTokenHelper _jwtTokenHelper;

        public AuthenticationService(JwtTokenHelper jwtTokenHelper)
        {
            _jwtTokenHelper = jwtTokenHelper;
        }

        public async Task<string> AuthenticateAsync(string username, string password)
        {
            // Example: You would typically check the username and password against a database here.
            // For simplicity, we're just checking hardcoded values.

            if (username == "username" && password == "password")
            {
                // If valid, generate a JWT token
                return _jwtTokenHelper.GenerateJwtToken(username);
            }

            throw new UnauthorizedAccessException("Invalid credentials.");
        }
    }
}
