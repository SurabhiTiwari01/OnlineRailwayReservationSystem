using RailwayReservationManagementSystem.Models;

namespace RailwayReservationManagementSystem.Interfaces
{
    public interface IAdministratorRepo
    {
        Task<Administrator> GetAdministratorByUsernameAsync(string username);
        Task AddAdministratorAsync(Administrator admin);
        Task UpdateAdministratorAsync(Administrator admin);
    }
}
