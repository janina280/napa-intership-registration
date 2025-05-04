using DatabaseLayout.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DatabaseLayout.Context;

public interface IPortTrackerContext
{
    DbSet<Ship> Ships { get; set; }
    DbSet<Port> Ports { get; set; }
    DbSet<Voyage> Voyages { get; set; }
    Task<int> SaveChangesAsync();
}
