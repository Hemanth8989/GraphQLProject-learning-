using System.Linq;
using GraphQLProject.Data;
using GraphQLProject.Interfaces;
using GraphQLProject.Models;
using Microsoft.EntityFrameworkCore;

namespace GraphQLProject.Services
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly GraphqlDbContext _dbContext;

        public ReservationRepository(GraphqlDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Reservation> AddReservation(Reservation reservation)
        {
            _dbContext.Reservations.Add(reservation);
            await _dbContext.SaveChangesAsync();
            return reservation;
        }

        public async Task DeleteReservation(int id)
        {
            var existing = _dbContext.Reservations.Find(id);
            if (existing == null)
                return;

            _dbContext.Reservations.Remove(existing);
            _dbContext.SaveChanges();
        }

        public async Task<List<Reservation>> GetAllReservations()
        {
            return await _dbContext.Reservations.ToListAsync();
        }

        public async Task<Reservation> GetReservation(int id)
        {
            return await _dbContext.Reservations.FindAsync(id);
        }

        public async Task<Reservation> UpdateReservation(Reservation reservation)
        {
            var existing = await _dbContext.Reservations.FindAsync(reservation.Id);
            if (existing == null)
                return null;

            existing.CustomerName = reservation.CustomerName;
            existing.Email = reservation.Email;
            existing.PhoneNumber = reservation.PhoneNumber;
            existing.PartySize = reservation.PartySize;
            existing.SpecialRequests = reservation.SpecialRequests;
            existing.ReservationDate = reservation.ReservationDate;

            _dbContext.SaveChanges();
            return existing;
        }
    }
}
