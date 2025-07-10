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
    }
}
