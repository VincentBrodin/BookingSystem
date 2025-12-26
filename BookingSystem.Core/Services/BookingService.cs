using BookingSystem.Core.Data;
using BookingSystem.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Core.Services;

public class BookingService(BookingDbContext context)
{
    public async Task<Room> CreateRoom(string name)
    {
        var room = new Room(name);
        await context.Rooms.AddAsync(room);
        await context.SaveChangesAsync();
        return room;
    }

    public async Task<Room?> GetRoom(string id)
    {
        var room = await context.Rooms.Include(r => r.Bookings).FirstOrDefaultAsync(r => r.Id == id);
        return room;
    }


    public async Task<Room?> GetRoomByName(string name)
    {
        var room = await context.Rooms.Include(r => r.Bookings).FirstOrDefaultAsync(r => r.Name == name);
        return room;
    }

    public async Task<List<Room>> GetRooms()
    {
        var rooms = await context.Rooms.Include(r => r.Bookings).ToListAsync();
        return rooms;
    }


    public async Task<Booking> CreateBooking(Room room, string user, DateTime fromDate, DateTime toDate)
    {
        if (!room.IsAvailable(fromDate, toDate))
        {
            throw new Exception("Room is booked");
        }

        var booking = new Booking(room.Id, user, fromDate, toDate);
        await context.Bookings.AddAsync(booking);
        await context.SaveChangesAsync();
        return booking;
    }
}
