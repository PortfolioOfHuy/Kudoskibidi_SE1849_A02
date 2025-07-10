using DAL.Entities;
using DAL.Repositories;

namespace BLL.Services
{
    public class RoomTypeService
    {
        private readonly RoomTypeRepository _roomTypeRepository;

        public RoomTypeService()
        {
            _roomTypeRepository = new RoomTypeRepository();
        }

        public List<RoomType> GetAllRoomTypes()
        {
            return _roomTypeRepository.GetAllRoomTypes();
        }
    }
}
