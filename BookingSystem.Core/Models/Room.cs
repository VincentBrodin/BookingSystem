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
        foreach (var booking in Bookings)
        {
            if ((fromDate > booking.FromDate || toDate > booking.FromDate) && (fromDate < booking.ToDate || toDate < booking.ToDate))
            {
                return false;
            }
        }
        return true;
    }

    public override String ToString()
    {
        return $"[{Id}] {Name}";
    }
}
