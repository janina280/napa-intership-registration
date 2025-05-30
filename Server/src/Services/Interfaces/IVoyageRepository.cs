﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Services.Dtos;

namespace Services.Interfaces;

public interface IVoyageRepository
{
    Task<List<DatabaseLayout.Models.Voyage>> GetVoyagesAsync();
    Task<DatabaseLayout.Models.Voyage> GetVoyageAsync(int id);
    Task CreateVoyageAsync(VoyageDto voyageDto);
    Task UpdateVoyageAsync(DatabaseLayout.Models.Voyage model);
    Task DeleteVoyageAsync(int id);
}