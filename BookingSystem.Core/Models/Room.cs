using System.ComponentModel.DataAnnotations;

namespace BookingSystem.Core.Models;

public class Room
{
    [Key]
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public List<Booking> Bookings { get; set; } = [];

    public Room(string name)
    {
        Id = Guid.NewGuid().ToString();
        Name = name;
    }

    public bool IsAvailable(DateTime fromDate, DateTime toDate)
    {
        return !Bookings.Any(b => fromDate < b.ToDate && toDate > b.FromDate);
    }

    public override String ToString()
    {
        return $"[{Id}] {Name}";
    }
}
