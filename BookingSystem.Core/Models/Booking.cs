using System.ComponentModel.DataAnnotations;

namespace BookingSystem.Core.Models;

public class Booking
{
    [Key]
    public string Id { get; set; } = string.Empty;
    public string User { get; set; } = string.Empty;
    public string RoomId { get; set; } = string.Empty;
    public DateTime FromDate { get; set; } = DateTime.Now;
    public DateTime ToDate { get; set; } = DateTime.Now;

    public Booking(string roomId, string user, DateTime fromDate, DateTime toDate)
    {
        Id = Guid.NewGuid().ToString();
        User = user;
        RoomId = roomId;
        FromDate = fromDate;
        ToDate = toDate;
    }

    public override string ToString()
    {
        var now = DateTime.Now;
        var timespan = now - FromDate;
        if (timespan.Hours > 24)
        {
            return $"[{User}] {FromDate.ToShortDateString} {FromDate.Hour}:{FromDate.Minute}:{FromDate.Second} to {ToDate.ToShortDateString} {ToDate.Hour}:{ToDate.Minute}:{ToDate.Second}";
        }
        else
        {
            return $"[{User}] {FromDate.Hour}:{FromDate.Minute}:{FromDate.Second} to {ToDate.Hour}:{ToDate.Minute}:{ToDate.Second}";
        }
    }
}
