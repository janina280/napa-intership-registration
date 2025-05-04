using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Services.Interfaces;
using Services.Models;

namespace Server.Controllers;

public class ShipController : BaseController
{
    private readonly IShipService _shipService;
    private readonly ILogger<ShipController> _logger;

    public ShipController(IShipService shipService, ILogger<ShipController> logger)
    {
        _shipService = shipService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetShipsAsync()
    {
        _logger.LogInformation("Get all ships");
        var ships = await _shipService.GetShipsAsync();
        return ApiServiceResponse.ApiServiceResult(ships);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetShipAsync(int id)
    {
        _logger.LogInformation($"Get ship by id: {id}");
        var ship = await _shipService.GetShipAsync(id);
        return ApiServiceResponse.ApiServiceResult(ship);
    }

    [HttpPost]
    public async Task<IActionResult> CreateShipAsync([FromBody] Ship newShipDto)
    {
        _logger.LogInformation("Create ship");
        var result = await _shipService.CreateShipAsync(newShipDto);
        return ApiServiceResponse.ApiServiceResult(result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateShipAsync([FromBody] Ship updatedShipDto)
    {
        _logger.LogInformation($"Update ship: {updatedShipDto.Id}");
        var result = await _shipService.UpdateShipAsync(updatedShipDto);
        return ApiServiceResponse.ApiServiceResult(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteShipAsync(int id)
    {
        _logger.LogInformation($"Delete ship: {id}");
        var result = await _shipService.DeleteShipAsync(id);
        return ApiServiceResponse.ApiServiceResult(result);
    }
}