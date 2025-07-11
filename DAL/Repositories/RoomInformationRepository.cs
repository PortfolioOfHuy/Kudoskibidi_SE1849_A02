using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class RoomInformationRepository
    {
        private readonly FuminiHotelManagementContext _db;
        public RoomInformationRepository()
        {
            _db = new FuminiHotelManagementContext();
        }

        public void AddRoom(RoomInformation roomInformation)
        {
            _db.Add(roomInformation);
            _db.SaveChanges();
        }

        public void DeleteRoom(int id)
        {
            try
            {
                var room = _db.RoomInformations
                              .FirstOrDefault(r => r.RoomId == id);

                if (room == null)
                {
                    Console.WriteLine($"Room with ID {id} not found.");
                    return;
                }

                // Kiểm tra có BookingDetail liên quan không
                bool hasBookings = room.BookingDetails.Any();

                if (hasBookings)
                {
                    // Có Booking -> chỉ đổi RoomStatus về Inactive (0)
                    room.RoomStatus = 0; // Ví dụ: 1 = Active, 0 = Inactive
                    _db.RoomInformations.Update(room);
                    Console.WriteLine($"Room ID {id} has bookings. Status set to Inactive.");
                }
                else
                {
                    // Không có Booking -> xoá khỏi DB
                    _db.RoomInformations.Remove(room);
                    Console.WriteLine($"Room ID {id} deleted permanently.");
                }

                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in DeleteRoom: {ex.Message}");
            }
        }

        public List<RoomInformation> GetAllRoomInformations()
        {
            try
            {
                return _db.RoomInformations.Include(x => x.RoomType).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetAllRoomInformations: {ex.Message}");
                return new List<RoomInformation>();
            }
        }

        public List<RoomInformation> SearchRoom(string keyword)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(keyword))
                {
                    return _db.RoomInformations.ToList();
                }
                return _db.RoomInformations
                    .Where(r =>
                        (r.RoomNumber != null && r.RoomNumber.Contains(keyword, StringComparison.OrdinalIgnoreCase)) ||
                        (r.RoomDetailDescription != null && r.RoomDetailDescription.Contains(keyword, StringComparison.OrdinalIgnoreCase)))
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in SearchRoom: {ex.Message}");
                return new List<RoomInformation>();
            }
        }

        public void UpdateRoom(RoomInformation updatedRoom)
        {
            try
            {
                var existingRoom = _db.RoomInformations.FirstOrDefault(r => r.RoomId == updatedRoom.RoomId);
                if (existingRoom == null)
                {
                    throw new Exception("Room not found");
                }

                existingRoom.RoomNumber = updatedRoom.RoomNumber;
                existingRoom.RoomDetailDescription = updatedRoom.RoomDetailDescription;
                existingRoom.RoomMaxCapacity = updatedRoom.RoomMaxCapacity;
                existingRoom.RoomTypeId = updatedRoom.RoomTypeId;
                existingRoom.RoomStatus = updatedRoom.RoomStatus;
                existingRoom.RoomPricePerDay = updatedRoom.RoomPricePerDay;

                _db.RoomInformations.Update(existingRoom);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateRoom: {ex.Message}");
            }
        }
    }
}
