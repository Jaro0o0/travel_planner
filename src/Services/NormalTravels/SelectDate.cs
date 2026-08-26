using System;
using System.Drawing;
using System.Net.Http;
using System.Threading.Tasks;
using Pastel;

namespace Date;

public static class DateManager
{
    public static void ChooseDate()
    {
        while (true)
        {
            Console.WriteLine("Choose date for your trip: ".Pastel(Color.Blue));
            string? dateString = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(dateString))
            {
                Console.WriteLine("Wrong date");
                continue;
            }

            if (DateTime.TryParse(dateString, out DateTime travelDate))
            {
                Console.WriteLine($"Your travel date is {travelDate.ToShortDateString()}");
                break;
            }

            Console.WriteLine("Wrong date");
        }
    }

    public static async Task GetWeatherForecast()
    {
        using HttpClient client = new HttpClient();
        await Task.CompletedTask;
    }
}