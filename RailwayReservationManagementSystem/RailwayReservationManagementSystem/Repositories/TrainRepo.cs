using Microsoft.EntityFrameworkCore;
using RailwayReservationManagementSystem.Interfaces;
using RailwayReservationManagementSystem.Models;

namespace RailwayReservationManagementSystem.Repositories
{
    public class TrainRepo : ITrainRepo
    {
        private readonly RailwayReservationManagementSystemContext _context;

        public TrainRepo(RailwayReservationManagementSystemContext context)
        {
            _context = context;
        }

        //Get all trains
        public async Task<IEnumerable<Train>> GetAllTrainsAsync()
        {
            return await _context.Trains.ToListAsync();
        }
        public async Task<Train> GetTrainByIdAsync(int id)
        {
            return await _context.Trains.FindAsync(id);
        }
        public async Task AddTrainAsync(Train train)
        {
            _context.Trains.Add(train);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTrainAsync(Train train)
        {
            _context.Trains.Update(train);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteTrainAsync(int id)
        {
            var train = await _context.Trains.FindAsync(id);
            if (train != null)
            {
                _context.Trains.Remove(train);
                await _context.SaveChangesAsync();
            }
        }

    }
}
