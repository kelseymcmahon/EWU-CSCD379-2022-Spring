using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Wordle.api.Data;
using Wordle.api.Services;

namespace Wordle.api.Tests
{
    [TestClass]
    public class ScoreStatsServiceTests
    {
        [TestMethod]
        public void GetScoreStats()
        {
            var contextOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Wordle.Api.Tests;Trusted_Connection=True;MultipleActiveResultSets=true");
            var context = new AppDbContext(contextOptions.Options);
            context.Database.Migrate();
            ScoreStatsService.Seed(context);
            ScoreStatsService sut = new ScoreStatsService(context);

            Assert.AreEqual(6, sut.GetScoreStats().Count());
        }

        [TestMethod]
        public void Update()
        {

        }
    }
}
