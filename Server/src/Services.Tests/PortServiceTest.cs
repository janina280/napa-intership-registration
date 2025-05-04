using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Services.Interfaces;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace Services.Tests;

[TestClass]
[TestSubject(typeof(PortService))]
public class PortServiceTest
{
    private Mock<IPortRepository> _portRepositoryMock;
    private Mock<IMapper> _mapperMock;
    private Mock<ILogger<PortService>> _loggerMock;
    private PortService _portService;

    [TestInitialize]
    public void SetUp()
    {
        _portRepositoryMock = new Mock<IPortRepository>();
        _mapperMock = new Mock<IMapper>();
        _loggerMock = new Mock<ILogger<PortService>>();

        _portService = new PortService(
            _mapperMock.Object,
            _loggerMock.Object,
            _portRepositoryMock.Object
        );
    }

    [TestMethod]
    public async Task GetPortsAsync_ShouldReturnPorts_WhenRepositoryReturnsResults()
    {
        // Arrange
        var portsFromRepo = new List<DatabaseLayout.Models.Port>
        {
            new() { Id = 1, Name = "Port 1", Country = "Country 1" },
            new() { Id = 2, Name = "Port 2", Country = "Country 2" }
        };
        var mappedPorts = new List<Port>
        {
            new() { Id = 1, Name = "Port 1", Country = "Country 1" },
            new() { Id = 2, Name = "Port 2", Country = "Country 2" }
        };

        _portRepositoryMock.Setup(repo => repo.GetPortsAsync()).ReturnsAsync(portsFromRepo);
        _mapperMock.Setup(mapper => mapper.Map<List<Port>>(portsFromRepo)).Returns(mappedPorts);

        // Act
        var result = await _portService.GetPortsAsync();

        // Assert
        Assert.IsTrue(result.Success);
        Assert.IsNotNull(result.Response);
        Assert.AreEqual(2, result.Response.Count);
        _portRepositoryMock.Verify(repo => repo.GetPortsAsync(), Times.Once);
        _mapperMock.Verify(mapper => mapper.Map<List<Port>>(portsFromRepo), Times.Once);
    }

    [TestMethod]
    public async Task GetPortsAsync_ShouldReturnError_WhenRepositoryThrowsException()
    {
        // Arrange
        var exception = new Exception("Database error");
        _portRepositoryMock.Setup(repo => repo.GetPortsAsync()).ThrowsAsync(exception);

        // Act
        var result = await _portService.GetPortsAsync();

        // Assert
        Assert.IsFalse(result.Success);
        Assert.AreEqual(exception, result.Exception);
    }

    [TestMethod]
    public async Task GetPortAsync_ShouldReturnPort_WhenRepositoryReturnsResult()
    {
        // Arrange
        var portFromRepo = new DatabaseLayout.Models.Port { Id = 1, Name = "Port 1", Country = "Country 1" };
        var mappedPort = new Port { Id = 1, Name = "Port 1", Country = "Country 1" };

        _portRepositoryMock.Setup(repo => repo.GetPortAsync(1)).ReturnsAsync(portFromRepo);
        _mapperMock.Setup(mapper => mapper.Map<Port>(portFromRepo)).Returns(mappedPort);

        // Act
        var result = await _portService.GetPortAsync(1);

        // Assert
        Assert.IsTrue(result.Success);
        Assert.IsNotNull(result.Response);
        Assert.AreEqual(1, result.Response.Id);
        _portRepositoryMock.Verify(repo => repo.GetPortAsync(1), Times.Once);
        _mapperMock.Verify(mapper => mapper.Map<Port>(portFromRepo), Times.Once);
    }

    [TestMethod]
    public async Task GetPortAsync_ShouldReturnError_WhenRepositoryThrowsException()
    {
        // Arrange
        var exception = new Exception("Database error");
        _portRepositoryMock.Setup(repo => repo.GetPortAsync(1)).ThrowsAsync(exception);

        // Act
        var result = await _portService.GetPortAsync(1);

        // Assert
        Assert.IsFalse(result.Success);
        Assert.AreEqual(exception, result.Exception);
    }

    [TestMethod]
    public async Task CreatePortAsync_ShouldCreatePort_WhenValidPortIsProvided()
    {
        // Arrange
        var portToCreate = new Port { Id = 1, Name = "Port 1", Country = "Country 1" };
        var mappedEntity = new DatabaseLayout.Models.Port { Id = 1, Name = "Port 1", Country = "Country 1" };

        _mapperMock.Setup(mapper => mapper.Map<DatabaseLayout.Models.Port>(portToCreate)).Returns(mappedEntity);
        _portRepositoryMock.Setup(repo => repo.CreatePortAsync(mappedEntity)).Returns(Task.CompletedTask);

        // Act
        var result = await _portService.CreatePortAsync(portToCreate);

        // Assert
        Assert.IsTrue(result.Success);
        _mapperMock.Verify(mapper => mapper.Map<DatabaseLayout.Models.Port>(portToCreate), Times.Once);
        _portRepositoryMock.Verify(repo => repo.CreatePortAsync(mappedEntity), Times.Once);
    }

    [TestMethod]
    public async Task CreatePortAsync_ShouldReturnError_WhenRepositoryThrowsException()
    {
        // Arrange
        var portToCreate = new Port { Id = 1, Name = "Port 1", Country = "Country 1" };
        var exception = new Exception("Database error");

        _mapperMock.Setup(mapper => mapper.Map<DatabaseLayout.Models.Port>(portToCreate))
            .Throws(exception);

        // Act
        var result = await _portService.CreatePortAsync(portToCreate);

        // Assert
        Assert.IsFalse(result.Success);
        Assert.AreEqual(exception, result.Exception);
    }

    [TestMethod]
    public async Task UpdatePortAsync_ShouldUpdatePort_WhenValidPortIsProvided()
    {
        // Arrange
        var portToUpdate = new Port { Id = 1, Name = "Port 1", Country = "Country 1" };
        var mappedEntity = new DatabaseLayout.Models.Port { Id = 1, Name = "Port 1", Country = "Country 1" };

        _mapperMock.Setup(mapper => mapper.Map<DatabaseLayout.Models.Port>(portToUpdate)).Returns(mappedEntity);
        _portRepositoryMock.Setup(repo => repo.UpdatePortAsync(mappedEntity)).Returns(Task.CompletedTask);

        // Act
        var result = await _portService.UpdatePortAsync(portToUpdate);

        // Assert
        Assert.IsTrue(result.Success);
        _mapperMock.Verify(mapper => mapper.Map<DatabaseLayout.Models.Port>(portToUpdate), Times.Once);
        _portRepositoryMock.Verify(repo => repo.UpdatePortAsync(mappedEntity), Times.Once);
    }

    [TestMethod]
    public async Task UpdatePortAsync_ShouldReturnError_WhenRepositoryThrowsException()
    {
        // Arrange
        var portToUpdate = new Port { Id = 1, Name = "Port 1", Country = "Country 1" };
        var exception = new Exception("Database error");

        _mapperMock.Setup(mapper => mapper.Map<DatabaseLayout.Models.Port>(portToUpdate)).Throws(exception);

        // Act
        var result = await _portService.UpdatePortAsync(portToUpdate);

        // Assert
        Assert.IsFalse(result.Success);
        Assert.AreEqual(exception, result.Exception);
    }

    [TestMethod]
    public async Task DeletePortAsync_ShouldDeletePort_WhenValidIdIsProvided()
    {
        // Arrange
        const int portId = 1;

        _portRepositoryMock.Setup(repo => repo.DeletePortAsync(portId)).Returns(Task.CompletedTask);

        // Act
        var result = await _portService.DeletePortAsync(portId);

        // Assert
        Assert.IsTrue(result.Success);
        _portRepositoryMock.Verify(repo => repo.DeletePortAsync(portId), Times.Once);
    }

    [TestMethod]
    public async Task DeletePortAsync_ShouldReturnError_WhenRepositoryThrowsException()
    {
        // Arrange
        const int portId = 1;
        var exception = new Exception("Database error");

        _portRepositoryMock.Setup(repo => repo.DeletePortAsync(portId)).ThrowsAsync(exception);

        // Act
        var result = await _portService.DeletePortAsync(portId);

        // Assert
        Assert.IsFalse(result.Success);
        Assert.AreEqual(exception, result.Exception);
    }
}