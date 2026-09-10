namespace TravelPlanner.Domain.Models{

    public class DisplayName
    {
        public string? Text { get; set; }
    }

    public class Location
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

    public class Place
    {
        public string? Id { get; set; }
        public DisplayName? DisplayName { get; set; }
        public string? FormattedAddress { get; set; }
        public Location? Location { get; set; }
        public List<string>? Types { get; set; }
        public string? PrimaryType { get; set; }
    }


}
