using System.Collections.Generic;
using System.Threading.Tasks;
using Services.Models;

namespace Services.Interfaces;

public interface IPortService
{
    Task<ServiceResponse<List<Port>>> GetPortsAsync();
    Task<ServiceResponse<Port>> GetPortAsync(int id);
    Task<ServiceResponse> CreatePortAsync(Port port);
    Task<ServiceResponse> UpdatePortAsync(Port port);
    Task<ServiceResponse> DeletePortAsync(int id);
}