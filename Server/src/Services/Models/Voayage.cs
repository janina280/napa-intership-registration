using System;

namespace Services.Models;

public class Voyage
{
    public int Id { get; set; }
    public DateTime VoyageDate { get; set; }
    
    public Ship Ship { get; set; } = null!;
    
    public Port DeparturePort { get; set; } = null!;
    
    public Port ArrivalPort { get; set; } = null!;

    public DateTime VoyageStart { get; set; }
    public DateTime VoyageEnd { get; set; }
}
