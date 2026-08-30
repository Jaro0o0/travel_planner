namespace TravelPlanner.Domain.Models;

public record BackPackContext
{
    public DateTime CurrentDateTime { get; init; } = DateTime.Now;
    public string WeatherCondition { get; set; } = string.Empty;
    public double Temperature { get; set; }
}
