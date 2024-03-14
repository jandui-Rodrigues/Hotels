using TrybeHotel.Models;
using TrybeHotel.Dto;

namespace TrybeHotel.Repository
{
    public class RoomRepository : IRoomRepository
    {
        protected readonly ITrybeHotelContext _context;
        public RoomRepository(ITrybeHotelContext context)
        {
            _context = context;
        }

        // 7. Refatore o endpoint GET /room
        public IEnumerable<RoomDto> GetRooms(int HotelId)
        {
           var rooms = from r in _context.Rooms
                       where r.HotelId == HotelId
                       select new RoomDto
                       {
                           RoomId = r.RoomId,
                           Name = r.Name,
                           Capacity = r.Capacity,
                           Image = r.Image,
                           Hotel = (from h in _context.Hotels
                                    join c in _context.Cities on h.CityId equals c.CityId
                                    where h.HotelId == r.HotelId
                                    select new HotelDto
                                    {
                                        HotelId = h.HotelId,
                                        Name = h.Name,
                                        Address = h.Address,
                                        CityId = h.CityId,
                                        CityName = c.Name,
                                        State = c.State
                                    }).FirstOrDefault()
                       };
            return rooms;
        }

        // 8. Refatore o endpoint POST /room
        public RoomDto AddRoom(Room room) {

            _context.Rooms.Add(room);
            _context.SaveChanges();
            return new RoomDto {
                RoomId = room.RoomId,
                Name = room.Name,
                Capacity = room.Capacity,
                Image = room.Image,
                Hotel = (from h in _context.Hotels
                         join c in _context.Cities on h.CityId equals c.CityId
                         where h.HotelId == room.HotelId
                         select new HotelDto
                         {
                             HotelId = h.HotelId,
                             Name = h.Name,
                             Address = h.Address,
                             CityId = h.CityId,
                             CityName = c.Name,
                             State = c.State
                         }).FirstOrDefault()
            };
        }

        public void DeleteRoom(int RoomId) {
           throw new NotImplementedException();
        }
    }
}