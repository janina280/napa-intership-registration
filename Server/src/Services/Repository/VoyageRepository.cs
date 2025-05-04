using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DatabaseLayout.Context;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;

namespace Services.Repository;

public class VoyageRepository : IVoyageRepository
{
    private readonly IPortTrackerContext _context;

    public VoyageRepository(IPortTrackerContext context)
    {
        _context = context;
    }

    public async Task<List<DatabaseLayout.Models.Voyage>> GetVoyagesAsync()
    {
        var voyages = await _context.Voyages.ToListAsync();
        return voyages;
    }

    public async Task<DatabaseLayout.Models.Voyage> GetVoyageAsync(int id)
    {
        var voyage = await _context.Voyages.FindAsync(id);
        return voyage;
    }

    public async Task CreateVoyageAsync(DatabaseLayout.Models.Voyage voyage)
    {
        _context.Voyages.Add(voyage);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateVoyageAsync(DatabaseLayout.Models.Voyage model)
    {
        var voyage = await _context.Voyages.FindAsync(model.Id);

        if (voyage == null) throw new Exception("Voyage not found!");

        voyage.VoyageDate = model.VoyageDate;
        voyage.VoyageEnd = model.VoyageEnd;
        voyage.VoyageStart = model.VoyageStart;
        
        await _context.SaveChangesAsync();
    }

    public async Task DeleteVoyageAsync(int id)
    {
        var voyage = await _context.Voyages.FindAsync(id);
        if (voyage == null) throw new Exception("Voyage not found!");
        _context.Voyages.Remove(voyage);
        await _context.SaveChangesAsync();
    }
}