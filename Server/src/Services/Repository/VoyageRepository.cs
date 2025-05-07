using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DatabaseLayout.Context;
using DatabaseLayout.Models;
using Microsoft.EntityFrameworkCore;
using Services.Dtos;
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
        var voyages = await _context.Voyages
            .Include(v => v.Ship)
            .Include(v => v.DeparturePort)
            .Include(v => v.ArrivalPort)
            .ToListAsync();
        return voyages;
    }

    public async Task<DatabaseLayout.Models.Voyage> GetVoyageAsync(int id)
    {
        var voyage = await _context.Voyages.FindAsync(id);
        return voyage;
    }

    public async Task CreateVoyageAsync(VoyageDto dto)
    {
        var ship = await _context.Ships.FindAsync(dto.ShipId);
        var departurePort = await _context.Ports.FindAsync(dto.DeparturePortId);
        var arrivalPort = await _context.Ports.FindAsync(dto.ArrivalPortId);

        if (ship == null || departurePort == null || arrivalPort == null)
            throw new Exception("Ship or Port not found");

        var voyage = new Voyage
        {
            Ship = ship,
            DeparturePort = departurePort,
            ArrivalPort = arrivalPort,
            VoyageStart = dto.VoyageStart,
            VoyageEnd = dto.VoyageEnd,
            VoyageDate = DateTime.Now
        };

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