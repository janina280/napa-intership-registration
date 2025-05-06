using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Services.Interfaces;
using Services.Models;

namespace Server.Controllers;

public class PortsController : BaseController
{
    private readonly IPortService _portService;
    private readonly ILogger<PortsController> _logger;

    public PortsController(IPortService portService, ILogger<PortsController> logger)
    {
        _portService = portService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetPortsAsync()
    {
        _logger.LogInformation("Get all ports");
        var ports = await _portService.GetPortsAsync();
        return ApiServiceResponse.ApiServiceResult(ports);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPortAsync(int id)
    {
        _logger.LogInformation($"Get port by id: {id}");
        var port = await _portService.GetPortAsync(id);
        return ApiServiceResponse.ApiServiceResult(port);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePortAsync([FromBody] Port newPortDto)
    {
        _logger.LogInformation("Create port");
        var result = await _portService.CreatePortAsync(newPortDto);
        return ApiServiceResponse.ApiServiceResult(result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdatePortAsync([FromBody] Port updatedPortDto)
    {
        _logger.LogInformation($"Update port: {updatedPortDto.Id}");
        var result = await _portService.UpdatePortAsync(updatedPortDto);
        return ApiServiceResponse.ApiServiceResult(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePortAsync(int id)
    {
        _logger.LogInformation($"Delete port: {id}");
        var result = await _portService.DeletePortAsync(id);
        return ApiServiceResponse.ApiServiceResult(result);
    }
}