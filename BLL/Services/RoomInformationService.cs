using DAL.Entities;
using DAL.Repositories;

namespace BLL.Services
{
    public class RoomInformationService
    {
        private readonly RoomInformationRepository _roomInformationRepository;

        public RoomInformationService()
        {
            _roomInformationRepository = new RoomInformationRepository();
        }

        public List<RoomInformation> GetAllRoomInformations()
        {
            return _roomInformationRepository.GetAllRoomInformations();
        }
        public List<RoomInformation> SearchRoom(string keyword)
        {
            return _roomInformationRepository.SearchRoom(keyword);
        }
        public void AddRoom(RoomInformation roomInformation)
        {
            _roomInformationRepository.AddRoom(roomInformation);
        }
        public void DeleteRoom(int id)
        {
            _roomInformationRepository.DeleteRoom(id);
        }
    }
}
