using System



namespace Services

{
    public interface ISEquipment
    {
        void DisplayuEquipment();
    }

    public class Equipment :  ISEquipment
    {
        public ISTrip Place {get; set;}
        public string BackPack {get; set;}

        

        public Equipment(ISTrip place, string backPack ){

            Place = place;
            BackPack = backPack;



            
        }

        public void DisplayEquipment()
        {
            Console.WriteLine("1: Show Backpack: ");
            string UserOption = Console.ReadLine()?.Trim().ToLower() ?? "";

            //Show  Backpack
            foreach ( var item in BackPack )
            {
                Console.WriteLine(item);

            }


            


        }



        
    }

}

