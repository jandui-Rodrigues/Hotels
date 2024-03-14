using TrybeHotel.Models;
using TrybeHotel.Dto;

namespace TrybeHotel.Repository
{
    public class CityRepository : ICityRepository
    {
        protected readonly ITrybeHotelContext _context;
        public CityRepository(ITrybeHotelContext context)
        {
            _context = context;
        }

        public IEnumerable<CityDto> GetCities()
        {
           var cities = from city in _context.Cities
                        select new CityDto {
                            CityId = city.CityId,
                            Name = city.Name,
                            State = city.State
                        };
            return cities;
        }

        public CityDto AddCity(City city)
        {
            _context.Cities.Add(city);
            _context.SaveChanges();
            return new CityDto {
                CityId = city.CityId,
                Name = city.Name,
                State = city.State
            };
        }

        public CityDto UpdateCity(City city)
        {
            var cityToUpdate = _context.Cities.Find(city.CityId) ?? throw new Exception("City not found");

            cityToUpdate.Name = city.Name;
            cityToUpdate.State = city.State;
            _context.SaveChanges();
            return new CityDto {
                CityId = cityToUpdate.CityId,
                Name = cityToUpdate.Name,
                State = cityToUpdate.State
            };
        }

    }
}