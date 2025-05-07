using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Services.Dtos;
using Services.Interfaces;
using Services.Models;

namespace Server.Controllers;

public class VoyagesController : BaseController
{
    private readonly IVoyageService _voyageService;
    private readonly ILogger<VoyagesController> _logger;

    public VoyagesController(IVoyageService voyageService, ILogger<VoyagesController> logger)
    {
        _voyageService = voyageService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetVoyagesAsync()
    {
        _logger.LogInformation("Get all voyages");
        var voyages = await _voyageService.GetVoyagesAsync();
        return ApiServiceResponse.ApiServiceResult(voyages);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetVoyageAsync(int id)
    {
        _logger.LogInformation($"Get voyage by id: {id}");
        var voyage = await _voyageService.GetVoyageAsync(id);
        return ApiServiceResponse.ApiServiceResult(voyage);
    }

    [HttpPost]
    public async Task<IActionResult> CreateVoyageAsync([FromBody] VoyageDto newVoyageDto)
    {
        _logger.LogInformation("Create voyage");
        var result = await _voyageService.CreateVoyageAsync(newVoyageDto);
        return ApiServiceResponse.ApiServiceResult(result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateVoyageAsync([FromBody] Voyage updatedVoyageDto)
    {
        _logger.LogInformation($"Update voyage: {updatedVoyageDto.Id}");
        var result = await _voyageService.UpdateVoyageAsync(updatedVoyageDto);
        return ApiServiceResponse.ApiServiceResult(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteVoyageAsync(int id)
    {
        _logger.LogInformation($"Delete voyage: {id}");
        var result = await _voyageService.DeleteVoyageAsync(id);
        return ApiServiceResponse.ApiServiceResult(result);
    }
}