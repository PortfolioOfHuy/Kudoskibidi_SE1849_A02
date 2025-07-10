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
    }
}
