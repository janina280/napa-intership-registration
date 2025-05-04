using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Services.Interfaces;
using Services.Models;

namespace Services;

public class ShipService : IShipService
{
    private readonly IShipRepository _shipRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<ShipService> _logger;

    public ShipService(IMapper mapper, ILogger<ShipService> logger,
        IShipRepository shipRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _shipRepository = shipRepository;
    }

    public async Task<ServiceResponse<List<Ship>>> GetShipsAsync()
    {
        try
        {
            var ships = await _shipRepository.GetShipsAsync();
            var result = _mapper.Map<List<Ship>>(ships);
            return new ServiceResponse<List<Ship>>(result);
        }
        catch (Exception ex)
        {
            _logger.LogInformation(ex.Message);
            return new ServiceResponse<List<Ship>>(ex);
        }
    }

    public async Task<ServiceResponse<Ship>> GetShipAsync(int id)
    {
        try
        {
            var ship = await _shipRepository.GetShipAsync(id);
            var result = _mapper.Map<Ship>(ship);
            return new ServiceResponse<Ship>(result);
        }
        catch (Exception ex)
        {
            _logger.LogInformation(ex.Message);
            return new ServiceResponse<Ship>(ex);
        }
    }

    public async Task<ServiceResponse> CreateShipAsync(Ship ship)
    {
        try
        {
            var entity = _mapper.Map<DatabaseLayout.Models.Ship>(ship);
            await _shipRepository.CreateShipAsync(entity);
            return new ServiceResponse();
        }
        catch (Exception ex)
        {
            _logger.LogInformation(ex.Message);
            return new ServiceResponse(ex);
        }
    }

    public async Task<ServiceResponse> UpdateShipAsync(Ship ship)
    {
        try
        {
            var entity = _mapper.Map<DatabaseLayout.Models.Ship>(ship);
            await _shipRepository.UpdateShipAsync(entity);
            return new ServiceResponse();
        }
        catch (Exception ex)
        {
            _logger.LogInformation(ex.Message);
            return new ServiceResponse(ex);
        }
    }

    public async Task<ServiceResponse> DeleteShipAsync(int id)
    {
        try
        {
            await _shipRepository.DeleteShipAsync(id);
            return new ServiceResponse();
        }
        catch (Exception ex)
        {
            _logger.LogInformation(ex.Message);
            return new ServiceResponse(ex);
        }
    }
}