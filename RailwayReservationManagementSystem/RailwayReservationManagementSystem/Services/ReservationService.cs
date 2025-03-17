using RailwayReservationManagementSystem.Interfaces;
using RailwayReservationManagementSystem.Models;

namespace RailwayReservationManagementSystem.Services
{
    public class ReservationService : IReservationRepo
    {
        private readonly IReservationRepo _reservationRepository;
        private readonly ITrainRepo _trainRepository;

        public ReservationService(IReservationRepo reservationRepository, ITrainRepo trainRepository)
        {
            _reservationRepository = reservationRepository;
            _trainRepository = trainRepository;
        }

        public async Task<Reservation> GetReservationByPNRAsync(string pnr)
        {
            return await _reservationRepository.GetReservationByPNRAsync(pnr);
        }

        public async Task<IEnumerable<Reservation>> GetReservationsByPassengerAsync(int passengerId)
        {
            return await _reservationRepository.GetReservationsByPassengerAsync(passengerId);
        }

        public async Task AddReservationAsync(Reservation reservation)
        {
            // Check if train has available seats (business logic)
            var train = await _trainRepository.GetTrainByIdAsync(reservation.TrainId);
            if (train == null)
            {
                throw new ArgumentException("Train not found.");
            }

            // Proceed with reservation
            await _reservationRepository.AddReservationAsync(reservation);
        }

        public async Task UpdateReservationAsync(Reservation reservation)
        {
            await _reservationRepository.UpdateReservationAsync(reservation);
        }

        public async Task CancelReservationAsync(string pnr)
        {
            await _reservationRepository.CancelReservationAsync(pnr);
        }
    }
}
