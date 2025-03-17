using RailwayReservationManagementSystem.DTO;
using RailwayReservationManagementSystem.Models;

namespace RailwayReservationManagementSystem.Interfaces
{
    public interface IUser
    {
        Task<User> RegisterAsync(string username, string password, string role);
        Task<AuthResponse> LoginAsync(string username, string password);

    }
}
