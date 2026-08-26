namespace Services
{
    public interface ITrip
    {
        void ShowWeather();
    }
    public class TripFactory
    {
         public ITrip Create( string type)
        {
            if(type == "normal")
            {
                
            }

            if(type == "mountain")
            {
                
            }

            throw new ArgumentException("Unknown travel type");
        }
    }
}