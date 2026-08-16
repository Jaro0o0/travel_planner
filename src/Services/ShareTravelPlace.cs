using System;
using System.Threading.Tasks;
using System.Net.Http;

class ShareTravelPlace
{
    static void Main()
    {
        
    }

    static async Task SharePalce()
    {
        using HttpClient client = new HttpClient();

        string PlaceId = 
        
        try
        {
            string url = " https://places.googleapis.com/v1/places/{place_id}";
            HttpResponseMessage response = await client.GetAsync()
        }


        
    }
}