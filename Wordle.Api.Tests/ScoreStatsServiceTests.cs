using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wordle.api.Services;
using Wordle.api.Data;

namespace Wordle.api.Tests
{
    [TestClass]
    public class ScoreStatsServiceTests
    {

        [TestMethod]
        public void GetScoreStats()
        {
            var contextOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlServer("Server=(localdb)\\MSSQLLocalDB; Database=Wordle.api.Tests; Trusted_Conection=true; MultipleActiveResultSets=true");
            var context = new AppDbContext(contextOptions.Options);
            context.Database.Migrate();
            ScoreStatsService.seed(context);
            ScoreStatsService service = new();
        }
    }
}
