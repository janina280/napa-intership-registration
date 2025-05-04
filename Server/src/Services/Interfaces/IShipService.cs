using System.Collections.Generic;
using System.Threading.Tasks;
using Services.Models;

namespace Services.Interfaces;

public interface IShipService
{
    Task<ServiceResponse<List<Ship>>> GetShipsAsync();
    Task<ServiceResponse<Ship>> GetShipAsync(int id);
    Task<ServiceResponse> CreateShipAsync(Ship ship);
    Task<ServiceResponse> UpdateShipAsync(Ship ship);
    Task<ServiceResponse> DeleteShipAsync(int id);
}