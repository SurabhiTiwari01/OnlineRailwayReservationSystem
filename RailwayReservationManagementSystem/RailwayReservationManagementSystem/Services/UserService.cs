using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RailwayReservationManagementSystem.DTO;
using RailwayReservationManagementSystem.Interfaces;
using RailwayReservationManagementSystem.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace RailwayReservationManagementSystem.Services
{
    public class UserService : IUser
    {
        private readonly RailwayReservationManagementSystemContext _context; // Assuming you're using EF Core for DB access
        private readonly IConfiguration _configuration;

        public UserService(RailwayReservationManagementSystemContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<User> RegisterAsync(string username, string password, string role)
        {
            var userObj = _context.Users.FirstOrDefault(u => u.Username == username);
            //return userObj;
            if (userObj == null)
            {// Hash the password
             var salt = GenerateSalt();
                var passwordHash = HashPassword(password, salt);
                var user = new User
                {
                    UserId =1,
                    Username = username,
                    PasswordHash = password,
                    Role = role // Default role, can be customized
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return user;
            }

            return null;
        }

        public async Task<AuthResponse> LoginAsync(string username, string password)
        {

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);

            if (user == null)
                throw new Exception("Invalid username or password.");

            // Validate password
            //if (!VerifyPassword(password, user.PasswordHash))
            if(password != user.PasswordHash)
                throw new Exception("Invalid username or password.");

            // Generate JWT Token
            var token = GenerateJwtToken(user);

            return new AuthResponse
            {
                Token = token,
                Username = user.Username
            };
        }

        private string GenerateSalt()
        {
            var salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return Convert.ToBase64String(salt);
        }

        private string HashPassword(string password, string salt)
        {
            var saltBytes = Convert.FromBase64String(salt);
            var hash = KeyDerivation.Pbkdf2(
                password: password,
                salt: saltBytes,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8
            );

            return Convert.ToBase64String(hash);
        }

        private bool VerifyPassword(string password, string storedHash)
        {
            var storedHashBytes = Convert.FromBase64String(storedHash);
            var salt = storedHashBytes.Take(128 / 8).ToArray();
            var computedHash = HashPassword(password, Convert.ToBase64String(salt));

            return storedHash == computedHash;
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role),
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
