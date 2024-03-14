using TrybeHotel.Models;
using TrybeHotel.Dto;
using Microsoft.EntityFrameworkCore;

namespace TrybeHotel.Repository
{
    public class HotelRepository : IHotelRepository
    {
        protected readonly ITrybeHotelContext _context;
        public HotelRepository(ITrybeHotelContext context)
        {
            _context = context;
        }

        public IEnumerable<HotelDto> GetHotels()
        {
            var hotelResponse = from hotel in _context.Hotels
                                join city in _context.Cities on hotel.CityId equals city.CityId
                                select new HotelDto
                                {
                                    HotelId = hotel.HotelId,
                                    Name = hotel.Name,
                                    Address = hotel.Address,
                                    CityId = hotel.CityId,
                                    CityName = city.Name,
                                    State = city.State
                                };
            return hotelResponse;
        }

        // 6. Refatore o endpoint POST /hotel
        public HotelDto AddHotel(Hotel hotel)
        {
            _context.Hotels.Add(hotel);
            _context.SaveChanges();
            var hotelResponse = (from city in _context.Cities
                                where city.CityId == hotel.CityId
                                select new HotelDto
                                {
                                    HotelId = hotel.HotelId,
                                    Name = hotel.Name,
                                    Address = hotel.Address,
                                    CityId = hotel.CityId,
                                    CityName = city.Name,
                                    State = city.State
                                }).FirstOrDefault() ?? throw new Exception("Erro ao procurar por cidade do hotel");
            return hotelResponse;
        }
    }
}