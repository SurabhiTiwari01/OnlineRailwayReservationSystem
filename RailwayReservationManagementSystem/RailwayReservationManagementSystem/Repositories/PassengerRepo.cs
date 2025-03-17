using Microsoft.EntityFrameworkCore;
using RailwayReservationManagementSystem.Interfaces;
using RailwayReservationManagementSystem.Models;

namespace RailwayReservationManagementSystem.Repositories
{
    
        public class PassengerRepo : IPassengerRepo
        {
        private readonly RailwayReservationManagementSystemContext _context;

        public PassengerRepo(RailwayReservationManagementSystemContext context)
        {
            _context = context;
        }

        public async Task<Passenger> GetPassengerByIdAsync(int id)
        {
            return await _context.Passengers.FindAsync(id);
        }

        public async Task<IEnumerable<Passenger>> GetAllPassengersAsync()
        {
            return await _context.Passengers.ToListAsync();
        }

        public async Task AddPassengerAsync(Passenger passenger)
        {
            _context.Passengers.Add(passenger);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePassengerAsync(Passenger passenger)
        {
            _context.Passengers.Update(passenger);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePassengerAsync(int id)
        {
            var passenger = await _context.Passengers.FindAsync(id);
            if (passenger != null)
            {
                _context.Passengers.Remove(passenger);
                await _context.SaveChangesAsync();
            }
        }

    }
    }
