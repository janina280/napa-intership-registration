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
[TestSubject(typeof(ShipService))]
public class ShipServiceTest
{
    private ShipService _shipService;
    private Mock<IShipRepository> _shipRepositoryMock;
    private Mock<IMapper> _mapperMock;
    private Mock<ILogger<ShipService>> _loggerMock;

    [TestInitialize]
    public void TestInitialize()
    {
        _shipRepositoryMock = new Mock<IShipRepository>();
        _mapperMock = new Mock<IMapper>();
        _loggerMock = new Mock<ILogger<ShipService>>();

        _shipService = new ShipService(_mapperMock.Object, _loggerMock.Object, _shipRepositoryMock.Object);
    }

    [TestMethod]
    public async Task GetShipsAsync_ShouldReturnListOfShips_WhenRepositoryReturnsData()
    {
        // Arrange
        var ships = new List<DatabaseLayout.Models.Ship>
        {
            new() { Id = 1, Name = "Ship1", MaximumSpeed = 20.5 },
            new() { Id = 2, Name = "Ship2", MaximumSpeed = 30.0 }
        };

        var mappedShips = new List<Ship>
        {
            new() { Id = 1, Name = "Ship1", MaximumSpeed = 20.5 },
            new() { Id = 2, Name = "Ship2", MaximumSpeed = 30.0 }
        };

        _shipRepositoryMock.Setup(r => r.GetShipsAsync()).ReturnsAsync(ships);
        _mapperMock.Setup(m => m.Map<List<Ship>>(ships)).Returns(mappedShips);

        // Act
        var result = await _shipService.GetShipsAsync();

        // Assert
        Assert.IsTrue(result.Success);
        Assert.AreEqual(2, result.Response.Count);
    }

    [TestMethod]
    public async Task GetShipsAsync_ShouldReturnError_WhenRepositoryThrowsException()
    {
        // Arrange
        var exception = new Exception("Database error");
        _shipRepositoryMock.Setup(r => r.GetShipsAsync()).ThrowsAsync(exception);

        // Act
        var result = await _shipService.GetShipsAsync();

        // Assert
        Assert.IsFalse(result.Success);
        Assert.AreEqual(exception, result.Exception);
    }

    [TestMethod]
    public async Task GetShipAsync_ShouldReturnShip_WhenRepositoryReturnsData()
    {
        // Arrange
        var ship = new DatabaseLayout.Models.Ship { Id = 1, Name = "Ship1", MaximumSpeed = 20.0 };
        var mappedShip = new Ship { Id = 1, Name = "Ship1", MaximumSpeed = 20.0 };

        _shipRepositoryMock.Setup(r => r.GetShipAsync(1)).ReturnsAsync(ship);
        _mapperMock.Setup(m => m.Map<Ship>(ship)).Returns(mappedShip);

        // Act
        var result = await _shipService.GetShipAsync(1);

        // Assert
        Assert.IsTrue(result.Success);
        Assert.AreEqual(mappedShip, result.Response);
    }

    [TestMethod]
    public async Task GetShipAsync_ShouldReturnError_WhenRepositoryThrowsException()
    {
        // Arrange
        var exception = new Exception("Database error");

        _shipRepositoryMock.Setup(r => r.GetShipAsync(1)).ThrowsAsync(exception);

        // Act
        var result = await _shipService.GetShipAsync(1);

        // Assert
        Assert.IsFalse(result.Success);
        Assert.AreEqual(exception, result.Exception);
    }

    [TestMethod]
    public async Task CreateShipAsync_ShouldSucceed_WhenShipIsValid()
    {
        // Arrange
        var ship = new Ship { Id = 1, Name = "Ship1", MaximumSpeed = 20.0 };
        var dbShip = new DatabaseLayout.Models.Ship { Id = 1, Name = "Ship1", MaximumSpeed = 20.0 };

        _mapperMock.Setup(m => m.Map<DatabaseLayout.Models.Ship>(ship)).Returns(dbShip);

        _shipRepositoryMock.Setup(r => r.CreateShipAsync(dbShip)).Returns(Task.CompletedTask);

        // Act
        var result = await _shipService.CreateShipAsync(ship);

        // Assert
        Assert.IsTrue(result.Success);
    }

    [TestMethod]
    public async Task CreateShipAsync_ShouldReturnError_WhenRepositoryThrowsException()
    {
        // Arrange
        var ship = new Ship { Id = 1, Name = "Ship1", MaximumSpeed = 20.0 };
        var dbShip = new DatabaseLayout.Models.Ship { Id = 1, Name = "Ship1", MaximumSpeed = 20.0 };
        var exception = new Exception("Database error");

        _mapperMock.Setup(m => m.Map<DatabaseLayout.Models.Ship>(ship)).Returns(dbShip);
        _shipRepositoryMock.Setup(r => r.CreateShipAsync(dbShip)).ThrowsAsync(exception);

        // Act
        var result = await _shipService.CreateShipAsync(ship);

        // Assert
        Assert.IsFalse(result.Success);
        Assert.AreEqual(exception, result.Exception);
    }

    [TestMethod]
    public async Task UpdateShipAsync_ShouldSucceed_WhenShipIsValid()
    {
        // Arrange
        var ship = new Ship { Id = 1, Name = "UpdatedShip", MaximumSpeed = 25.0 };
        var dbShip = new DatabaseLayout.Models.Ship { Id = 1, Name = "UpdatedShip", MaximumSpeed = 25.0 };

        _mapperMock.Setup(m => m.Map<DatabaseLayout.Models.Ship>(ship)).Returns(dbShip);
        _shipRepositoryMock.Setup(r => r.UpdateShipAsync(dbShip)).Returns(Task.CompletedTask);

        // Act
        var result = await _shipService.UpdateShipAsync(ship);

        // Assert
        Assert.IsTrue(result.Success);
    }

    [TestMethod]
    public async Task UpdateShipAsync_ShouldReturnError_WhenRepositoryThrowsException()
    {
        // Arrange
        var ship = new Ship { Id = 1, Name = "UpdatedShip", MaximumSpeed = 25.0 };
        var dbShip = new DatabaseLayout.Models.Ship { Id = 1, Name = "UpdatedShip", MaximumSpeed = 25.0 };
        var exception = new Exception("Database error");

        _mapperMock.Setup(m => m.Map<DatabaseLayout.Models.Ship>(ship)).Returns(dbShip);
        _shipRepositoryMock.Setup(r => r.UpdateShipAsync(dbShip)).ThrowsAsync(exception);

        // Act
        var result = await _shipService.UpdateShipAsync(ship);

        // Assert
        Assert.IsFalse(result.Success);
        Assert.AreEqual(exception, result.Exception);
    }

    [TestMethod]
    public async Task DeleteShipAsync_ShouldSucceed_WhenShipExists()
    {
        // Arrange
        _shipRepositoryMock.Setup(r => r.DeleteShipAsync(1)).Returns(Task.CompletedTask);

        // Act
        var result = await _shipService.DeleteShipAsync(1);

        // Assert
        Assert.IsTrue(result.Success);
    }

    [TestMethod]
    public async Task DeleteShipAsync_ShouldReturnError_WhenRepositoryThrowsException()
    {
        // Arrange
        var exception = new Exception("Database error");

        _shipRepositoryMock.Setup(r => r.DeleteShipAsync(1)).ThrowsAsync(exception);

        // Act
        var result = await _shipService.DeleteShipAsync(1);

        // Assert
        Assert.IsFalse(result.Success);
        Assert.AreEqual(exception, result.Exception);
    }
}