using RailwayReservationManagementSystem.Models;

namespace RailwayReservationManagementSystem.Interfaces
{
    public interface IPassengerRepo
    {
        Task<Passenger> GetPassengerByIdAsync(int id);
        Task<IEnumerable<Passenger>> GetAllPassengersAsync();
        Task AddPassengerAsync(Passenger passenger);
        Task UpdatePassengerAsync(Passenger passenger);
        Task DeletePassengerAsync(int id);

    }
}
