using System;
using System.Drawing;
using System.Threading.Tasks;
using Pastel;
using TravelPlanner.Interfaces;
using  TravelPlanner.Interfaces;
// using CreateBackpack;

namespace TravelPlanner
{
    class Program
    {
        static async Task Main()
        {
            while (true)
            {
                Console.WriteLine("###Trials_planner###".Pastel(Color.Blue));
                Console.WriteLine("choose option");
                Console.WriteLine("1 Add new trip ");
                Console.WriteLine("2 Show Tracks list ");
                Console.WriteLine("4 Exit");
                Console.WriteLine("5: plan your equipment");

                string choose = Console.ReadLine() ?? "";

                switch (choose)
                {
                    case "1":
                

                        Console.WriteLine("choose your kind of travel");
                        Console.WriteLine("1: Standard travel");
                        Console.WriteLine("2: Moluntain Teavel");

                        string travelKind = Console.ReadLine()?.Trim() ?? "";

                        switch (travelKind)
                        {
                            case "1":
                                Console.WriteLine("Where you wanto to travel");
                                string travelPlace = Console.ReadLine()?.Trim() ?? "";
                                //Travel place info
                                if (!string.IsNullOrWhiteSpace(travelPlace))
                                {
                                   await ShareTravelPalace.PlacesService.PlacesInfo(travelPlace);
                                   Console.WriteLine("Do you want to add this place to your trip? (y/n)");
                                   if (Console.ReadLine().Trim().ToLower() == "y")
                                   {
                                       
                                       
                                   }
                                }
                                break;
                        }
                        break;

                    case "2":
                        Console.WriteLine("Exit");
                        return;
                    
                    case "3":

                    case "4":
                       return;
                        


                    default:
                        Console.WriteLine("Erro");
                        break;

                    // case "5":
                    //     Backpack.SelectSize();
                    //     Console.WriteLine("");
                    //     break;
                }
            }
        }
    }
}
