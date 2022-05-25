using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wordle.api.Data;
using Wordle.api.Services;

namespace Wordle.api.Tests
{
    [TestClass]
    public class GameServiceTests : DatabaseBaseTests
    {
        [TestMethod]
        public void CreateGame()
        {
            using var context = new TestAppDbContext(Options);
            var service = new GameService(context);
            Word.SeedWords(context);

            Guid playerGuid = Guid.NewGuid();
            var game = service.CreateGame(playerGuid, Game.GameTypeEnum.Random);

            Assert.IsNotNull(game);
            Assert.AreEqual(playerGuid, game.Player.Guid);
            Assert.AreEqual(5, game.Word.Value.Length);
        }

    }
}

