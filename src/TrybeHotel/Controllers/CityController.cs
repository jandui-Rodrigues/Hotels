using Microsoft.AspNetCore.Mvc;
using TrybeHotel.Models;
using TrybeHotel.Repository;

namespace TrybeHotel.Controllers
{
    [ApiController]
    [Route("city")]
    public class CityController : Controller
    {
        private readonly ICityRepository _repository;
        public CityController(ICityRepository repository)
        {
            _repository = repository;
        }
        
        [HttpGet]
        public IActionResult GetCities(){
            try
            {
                var cities = _repository.GetCities();
                return Ok(cities);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult PostCity([FromBody] City city){
            try
            {
                var cityCreated = _repository.AddCity(city);
                return Created("", cityCreated);
            }
            catch (Exception)
            {
                
                return BadRequest();
            }
        }
        
        [HttpPut]
        public IActionResult PutCity([FromBody] City city){
            try{
                var cityUpdated = _repository.UpdateCity(city);
                return Ok(cityUpdated);
            }
            catch (Exception){
                return BadRequest();
            }
        }
    }
}