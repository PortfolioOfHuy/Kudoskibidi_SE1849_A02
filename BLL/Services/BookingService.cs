using DAL.Entities;
using DAL.Repositories;

namespace BLL.Services
{
    public class BookingService
    {
        private readonly BookingRepository _bookingRepository;

        public BookingService()
        {
            _bookingRepository = new BookingRepository();
        }

        public List<BookingReservation> GetBookingsByCustomerId(int customerId)
        {
            return _bookingRepository.GetBookingsByCustomerId(customerId);
        }

        public List<BookingReservation> GetAllBookings()
        {
            return _bookingRepository.GetAll();
        }

        public BookingReservation? GetBookingById(int id)
        {
            return _bookingRepository.GetById(id);
        }

        public void AddBooking(BookingReservation booking)
        {
            _bookingRepository.AddBookingWithDetails(booking);
        }

        public void UpdateBooking(BookingReservation booking)
        {
            _bookingRepository.Update(booking);
        }

        public void DeleteBooking(int id)
        {
            _bookingRepository.Delete(id);
        }
    }
}
