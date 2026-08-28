using Services;
using ShareTravelPalace;

public class ContextEngine
{
    public int ScorePlace(Place place, TripContext context)
    {
        int score = 0;
        List<string> types = place.Types ?? new List<string>();

         //Weather

        //Regex z deszczem
        if (context.WeatherCondition == "moderate rain")
        {
            //In rain
            if (types.Contains("museum") || types.Contains("art_gallery") ||
                types.Contains("cafe") || types.Contains("coffee_shop"))
            {
                score += 20;
            }
            else
            {
                score -= 50;
            }
        }
        // WeatherAPi and Temp
        if (context.WeatherCondition == "sunny" && context.Temperature > 25)
        {
            if (types.Contains("park") || types.Contains("beach") || types.Contains("ice_cream_shop"))
                score += 15;
        }

        //Time of the day
        if (context.CurrentDateTime.Hour < 11) //morning
        {
             if (types.Contains("cafe") || types.Contains("bakery"))
                score += 15;
        } 

        //User preferences 
         if (!string.IsNullOrWhiteSpace(place.PrimaryType) &&
             context.Interests.Contains(place.PrimaryType))
            score += 30;
        
        return score;
        
    }

    public void ScoreBackpack(Place place, TripContext context)
    {
        List<string> types = place.Types ?? new List<string>();

        int score = 0;
        
        if(context.WeatherCondition == " moderate rain")
        {
            score += 15;
        }

    }
}
