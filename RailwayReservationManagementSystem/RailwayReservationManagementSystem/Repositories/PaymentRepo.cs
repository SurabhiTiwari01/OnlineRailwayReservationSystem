using Microsoft.EntityFrameworkCore;
using RailwayReservationManagementSystem.Interfaces;
using RailwayReservationManagementSystem.Models;

namespace RailwayReservationManagementSystem.Repositories
{
    public class PaymentRepo : IPaymentRepo
    {
        private readonly RailwayReservationManagementSystemContext _context;

        public PaymentRepo(RailwayReservationManagementSystemContext context)
        {
            _context = context;
        }

        public async Task<Payment> GetPaymentByReservationIdAsync(int reservationId)
        {
            return await _context.Payments
                                 .Include(p => p.Reservation)
                                 .FirstOrDefaultAsync(p => p.ReservationId == reservationId);
        }

        public async Task AddPaymentAsync(Payment payment)
        {
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();
        }

    }
}
