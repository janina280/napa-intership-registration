using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DatabaseLayout.Context;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;

namespace Services.Repository;

public class PortRepository : IPortRepository
{
    private readonly IPortTrackerContext _context;

    public PortRepository(IPortTrackerContext context)
    {
        _context = context;
    }

    public async Task<List<DatabaseLayout.Models.Port>> GetPortsAsync()
    {
        var ports = await _context.Ports.ToListAsync();
        return ports;
    }

    public async Task<DatabaseLayout.Models.Port> GetPortAsync(int id)
    {
        var port = await _context.Ports.FindAsync(id);
        return port;
    }

    public async Task CreatePortAsync(DatabaseLayout.Models.Port port)
    {
        _context.Ports.Add(port);
        await _context.SaveChangesAsync();
    }

    public async Task UpdatePortAsync(DatabaseLayout.Models.Port model)
    {
        var port = await _context.Ports.FindAsync(model.Id);

        if (port == null) throw new Exception("Port not found!");

        port.Name = model.Name;
        port.Country = model.Country;

        await _context.SaveChangesAsync();
    }

    public async Task DeletePortAsync(int id)
    {
        var port = await _context.Ports.FindAsync(id);
        if (port == null) throw new Exception("Port not found!");
        _context.Ports.Remove(port);
        await _context.SaveChangesAsync();
    }
}