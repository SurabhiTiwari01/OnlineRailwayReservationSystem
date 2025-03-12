using RailwayReservationManagementSystem.Models;

namespace RailwayReservationManagementSystem.Interfaces
{
    public interface IPaymentRepo 
    {
        Task<Payment> GetPaymentByReservationIdAsync(int reservationId);
        Task AddPaymentAsync(Payment payment);

    }
}
