using RailwayReservationManagementSystem.Interfaces;
using RailwayReservationManagementSystem.Models;

namespace RailwayReservationManagementSystem.Services
{
    public class AdministratorService :IAdministratorRepo
    {
        private readonly IAdministratorRepo _administratorRepository;

        public AdministratorService(IAdministratorRepo administratorRepository)
        {
            _administratorRepository = administratorRepository;
        }

        public async Task<Administrator> GetAdministratorByUsernameAsync(string username)
        {
            return await _administratorRepository.GetAdministratorByUsernameAsync(username);
        }

        public async Task AddAdministratorAsync(Administrator administrator)
        {
            if (string.IsNullOrEmpty(administrator.Username))
            {
                throw new ArgumentException("Username cannot be empty.");
            }

            // Password hashing or validation would typically be here
            await _administratorRepository.AddAdministratorAsync(administrator);
        }

        public async Task UpdateAdministratorAsync(Administrator administrator)
        {
            await _administratorRepository.UpdateAdministratorAsync(administrator);
        }
    }
}
