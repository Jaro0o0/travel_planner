
using System.Collections;

namespace Services
{
    public class Trip : ISTrip
{
    public int Id { get; private set; }
    public string Destination { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }

    public ISEquipment Equipment {get; private set;}


    public Trip(int id, string destination, DateTime startDate, DateTime endDate, ISEquipment? equipment = null)
    {
        Id = id;
        Destination = destination;
        StartDate = startDate;
        EndDate = endDate;
        Equipment = equipment;
    }

    public void WeatherForecast()
        {
            Console.WriteLine($"Weather forecast for {Destination} from {StartDate.ToShortDateString()} to {EndDate.ToShortDateString()}:");
            // Here you can implement the logic to fetch and display the weather forecast for the trip destination and dates.
        }

}
}

