using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class BookingRepository
    {
        private readonly FuminiHotelManagementContext _db;

        public BookingRepository()
        {
            _db = new FuminiHotelManagementContext();
        }

        public List<BookingReservation> GetBookingsByCustomerId(int customerId)
        {
            return _db.BookingReservations
                      .Where(b => b.CustomerId == customerId)
                      .Include(b => b.BookingDetails)
                        .ThenInclude(d => d.Room) // Bao gồm thông tin phòng
                      .ToList();
        }

        public List<BookingReservation> GetAll()
        {
            return _db.BookingReservations
                           .Include(b => b.Customer)
                           .Include(b => b.BookingDetails)
                               .ThenInclude(d => d.Room)
                           .ToList();
        }

        public BookingReservation? GetById(int id)
        {
            return _db.BookingReservations
                           .Include(b => b.Customer)
                           .Include(b => b.BookingDetails)
                               .ThenInclude(d => d.Room)
                           .FirstOrDefault(b => b.BookingReservationId == id);
        }

        public void Add(BookingReservation booking)
        {
            _db.BookingReservations.Add(booking);
            _db.SaveChanges();
        }

        public void Update(BookingReservation booking)
        {
            _db.BookingReservations.Update(booking);
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            var booking = _db.BookingReservations
                                  .Include(b => b.BookingDetails)
                                  .FirstOrDefault(b => b.BookingReservationId == id);

            if (booking != null)
            {
                _db.BookingDetails.RemoveRange(booking.BookingDetails);
                _db.BookingReservations.Remove(booking);
                _db.SaveChanges();
            }
        }

        public void AddBookingWithDetails(BookingReservation booking)
        {
            _db.BookingReservations.Add(booking);
            _db.SaveChanges();
        }
    }
}
