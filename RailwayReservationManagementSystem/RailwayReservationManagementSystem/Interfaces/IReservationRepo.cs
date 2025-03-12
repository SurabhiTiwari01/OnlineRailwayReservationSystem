using RailwayReservationManagementSystem.Models;

namespace RailwayReservationManagementSystem.Interfaces
{
    public interface IReservationRepo
    {
        Task<Reservation> GetReservationByPNRAsync(string pnr);
        Task<IEnumerable<Reservation>> GetReservationsByPassengerAsync(int passengerId);
        Task AddReservationAsync(Reservation reservation);
        Task UpdateReservationAsync(Reservation reservation);
        Task CancelReservationAsync(string pnr);

    }
}
