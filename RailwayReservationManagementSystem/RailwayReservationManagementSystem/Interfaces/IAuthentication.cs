namespace RailwayReservationManagementSystem.Interfaces
{
    public interface IAuthentication
    {
        Task<string> AuthenticateAsync(string username, string password);
    }
}
