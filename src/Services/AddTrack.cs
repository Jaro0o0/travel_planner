using System.Collections.Generic;

public class TripService
{
    private readonly List<string> TripsNames = new();

    public string SetTripName { get; private set; } = string.Empty;

    public void AddTrip(string tripName)
    {
        if (string.IsNullOrWhiteSpace(tripName))
            return;

        TripsNames.Add(tripName);
        SetTripName = tripName;
    }
}