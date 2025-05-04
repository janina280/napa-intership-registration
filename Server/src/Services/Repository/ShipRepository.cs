using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DatabaseLayout.Context;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;

namespace Services.Repository;

public class ShipRepository : IShipRepository
{
    private readonly IPortTrackerContext _context;

    public ShipRepository(IPortTrackerContext context)
    {
        _context = context;
    }

    public async Task<List<DatabaseLayout.Models.Ship>> GetShipsAsync()
    {
        var ships = await _context.Ships.ToListAsync();
        return ships;
    }

    public async Task<DatabaseLayout.Models.Ship> GetShipAsync(int id)
    {
        var ship = await _context.Ships.FindAsync(id);
        return ship;
    }

    public async Task CreateShipAsync(DatabaseLayout.Models.Ship ship)
    {
        _context.Ships.Add(ship);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateShipAsync(DatabaseLayout.Models.Ship model)
    {
        var ship = await _context.Ships.FindAsync(model.Id);

        if (ship == null) throw new Exception("Ship not found!");

        ship.Name = model.Name;
        ship.MaximumSpeed = model.MaximumSpeed;

        await _context.SaveChangesAsync();
    }

    public async Task DeleteShipAsync(int id)
    {
        var ship = await _context.Ships.FindAsync(id);
        if (ship == null) throw new Exception("Ship not found!");
        _context.Ships.Remove(ship);
        await _context.SaveChangesAsync();
    }
}