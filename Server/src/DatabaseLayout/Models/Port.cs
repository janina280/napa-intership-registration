using System.Collections.Generic;

namespace DatabaseLayout.Models;

public class Port
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;

    public ICollection<Voyage> DepartingVoyages { get; set; } = new List<Voyage>();
    public ICollection<Voyage> ArrivingVoyages { get; set; } = new List<Voyage>();
}
