namespace TravelPlanner.Domain.Models;

public record TripContext
{
    public DateTime CurrentDateTime { get; init; } = DateTime.Now;
    public string WeatherCondition { get; set; } = string.Empty;
    public double Temperature { get; set; }

    public List<string> Interests { get; init; } = new();
    public int Budget { get; set; }
}
