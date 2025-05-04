using System;
using System.Collections.Generic;

namespace DatabaseLayout.Models;

public class Ship
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public double MaximumSpeed { get; set; }

    public ICollection<Voyage> Voyages { get; set; } = new List<Voyage>();
}