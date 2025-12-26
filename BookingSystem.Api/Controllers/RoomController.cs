
using BookingSystem.Api.Dto;
using BookingSystem.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.Api.Controllers;


[ApiController]
[Route("[Controller]")]
public class RoomController(BookingService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] string id)
    {
        var room = await service.GetRoom(id) ?? await service.GetRoomByName(id);
        if (room == null)
        {
            return BadRequest($"No room with id {id}");
        }
        else
        {
            return Ok(room);
        }
    }

    [HttpGet("/[Controller]s")]
    public async Task<IActionResult> GetAll()
    {
        var rooms = await service.GetRooms();
        return Ok(rooms);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] RoomDto roomDto)
    {
        var room = await service.CreateRoom(roomDto.Name);
        return Ok(room);
    }
}
