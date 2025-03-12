using RailwayReservationManagementSystem.Models;

namespace RailwayReservationManagementSystem.Interfaces
{
    public interface ITrainRepo
    {
        Task<IEnumerable<Train>> GetAllTrainsAsync();
        Task<Train> GetTrainByIdAsync(int id);
        Task AddTrainAsync(Train train);
        Task UpdateTrainAsync(Train train);
        Task DeleteTrainAsync(int id);

    }
}
