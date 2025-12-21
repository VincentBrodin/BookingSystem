using BookingSystem.Core.Services;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Tests;

public class BookingServiceTests
{
    [Fact]
    public async Task DatabaseTest()
    {
        using (var scope = new TestDbScope())
        {
            var service = new BookingService(scope.Context);
            await service.CreateRoom("TEST_ROOM");
            var savedRoom = await scope.Context.Rooms.FirstOrDefaultAsync(r => r.Name == "TEST_ROOM");
            Assert.NotNull(savedRoom);
        }
    }

    [Fact]
    public async Task CreateBookingTest()
    {
        using (var scope = new TestDbScope())
        {
            var service = new BookingService(scope.Context);
            await service.CreateRoom("TEST_ROOM");
            var room = await scope.Context.Rooms.FirstAsync();
            var booking = await service.CreateBooking(room, "User1", DateTime.Now, DateTime.Now.AddHours(2));
            Assert.NotNull(booking);
            Assert.NotNull(booking.Id);
            Assert.Equal(1, await scope.Context.Bookings.CountAsync());
        }
    }


    [Fact]
    public async Task CreateOverlapBooking()
    {
        using (var scope = new TestDbScope())
        {
            var service = new BookingService(scope.Context);
            var room = await service.CreateRoom("Tight Schedule Room");
            var pivotTime = DateTime.Now.Date.AddHours(14);
            await service.CreateBooking(room, "UserA", pivotTime.AddHours(-1), pivotTime);
            var successBooking = await service.CreateBooking(room, "UserB", pivotTime, pivotTime.AddHours(1));
            Assert.NotNull(successBooking);
            Assert.Equal(2, (await scope.Context.Bookings.CountAsync()));
        }
    }
}
