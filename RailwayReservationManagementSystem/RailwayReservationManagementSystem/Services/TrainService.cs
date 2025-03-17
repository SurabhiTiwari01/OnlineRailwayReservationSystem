using RailwayReservationManagementSystem.Interfaces;
using RailwayReservationManagementSystem.Models;

namespace RailwayReservationManagementSystem.Services
{
    public class TrainService : ITrainRepo
    {
        private readonly ITrainRepo _trainRepository;

        public TrainService(ITrainRepo trainRepository)
        {
            _trainRepository = trainRepository;
        }

        public async Task<Train> GetTrainByIdAsync(int id)
        {
            return await _trainRepository.GetTrainByIdAsync(id);
        }

        public async Task<IEnumerable<Train>> GetAllTrainsAsync()
        {
            return await _trainRepository.GetAllTrainsAsync();
        }

        public async Task AddTrainAsync(Train train)
        {
            // Business logic: Ensure train doesn't conflict with an existing schedule.
            if (train.DepartureTime <= train.ArrivalTime)
            {
                throw new ArgumentException("Departure time must be later than arrival time.");
            }

            await _trainRepository.AddTrainAsync(train);
        }

        public async Task UpdateTrainAsync(Train train)
        {
            await _trainRepository.UpdateTrainAsync(train);
        }

        public async Task DeleteTrainAsync(int id)
        {
            await _trainRepository.DeleteTrainAsync(id);
        }
    }
}
