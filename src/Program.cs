using System;
using System.Drawing;
using Pastel;

namespace Mountians_Planner

{
    class Program
    {
        static void Main(){
            Point point = new Point();

            while (true)
            {
                Console.WriteLine("###Trials_planner###".Pastel(Color.Blue));
                Console.WriteLine("choose option");
                Console.WriteLine("1 Plan our track ");
                Console.WriteLine("2 Show Tracks list ");
                Console.WriteLine("3 Add friend to tracke ");
                Console.WriteLine("4 Exit");

                string choose = Console.ReadLine() ?? "";

                switch (choose)
                {
                    
                   case "1":
                    
                    Console.WriteLine("Okay set Mountain: ");
                    string MountainName = Console.ReadLine() ?? "";
                    point.SetMountain(MountainName);
                    Console.WriteLine("Mountain set: " + point.MountainName);

                   break;

                   case "2":
                   Console.WriteLine("Exit");
                   return;

                   default:
                   Console.WriteLine("Erro");
                   break;

                }
                
            }

            
               

        


        }
    
    }

    class Point
    {

        public string MountainName { get; set; } = "";

        public void SetMountain(string mountain){

            MountainName = mountain;

            

        }
        
    }

    class Travel
    {
        public string TravelName { get; set; } = "";
        public DateTime StartDate { get; set; }
        public int Days { get; set; }
        

        
    }

    

    
}
