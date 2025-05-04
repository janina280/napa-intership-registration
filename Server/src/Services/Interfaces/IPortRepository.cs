using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interfaces;

public interface IPortRepository
{
    Task<List<DatabaseLayout.Models.Port>> GetPortsAsync();
    Task<DatabaseLayout.Models.Port> GetPortAsync(int id);
    Task CreatePortAsync(DatabaseLayout.Models.Port port);
    Task UpdatePortAsync(DatabaseLayout.Models.Port model);
    Task DeletePortAsync(int id);
}