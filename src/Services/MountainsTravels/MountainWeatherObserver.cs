using System.Data;
using Microsoft.VisualBasic;

namespace Services
{
    public class MountainWeatherObserver : IWeatherStation
    {
        public void Update(float temperature)
        {
            Console.WriteLine($"Temperature: {temperature}");
        }
    }
}