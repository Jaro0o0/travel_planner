using ShareTravelPalace;
using Xunit;

public class TravelContextEngineTests
{
    //Rainy_Weather_Test
    [Fact]
    public void RainyWeatherTest()
    {
        // Arrange
        var engine = new ContextEngine();
        var place = new Place
        {
            Types = new List<string> { "park","beach","ice_cream_shop" }
        };
        var context = new TripContext
        {
            WeatherCondition = "rain"
        };

        // Act
        var score = engine.ScorePlace(place, context);

        // Assert
        Assert.True(score < 0);
    }

    //Sunny_Weather_Test
    [Fact]
    public void SunnyWeatherTest()
    {
         // Arrange
        var engine = new ContextEngine();
        var place = new Place
        {
            Types = new List<string> { "park","beach","ice_cream_shop"   }
        };
        var context = new TripContext
        {
            WeatherCondition = "sunny",
            Temperature = 30
        };

         // Act
        var score = engine.ScorePlace(place, context);

        // Assert
        Assert.True(score > 0);

    }
}