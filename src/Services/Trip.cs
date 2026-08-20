
using System.Collections;

namespace Services
{
    public class Trip : ISTrip
{
    public int Id { get; private set; }
    public string Destination { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }

    public ISEquipment Equipment {get; private set;}

    public Trip(int id, string destination, DateTime startDate, DateTime endDate, IsEquipment equipment)
    {
        Id = id;
        Destination = destination;
        StartDate = startDate;
        EndDate = endDate;
        Equipment = equipment;
    }

}
}

