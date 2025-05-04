using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interfaces;

public interface IShipRepository
{
    Task<List<DatabaseLayout.Models.Ship>> GetShipsAsync();
    Task<DatabaseLayout.Models.Ship> GetShipAsync(int id);
    Task CreateShipAsync(DatabaseLayout.Models.Ship ship);
    Task UpdateShipAsync(DatabaseLayout.Models.Ship model);
    Task DeleteShipAsync(int id);
}