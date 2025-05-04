using System.Threading.Tasks;
using DatabaseLayout.Models;
using Microsoft.EntityFrameworkCore;

namespace DatabaseLayout.Context;

public class PortTrackerContext : DbContext, IPortTrackerContext
{
    public PortTrackerContext(DbContextOptions options)
        : base(options) { }

    public DbSet<Ship> Ships { get; set; }
    public DbSet<Port> Ports { get; set; }
    public DbSet<Voyage> Voyages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Voyage>()
            .HasOne(v => v.Ship)
            .WithMany(s => s.Voyages)
            .HasForeignKey(v => v.ShipId);

        modelBuilder.Entity<Voyage>()
            .HasOne(v => v.DeparturePort)
            .WithMany(p => p.DepartingVoyages)
            .HasForeignKey(v => v.DeparturePortId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Voyage>()
            .HasOne(v => v.ArrivalPort)
            .WithMany(p => p.ArrivingVoyages)
            .HasForeignKey(v => v.ArrivalPortId)
            .OnDelete(DeleteBehavior.Restrict);
    }
    public async Task<int> SaveChangesAsync()
    {
        return await base.SaveChangesAsync();
    }
}
