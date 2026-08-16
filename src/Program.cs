using System;
using System.Drawing;
using Pastel;
using CreateBackpack;

namespace Mountians_Planner

{
    class Program
    {
        static void Main(){
        

            while (true)
            {
                Console.WriteLine("###Trials_planner###".Pastel(Color.Blue));
                Console.WriteLine("choose option");
                Console.WriteLine("1 Add new trip ");
                Console.WriteLine("2 Show Tracks list ");
                Console.WriteLine("3 Add friend to tracke ");
                Console.WriteLine("4 Exit");
                Console.WriteLine("5: plan your equipment");

                string choose = Console.ReadLine() ?? "";

                switch (choose)
                {
                    
                   case "1":
                    
                    Console.WriteLine("Okay set Mountain: ");
                    string MountainName = Console.ReadLine() ?? "";
                    // point.SetMountain(MountainName);
                    // Console.WriteLine("Mountain set: " + point.MountainName);

                    Console.WriteLine('choose your kind of travel');
                    Console.WriteLine('1: Standard travel');
                    Console.WriteLine('2: Moluntain Teavel');

                    string TravelKind = Console.ReadLine().Trim();

                        switch (TravelKind)
                        {
                            case "1":
                                Console.WriteLine("Where you wanto to travel");
                                string TeavelPalce = Console.ReadLine().Trim();
                                




                            


                        }

                   break;

                   case "2":
                   Console.WriteLine("Exit");
                   return;

                   default:
                   Console.WriteLine("Erro");
                   break;

                   case "5":
                    Backpack.SelectSize();
                    Console.WriteLine("");
                    break;

                }
                
            }

            
               

        


        }
    
    }

//     class Point
//     {

//         public string MountainName { get; set; } = "";

//         public void SetMountain(string mountain){

//             MountainName = mountain;

            

//         }

        
        
//     }



    

    
// }
