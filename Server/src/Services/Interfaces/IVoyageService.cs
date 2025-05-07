using System.Collections.Generic;
using System.Threading.Tasks;
using Services.Dtos;
using Services.Models;

namespace Services.Interfaces;

public interface IVoyageService
{
    Task<ServiceResponse<List<Voyage>>> GetVoyagesAsync();
    Task<ServiceResponse<Voyage>> GetVoyageAsync(int id);
    Task<ServiceResponse> CreateVoyageAsync(VoyageDto voyage);
    Task<ServiceResponse> UpdateVoyageAsync(Voyage voyage);
    Task<ServiceResponse> DeleteVoyageAsync(int id);
}