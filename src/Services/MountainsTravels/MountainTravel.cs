using System;

namespace Services
{
    public class MountainTravel : Travel
    {
        private string mountain;
        public MountainTravel(string place, DateTime date, string equipment) : base(place, date, equipment)
        {
            
        }
       

        public override void ShowWeather()
        {
           Console.WriteLine($"{mountain}");
           
        }

    }
}