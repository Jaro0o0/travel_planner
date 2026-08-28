namespace Services
{
    

public enum InterestToChoose{
            Museums,
            ArtGalleries,
            Historical,
            Churches,
            Restaurants,
            Cafes,
            Bars,
            Parks,
            Viewpoints,
            Shopping
                    
        }

public class InterestMapper
{
    private static readonly Dictionary<InterestToChoose , string[]>_map = new()
    {
        [InterestToChoose.Museums] = new[] { "museum" },
        [InterestToChoose.ArtGalleries] = new[] { "art_gallery" },
        [InterestToChoose.Historical] = new[] { "historical_landmark", "monument", "historical_place" },
        [InterestToChoose.Cafes] = new[] { "cafe", "coffee_shop" },
        [InterestToChoose.Parks] = new[] { "park", "garden" },
        [InterestToChoose.Viewpoints] = new[] { "tourist_attraction", "observation_deck" }
    };

    public static string[] ToGoogleTypes(InterestToChoose interest) => _map[interest];
}

}
