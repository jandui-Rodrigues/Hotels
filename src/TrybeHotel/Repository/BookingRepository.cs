using TrybeHotel.Models;
using TrybeHotel.Dto;

namespace TrybeHotel.Repository
{
    public class BookingRepository : IBookingRepository
    {
        protected readonly ITrybeHotelContext _context;
        public BookingRepository(ITrybeHotelContext context)
        {
            _context = context;
        }

        // 9. Refatore o endpoint POST /booking
        public BookingResponse Add(BookingDtoInsert booking, string email)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email) ?? throw new Exception("User not found");
            var room = GetRoomById(booking.RoomId);
            var bookingNewEntity = new Booking {
                UserId = user.UserId,
                RoomId = room.RoomId,
                CheckIn = booking.CheckIn,
                CheckOut = booking.CheckOut,
                GuestQuant = booking.GuestQuant
            };
            var bookingResponse =_context.Bookings.Add(bookingNewEntity);
            _context.SaveChanges();

            return new BookingResponse {
                BookingId = bookingResponse.Entity.BookingId,
                CheckIn = bookingNewEntity.CheckIn,
                CheckOut = bookingNewEntity.CheckOut,
                GuestQuant = bookingNewEntity.GuestQuant,
                Room = new RoomDto {
                    RoomId = room.RoomId,
                    Capacity = room.Capacity,
                    Image = room.Image,
                    Name = room.Name,
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
                }
            };
        }

        // 10. Refatore o endpoint GET /booking
        public BookingResponse GetBooking(int bookingId, string email)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email) ?? throw new Exception("User not found");
            var booking = _context.Bookings.FirstOrDefault(b => b.BookingId == bookingId) ?? throw new Exception("Booking not found");

            if (booking.UserId != user.UserId) throw new UnauthorizedAccessException("User not allowed to access this booking");

            var room = GetRoomById(booking.RoomId);
            var hotel = (from h in _context.Hotels
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
                         }).FirstOrDefault();
            return new BookingResponse {
                BookingId = booking.BookingId,
                CheckIn = booking.CheckIn,
                CheckOut = booking.CheckOut,
                GuestQuant = booking.GuestQuant,
                Room = new RoomDto {
                    RoomId = room.RoomId,
                    Capacity = room.Capacity,
                    Image = room.Image,
                    Name = room.Name,
                    Hotel = hotel
                }
            };
        }

        public Room GetRoomById(int RoomId)
        {
             var room = _context.Rooms.FirstOrDefault(r => r.RoomId == RoomId) ?? throw new Exception("Room not found");
             return room;
        }

    }

}