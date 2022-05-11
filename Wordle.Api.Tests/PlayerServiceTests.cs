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
        Assert.AreEqual(6, service.GetPlayers().Count());
    }

    [TestMethod]
    public void Update_AddsNewPlayer()
    {
        Player player = service.GetPlayers().First(x => x.Name == "Paul").Clone();

        //double newAverage = Math.Round(player.AverageAttempts + 5 / player.GameCount++, 2);
        service.Update("Paul", 1, 5, 30);

        Assert.AreEqual(player.GameCount + 1, service.GetPlayers().First(x => x.Name == "Paul").GameCount);
        Assert.AreEqual(2.55, service.GetPlayers().First(x => x.Name == "Paul").AverageAttempts);
    }

    [TestMethod]
    public void Update_UpdatesCurrentPlayer()
    {

    }
}

