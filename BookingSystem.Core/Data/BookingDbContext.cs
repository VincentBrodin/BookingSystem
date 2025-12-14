
using BookingSystem.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Core.Data;

public class BookingDbContext : DbContext
{
    public BookingDbContext()
    {
    }

    public BookingDbContext(DbContextOptions<BookingDbContext> options) : base(options)
    {
    }

    public DbSet<Room> Rooms {get; set;}
    public DbSet<Booking> Bookings {get; set;}
}
