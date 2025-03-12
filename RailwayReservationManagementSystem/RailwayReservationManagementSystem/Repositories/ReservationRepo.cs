using Microsoft.EntityFrameworkCore;
using RailwayReservationManagementSystem.Interfaces;
using RailwayReservationManagementSystem.Models;

namespace RailwayReservationManagementSystem.Repositories
{
    public class ReservationRepo : IReservationRepo
    {
        private readonly RailwayReservationManagementSystemContext _context;

        public ReservationRepo(RailwayReservationManagementSystemContext context)
        {
            _context = context;
        }

        public async Task<Reservation> GetReservationByPNRAsync(string pnr)
        {
            return await _context.Reservations
                                 .Include(r => r.Passenger)
                                 .Include(r => r.Train)
                                 .FirstOrDefaultAsync(r => r.Pnrnumber == pnr);
        }

        public async Task<IEnumerable<Reservation>> GetReservationsByPassengerAsync(int passengerId)
        {
            return await _context.Reservations
                                 .Where(r => r.PassengerId == passengerId)
                                 .Include(r => r.Passenger)
                                 .Include(r => r.Train)
                                 .ToListAsync();
        }

        public async Task AddReservationAsync(Reservation reservation)
        {
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateReservationAsync(Reservation reservation)
        {
            _context.Reservations.Update(reservation);
            await _context.SaveChangesAsync();
        }

        public async Task CancelReservationAsync(string pnr)
        {
            var reservation = await GetReservationByPNRAsync(pnr);
            if (reservation != null)
            {
                reservation.Status = "Cancelled";
                _context.Reservations.Update(reservation);
                await _context.SaveChangesAsync();
            }
        }

    }
}
