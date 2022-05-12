using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wordle.api.Data;
using Wordle.api.Services;

namespace Wordle.api.Tests;

[TestClass]
public class PlayerServiceTests
{
    private AppDbContext _context;
    private PlayerService service;

    [TestInitialize]
    public void testInit()
    {
        var contextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Wordle.Api.Tests;Trusted_Connection=True;MultipleActiveResultSets=true");
        _context = new AppDbContext(contextOptions.Options);
        _context.Database.Migrate();

        service = new PlayerService(_context);

        _context.Players.Add(new Player()
        {
            Name = "Paul",
            GameCount = 10,
            AverageAttempts = 2.3,
            AverageSeconds = 20
        });

        _context.Players.Add(new Player()
        {
            Name = "Paulina",
            GameCount = 5,
            AverageAttempts = 3.0,
            AverageSeconds = 100
        });

        _context.Players.Add(new Player()
        {
            Name = "Donkey",
            GameCount = 3,
            AverageAttempts = 4.0,
            AverageSeconds = 50
        });

        _context.SaveChanges();
    }

    [TestCleanup]
    public void testClean()
    {
        _context.Players.Remove(service.GetPlayers().First(x => x.Name == "Paul"));
        _context.Players.Remove(service.GetPlayers().First(x => x.Name == "Paulina"));
        _context.Players.Remove(service.GetPlayers().First(x => x.Name == "Donkey"));

        _context.SaveChanges();
    }

    [TestMethod]
    public void GetPlayers()
    {
        Assert.AreEqual(7, service.GetPlayers().Count());
    }

    [TestMethod]
    public void Update_AddsNewPlayer()
    {
        service.Update("Hello", 1, 5, 30);

        Assert.AreEqual(1, service.GetPlayers().First(x => x.Name == "Hello").GameCount);
        Assert.AreEqual(5, service.GetPlayers().First(x => x.Name == "Hello").AverageAttempts);
        Assert.AreEqual(30, service.GetPlayers().First(x => x.Name == "Hello").AverageSeconds);

        service.RemovePlayer("Hello");
    }

    [TestMethod]
    public void Update_UpdatesCurrentPlayer()
    {
        Player player = service.GetPlayers().First(x => x.Name == "Paul").Clone();

        int gameCount = player.GameCount;
        double averageAttemps = Math.Round((player.GameCount * player.AverageAttempts + 5) / (player.GameCount + 1), 2);
        double averageSeconds = Math.Round((player.GameCount * player.AverageSeconds + 30) / (player.GameCount + 1), 2);

        service.Update("Paul", 1, 5, 30);

        Assert.AreEqual(gameCount + 1, service.GetPlayers().First(x => x.Name == "Paul").GameCount);
        Assert.AreEqual(averageAttemps, service.GetPlayers().First(x => x.Name == "Paul").AverageAttempts);
        Assert.AreEqual(averageSeconds, service.GetPlayers().First(x => x.Name == "Paul").AverageSeconds);
    }
}

