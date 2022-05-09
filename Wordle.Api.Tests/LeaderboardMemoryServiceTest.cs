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
        public void AddScore_AddsNewPlayer()
        {
            LeaderboardServiceMemory sut = new();
            sut.AddScore(new GameScore(1, "test"));
            Assert.AreEqual(3, sut.GetScores().Count());
        }

        [TestMethod]
        public void AddScore_UpdatesExistingPlayer()
        {
            LeaderboardServiceMemory sut = new();
            sut.AddScore(new GameScore(5, "Ralph"));
            Assert.AreEqual(31, sut.GetScores().First(x => x.Name == "Ralph").NumberGames);
        }
    }
}