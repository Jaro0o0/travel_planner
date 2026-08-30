namespace TravelPlanner.Domain.Models;

public class BackpackContextEngine
{
    public List<string> GetRecommendedItems(BackPackContext context)
    {
        List<string> recommendedItems = new List<string>();
        string weatherCondition = context.WeatherCondition.Trim().ToLower();

        if (weatherCondition.Contains("rain"))
        {
            recommendedItems.Add("rain jacket");
            recommendedItems.Add("umbrella");
            recommendedItems.Add("waterproof boots");
        }

        if (context.Temperature <= 10)
        {
            recommendedItems.Add("hoodie");
            recommendedItems.Add("warm jacket");
        }

        if (context.Temperature >= 25)
        {
            recommendedItems.Add("water bottle");
            recommendedItems.Add("sunscreen");
            recommendedItems.Add("cap");
        }

        return recommendedItems.Distinct().ToList();
    }
}
