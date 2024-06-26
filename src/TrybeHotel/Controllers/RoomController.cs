using Microsoft.AspNetCore.Mvc;
using TrybeHotel.Models;
using TrybeHotel.Repository;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace TrybeHotel.Controllers
{
    [ApiController]
    [Route("room")]
    public class RoomController : Controller
    {
        private readonly IRoomRepository _repository;
        public RoomController(IRoomRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{HotelId}")]
        public IActionResult GetRoom(int HotelId){
            try
            {
                var rooms = _repository.GetRooms(HotelId);
                return Ok(rooms);
            } catch
            {
                return BadRequest("Erro ao buscar quartos");
            }
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Policy = "Admin")]
        public IActionResult PostRoom([FromBody] Room room){
            try
            {
                var roomReponse = _repository.AddRoom(room);
                return Created("", roomReponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{RoomId}")]
        public IActionResult Delete(int RoomId)
        {
             try
             {
                _repository.DeleteRoom(RoomId);
                return NoContent();
             } catch (Exception ex)
             {
                 return BadRequest(ex.Message);
             }
        }
    }
}