using System;

namespace DatabaseLayout.Models;

public class Voyage
{
    public int Id { get; set; }
    public DateTime VoyageDate { get; set; }

    public int ShipId { get; set; }
    public Ship Ship { get; set; } = null!;

    public int DeparturePortId { get; set; }
    public Port DeparturePort { get; set; } = null!;

    public int ArrivalPortId { get; set; }
    public Port ArrivalPort { get; set; } = null!;

    public DateTime VoyageStart { get; set; }
    public DateTime VoyageEnd { get; set; }
}
