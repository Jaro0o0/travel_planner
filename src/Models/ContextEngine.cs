using Services;
using ShareTravelPalace;

public class ContextEngine
{
    public ScorePlace(InterestToChoose googleTypes , context)
    {

       
        int score = 0;
         //Weather

        //Regex z deszczem
        if (context.Weather == "moderate rain")
        {
            //In rain
            if(googleTypes.Contains("museum","art_gallery","cafe", "coffee_shop"))
            {
                score += 20;
            }
            else
            {
                score -= 50;
            }
        }
        // WeatherAPi and Temp
        if (context.Weather == "sunnt" && context.Temperature > 25)
        {
            if (place.Types.Contains("park") || place.Types.Contains("beach") || place.Types.Contains("ice_cream_shop"))
                score += 15;
        }

        //Time of the day
        if(context.CurrentDataTime.Hour < 11) //morning
        {
             if (place.Types.Contains("cafe") || place.Types.Contains("bakery"))
                score += 15;
        } 

        //User preferences 
         if (context.Preferences.Interests.Contains(place.MainCategory))
            score += 30;
        
        return score;
        
    }
}