using GraphQLProject.Models;

namespace GraphQLProject.Interfaces
{
    public interface IReservationRepository
    {
        Task<List<Reservation>> GetAllReservations();
        Task<Reservation> GetReservation(int id);
        Task<Reservation> AddReservation(Reservation reservation);
        Task<Reservation> UpdateReservation(Reservation reservation);
        Task<Reservation?> DeleteReservation(int id);
    }
}
