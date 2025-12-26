
namespace BookingSystem.Api.Dto;

public class BookingDto
{
    public string? id { get; set; } = string.Empty;
    public string? name { get; set; } = string.Empty;
    public string user { get; set; } = string.Empty;
    public DateTime fromDate { get; set; }
    public DateTime toDate { get; set; }
}
