using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Collections.Generic;
using Pastel;
using ShareTravelPalace;
using Services;
using Microsoft.Data.Sqlite;

// using CreateBackpack;

namespace TravelPlanner
{
    class Program
    {

        // diaspley equipment
        static void Main()
        {
            while (true)
            {
                Console.WriteLine("###Trials_planner###".Pastel(Color.Blue));
                Console.WriteLine("choose option");
                Console.WriteLine("1 Add new trip ");
                //   Console.WriteLine("1 Show planp ");
                //    Console.WriteLine("1 edit plan ");
                //    Console.WriteLine("1 remove ");
                //    budget
                //    caache
                //    sync
                //    suggest
                //    Polly

                Console.WriteLine("2 Show Tracks list ");
                Console.WriteLine("4 Exit");
                Console.WriteLine("5: plan your equipment");

                string choose = Console.ReadLine() ?? "";

                switch (choose)
                {
                    case "1":
                        Console.WriteLine("CASE: 1");
                        Task.Run(async () => await AddNewTravel.AddNewTrip());
                       
                        break;

                     case "2":
                    {
                        TracksList.ShowTracksList();
                        break;
                    }
                      
                    
                
                    
                    case "3":
                        Console.WriteLine("Plan your equipment");
                        break;

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
