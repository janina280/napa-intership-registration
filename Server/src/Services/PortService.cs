using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Services.Interfaces;
using Services.Models;

namespace Services;

public class PortService : IPortService
{
    private readonly IPortRepository _portRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<PortService> _logger;

    public PortService(IMapper mapper, ILogger<PortService> logger,
        IPortRepository portRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _portRepository = portRepository;
    }

    public async Task<ServiceResponse<List<Port>>> GetPortsAsync()
    {
        try
        {
            var ports = await _portRepository.GetPortsAsync();
            var result = _mapper.Map<List<Port>>(ports);
            return new ServiceResponse<List<Port>>(result);
        }
        catch (Exception ex)
        {
            _logger.LogInformation(ex.Message);
            return new ServiceResponse<List<Port>>(ex);
        }
    }

    public async Task<ServiceResponse<Port>> GetPortAsync(int id)
    {
        try
        {
            var port = await _portRepository.GetPortAsync(id);
            var result = _mapper.Map<Port>(port);
            return new ServiceResponse<Port>(result);
        }
        catch (Exception ex)
        {
            _logger.LogInformation(ex.Message);
            return new ServiceResponse<Port>(ex);
        }
    }

    public async Task<ServiceResponse> CreatePortAsync(Port port)
    {
        try
        {
            var entity = _mapper.Map<DatabaseLayout.Models.Port>(port);
            await _portRepository.CreatePortAsync(entity);
            return new ServiceResponse();
        }
        catch (Exception ex)
        {
            _logger.LogInformation(ex.Message);
            return new ServiceResponse(ex);
        }
    }

    public async Task<ServiceResponse> UpdatePortAsync(Port port)
    {
        try
        {
            var entity = _mapper.Map<DatabaseLayout.Models.Port>(port);
            await _portRepository.UpdatePortAsync(entity);
            return new ServiceResponse();
        }
        catch (Exception ex)
        {
            _logger.LogInformation(ex.Message);
            return new ServiceResponse(ex);
        }
    }

    public async Task<ServiceResponse> DeletePortAsync(int id)
    {
        try
        {
            await _portRepository.DeletePortAsync(id);
            return new ServiceResponse();
        }
        catch (Exception ex)
        {
            _logger.LogInformation(ex.Message);
            return new ServiceResponse(ex);
        }
    }
}