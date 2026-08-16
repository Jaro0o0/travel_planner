using System;
using System.Drawing;
using Spectre.Console;
using Pastel;

using System.Net.Http;
using System.Threading.Tasks;

namespace Date;

// na pdostwaie daty pogoda
public static class DateManager {

    public static void ChooseDate(){

        while (true) {
            Console.WriteLine("Choose date for your trip: ".Pastel(Color.Blue));
            string dateString Console.ReadLine();

            if(DateTime.TryParse(dateString, out DateTime TravelDate))
            {
                Console.WriteLine($" Your travel date is {TravelDate.ToShortDateString()}")
                break;
            }
            else
            {
                Console.WriteLine('Wrong date')
            }

     

      

        }
    }

    static async GetWeatherForecast()
    {
        using (HttpClient client = new HttpClient())

        try
            {
                
            }
    

}