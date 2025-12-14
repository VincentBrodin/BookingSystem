using BookingSystem.Core.Models;

namespace BookingSystem.Tests;

public class BookingUnitTest
{
    [Fact]
    public void BothDateMiss()
    {
        var now = DateTime.Now;
        var room = new Room("TEST_ROOM");
        room.Bookings.Add(new Booking(room.Id, "TEST_USER", now.AddHours(1), now.AddHours(1).AddMinutes(30)));
        Assert.True(room.IsAvailable(now, now.AddMinutes(30)));
    }

    [Fact]
    public void OneDateHit()
    {
        var now = DateTime.Now;
        var room = new Room("TEST_ROOM");
        room.Bookings.Add(new Booking(room.Id, "TEST_USER", now.AddHours(1), now.AddHours(1).AddMinutes(30)));
        Assert.False(room.IsAvailable(now, now.AddHours(1).AddMinutes(15)));
    }

    [Fact]
    public void BothDateHit()
    {
        var now = DateTime.Now;
        var room = new Room("TEST_ROOM");
        room.Bookings.Add(new Booking(room.Id, "TEST_USER", now.AddHours(1), now.AddHours(1).AddMinutes(30)));
        Assert.False(room.IsAvailable(now.AddHours(1).AddMinutes(5), now.AddHours(1).AddMinutes(15)));
    }


    [Fact]
    public void OneDateMiss()
    {
        var now = DateTime.Now;
        var room = new Room("TEST_ROOM");
        room.Bookings.Add(new Booking(room.Id, "TEST_USER", now.AddHours(1), now.AddHours(1).AddMinutes(30)));
        Assert.False(room.IsAvailable(now.AddHours(1).AddMinutes(5), now.AddHours(1).AddMinutes(45)));
    }
}
