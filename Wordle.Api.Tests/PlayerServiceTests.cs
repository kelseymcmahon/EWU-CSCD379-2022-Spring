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

    public PlayerServiceTests()
    {
        var contextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Wordle.Api.Tests;Trusted_Connection=True;MultipleActiveResultSets=true");
        _context = new AppDbContext(contextOptions.Options);
        _context.Database.Migrate();
        PlayerService.Seed(_context);
    }

    [TestMethod]
    public void GetPlayers()
    {
        PlayerService service = new PlayerService(_context);
        Assert.AreEqual(3, service.GetPlayers().Count());
    }

    [TestMethod]
    public void Update_AddsNewPlayer()
    {
        PlayerService service = new PlayerService(_context);
        //service.Update("Kelsey", 1, 5, 20);
        //Assert.AreEqual("Kelsey", service.GetPlayers().First(x => x.Name == "Kelsey").Name);
        //Assert.AreEqual(11, service.GetPlayers().First(x => x.Name == "Kelsey").GameCount);
        //Assert.AreEqual(2, service.GetPlayers().First(x => x.Name == "Kelsey").AverageAttempts);
    }

    [TestMethod]
    public void Update_UpdatesCurrentPlayer()
    {

    }
}

