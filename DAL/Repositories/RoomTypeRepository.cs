using DAL.Entities;

namespace DAL.Repositories
{
    public class RoomTypeRepository
    {
        private readonly FuminiHotelManagementContext _db;

        public RoomTypeRepository()
        {
            _db = new FuminiHotelManagementContext();
        }

        public List<RoomType> GetAllRoomTypes()
        {
            return _db.RoomTypes.ToList();
        }
    }
}
