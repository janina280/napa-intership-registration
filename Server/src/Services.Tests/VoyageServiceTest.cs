using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;
using Services.Interfaces;
using Services.Models;

namespace Services.Tests;

[TestClass]
[TestSubject(typeof(VoyageService))]
public class VoyageServiceTest
{
    private Mock<IVoyageRepository> _voyageRepositoryMock;
    private Mock<IMapper> _mapperMock;
    private Mock<ILogger<VoyageService>> _loggerMock;
    private VoyageService _voyageService;

    [TestInitialize]
    public void Initialize()
    {
        _voyageRepositoryMock = new Mock<IVoyageRepository>();
        _mapperMock = new Mock<IMapper>();
        _loggerMock = new Mock<ILogger<VoyageService>>();
        _voyageService = new VoyageService(_mapperMock.Object, _loggerMock.Object, _voyageRepositoryMock.Object);
    }

    [TestMethod]
    public async Task GetVoyagesAsync_ShouldReturnVoyages_WhenRepositoryReturnsData()
    {
        // Arrange
        var databaseVoyages = new List<DatabaseLayout.Models.Voyage>
        {
            new DatabaseLayout.Models.Voyage(),
            new DatabaseLayout.Models.Voyage()
        };
        var mappedVoyages = new List<Voyage>
        {
            new Voyage(),
            new Voyage()
        };
        _voyageRepositoryMock.Setup(repo => repo.GetVoyagesAsync()).ReturnsAsync(databaseVoyages);
        _mapperMock.Setup(mapper => mapper.Map<List<Voyage>>(databaseVoyages)).Returns(mappedVoyages);

        // Act
        var response = await _voyageService.GetVoyagesAsync();

        // Assert
        Assert.IsTrue(response.Success);
        Assert.AreEqual(mappedVoyages, response.Response);
        _voyageRepositoryMock.Verify(repo => repo.GetVoyagesAsync(), Times.Once);
        _mapperMock.Verify(mapper => mapper.Map<List<Voyage>>(databaseVoyages), Times.Once);
    }

    [TestMethod]
    public async Task GetVoyagesAsync_ShouldReturnError_WhenRepositoryThrowsException()
    {
        // Arrange
        var exception = new Exception("Database error");
        _voyageRepositoryMock.Setup(repo => repo.GetVoyagesAsync()).ThrowsAsync(exception);

        // Act
        var response = await _voyageService.GetVoyagesAsync();

        // Assert
        Assert.IsFalse(response.Success);
        Assert.AreEqual(exception, response.Exception);
        _voyageRepositoryMock.Verify(repo => repo.GetVoyagesAsync(), Times.Once);
    }

    [TestMethod]
    public async Task GetVoyageAsync_ShouldReturnVoyage_WhenRepositoryReturnsData()
    {
        // Arrange
        var voyageId = 1;
        var databaseVoyage = new DatabaseLayout.Models.Voyage();
        var mappedVoyage = new Voyage();
        _voyageRepositoryMock.Setup(repo => repo.GetVoyageAsync(voyageId)).ReturnsAsync(databaseVoyage);
        _mapperMock.Setup(mapper => mapper.Map<Voyage>(databaseVoyage)).Returns(mappedVoyage);

        // Act
        var response = await _voyageService.GetVoyageAsync(voyageId);

        // Assert
        Assert.IsTrue(response.Success);
        Assert.AreEqual(mappedVoyage, response.Response);
        _voyageRepositoryMock.Verify(repo => repo.GetVoyageAsync(voyageId), Times.Once);
        _mapperMock.Verify(mapper => mapper.Map<Voyage>(databaseVoyage), Times.Once);
    }

    [TestMethod]
    public async Task GetVoyageAsync_ShouldReturnError_WhenRepositoryThrowsException()
    {
        // Arrange
        var voyageId = 1;
        var exception = new Exception("Database error");
        _voyageRepositoryMock.Setup(repo => repo.GetVoyageAsync(voyageId)).ThrowsAsync(exception);

        // Act
        var response = await _voyageService.GetVoyageAsync(voyageId);

        // Assert
        Assert.IsFalse(response.Success);
        Assert.AreEqual(exception, response.Exception);
        _voyageRepositoryMock.Verify(repo => repo.GetVoyageAsync(voyageId), Times.Once);
    }

    [TestMethod]
    public async Task UpdateVoyageAsync_ShouldSucceed_WhenRepositoryUpdatesSuccessfully()
    {
        // Arrange
        var voyage = new Voyage();
        var databaseVoyage = new DatabaseLayout.Models.Voyage();
        _mapperMock.Setup(mapper => mapper.Map<DatabaseLayout.Models.Voyage>(voyage)).Returns(databaseVoyage);

        // Act
        var response = await _voyageService.UpdateVoyageAsync(voyage);

        // Assert
        Assert.IsTrue(response.Success);
        _mapperMock.Verify(mapper => mapper.Map<DatabaseLayout.Models.Voyage>(voyage), Times.Once);
        _voyageRepositoryMock.Verify(repo => repo.UpdateVoyageAsync(databaseVoyage), Times.Once);
    }

    [TestMethod]
    public async Task UpdateVoyageAsync_ShouldReturnError_WhenRepositoryThrowsException()
    {
        // Arrange
        var voyage = new Voyage();
        var exception = new Exception("Database error");
        _mapperMock.Setup(mapper => mapper.Map<DatabaseLayout.Models.Voyage>(voyage)).Throws(exception);
        _voyageRepositoryMock.Setup(repo => repo.UpdateVoyageAsync(It.IsAny<DatabaseLayout.Models.Voyage>()))
            .ThrowsAsync(exception);

        // Act
        var response = await _voyageService.UpdateVoyageAsync(voyage);

        // Assert
        Assert.IsFalse(response.Success);
        Assert.AreEqual(exception.Message, response.Exception?.Message);
    }

    [TestMethod]
    public async Task DeleteVoyageAsync_ShouldSucceed_WhenRepositoryDeletesSuccessfully()
    {
        // Arrange
        var voyageId = 1;

        // Act
        var response = await _voyageService.DeleteVoyageAsync(voyageId);

        // Assert
        Assert.IsTrue(response.Success);
        _voyageRepositoryMock.Verify(repo => repo.DeleteVoyageAsync(voyageId), Times.Once);
    }

    [TestMethod]
    public async Task DeleteVoyageAsync_ShouldReturnError_WhenRepositoryThrowsException()
    {
        // Arrange
        var voyageId = 1;
        var exception = new Exception("Database error");
        _voyageRepositoryMock.Setup(repo => repo.DeleteVoyageAsync(voyageId)).ThrowsAsync(exception);

        // Act
        var response = await _voyageService.DeleteVoyageAsync(voyageId);

        // Assert
        Assert.IsFalse(response.Success);
        Assert.AreEqual(exception.Message, response.Exception.Message);
        _voyageRepositoryMock.Verify(repo => repo.DeleteVoyageAsync(voyageId), Times.Once);
    }
}