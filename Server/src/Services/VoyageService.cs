using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Services.Dtos;
using Services.Interfaces;
using Services.Models;

namespace Services;

public class VoyageService : IVoyageService
{
    private readonly IVoyageRepository _voyageRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<VoyageService> _logger;

    public VoyageService(IMapper mapper, ILogger<VoyageService> logger,
        IVoyageRepository voyageRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _voyageRepository = voyageRepository;
    }

    public async Task<ServiceResponse<List<Voyage>>> GetVoyagesAsync()
    {
        try
        {
            var voyages = await _voyageRepository.GetVoyagesAsync();
            var result = _mapper.Map<List<Voyage>>(voyages);
            return new ServiceResponse<List<Voyage>>(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return new ServiceResponse<List<Voyage>>(ex);
        }
    }

    public async Task<ServiceResponse<Voyage>> GetVoyageAsync(int id)
    {
        try
        {
            var voyage = await _voyageRepository.GetVoyageAsync(id);
            var result = _mapper.Map<Voyage>(voyage);
            return new ServiceResponse<Voyage>(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return new ServiceResponse<Voyage>(ex);
        }
    }

    public async Task<ServiceResponse> CreateVoyageAsync(VoyageDto voyage)
    {
        try
        {
            await _voyageRepository.CreateVoyageAsync(voyage);
            return new ServiceResponse();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return new ServiceResponse(ex);
        }
    }


    public async Task<ServiceResponse> UpdateVoyageAsync(Voyage voyage)
    {
        try
        {
            var entity = _mapper.Map<DatabaseLayout.Models.Voyage>(voyage);
            await _voyageRepository.UpdateVoyageAsync(entity);
            return new ServiceResponse();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return new ServiceResponse(ex);
        }
    }

    public async Task<ServiceResponse> DeleteVoyageAsync(int id)
    {
        try
        {
            await _voyageRepository.DeleteVoyageAsync(id);
            return new ServiceResponse();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return new ServiceResponse(ex);
        }
    }
}