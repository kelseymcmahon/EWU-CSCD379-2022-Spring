using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Wordle.api.Services;

namespace Wordle.api.Tests
{
    [TestClass]
    public class LeaderboardMemoryServiceTest
    {
        [TestMethod]
        public void GetScores()
        {
            LeaderboardServiceMemory service = new();

            Assert.AreEqual(3, service.GetScores().Count());
            
        }

        [TestMethod]
        public void AddNewPlayer()
        {
            LeaderboardServiceMemory service = new();

            service.AddScore(new GameScore(1, "Leona"));
            service.AddScore(new GameScore(2, "Leona"));
            service.AddScore(new GameScore(20, "Leona"));
        }

        [TestMethod]
        public void AddScore()
        {
            LeaderboardServiceMemory service = new();

            service.AddScore(new GameScore(60, "Kelsey"));

            Assert.AreEqual(26, service.GetScores().First(x => x.Name == "Kelsey").NumberGames);
        }
    }
}