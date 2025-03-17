using RailwayReservationManagementSystem.Interfaces;
using RailwayReservationManagementSystem.Models;

namespace RailwayReservationManagementSystem.Services
{
    public class PassengerService : IPassengerRepo
    {
        private readonly IPassengerRepo _passengerRepository;

        public PassengerService(IPassengerRepo passengerRepository)
        {
            _passengerRepository = passengerRepository;
        }

        public async Task<Passenger> GetPassengerByIdAsync(int id)
        {
            return await _passengerRepository.GetPassengerByIdAsync(id);
        }

        public async Task<IEnumerable<Passenger>> GetAllPassengersAsync()
        {
            return await _passengerRepository.GetAllPassengersAsync();
        }

        public async Task AddPassengerAsync(Passenger passenger)
        {
            // Add validation logic before adding
            if (string.IsNullOrWhiteSpace(passenger.Name))
            {
                throw new ArgumentException("Passenger name is required.");
            }

            await _passengerRepository.AddPassengerAsync(passenger);
        }

        public async Task UpdatePassengerAsync(Passenger passenger)
        {
            await _passengerRepository.UpdatePassengerAsync(passenger);
        }

        public async Task DeletePassengerAsync(int id)
        {
            await _passengerRepository.DeletePassengerAsync(id);
        }
    }
}
