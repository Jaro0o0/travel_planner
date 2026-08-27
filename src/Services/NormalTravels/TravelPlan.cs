using System.Net.Http;
using using System.Threading.Tasks;
using System.Text;
using System.Text.Json;

namespace Services
{

    public class Clothes {
        public bool Hoodie {get; set;}
        public string Boots {get; set;}
        public string Tshirt {get; set;}

    }

    public class Attractions
    {
        
    }

    public class  TravelPlan
    {
        public string  TravelLocation {get; set;}
        public var Weather {get; set;}
        public vat Backpack {get; set;}
        

        public TravelPlan( string travelLocation, var Weather, var backPack){

            TravelLocatio = TravelLocatio;
            Weather = Weather;
            Backpack = backPack;


            // Clothes Object
            Clothes clothes = new Clothes();

            List<Clothes> ClothingList = new List<Clothes>();    

        }

        public void GeneratePlan(){

            if(Weather.Temperature <= 15 ){
                clothes.Hoodie = true;
                clothes.Boots = "Recomend higher boots"



            }

            Attractions attractions = new Attractions();

           


    
    }

    public async  Task GetAtractions(){
        Attractions attractions = new Attractions();
        string url = $"https://places.googleapis.com/v1/places/{PLACE_ID}"

        using HttpClient client  = new HttpClinet();

        try
        {
            var response  =  await client.GetAsync(url);
            var data  = await response.Content.ReadFromJsonAsync();
        }
        catch (System.Exception)
        {
            
            throw;
        }

        


    }
}

