
using BookingSystem.Api.Dto;
using BookingSystem.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.Api.Controllers;


[ApiController]
[Route("[Controller]")]
public class BookingController(BookingService service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] BookingDto bookingDto)
    {
        var id = bookingDto.id ?? bookingDto.name;
        Console.WriteLine(id);
        if (string.IsNullOrEmpty(id))
        {
            return BadRequest("Needs id OR name");
        }

        var room = await service.GetRoom(id) ?? await service.GetRoomByName(id);
        if (room == null)
        {
            return BadRequest($"No room with identifier: {id}");
        }

        if (!room.IsAvailable(bookingDto.fromDate, bookingDto.toDate))
        {
            return BadRequest("Room is not available that date");
        }
        var booking = await service.CreateBooking(room, bookingDto.user, bookingDto.fromDate, bookingDto.toDate);
        return Ok(booking);
    }
}
