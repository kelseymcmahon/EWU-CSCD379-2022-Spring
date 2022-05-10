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
        Assert.AreEqual(10, service.GetPlayers().Count());
    }
}

