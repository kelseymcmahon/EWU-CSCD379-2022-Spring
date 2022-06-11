using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wordle.Api.Data;
using Wordle.Api.Services;

namespace Wordle.Api.Tests;

[TestClass]
public class WordServiceTests : DatabaseBaseTests
{
    [TestMethod]
    public void GetWordList_GetWordWithNull()
    {
        using var context = new TestAppDbContext(Options);
        WordService service = new WordService(context);
        Word.SeedWords(context, "WordsTest.csv");

        IEnumerable<Word> words = service.GetWordList(10, 1, "?????");
        Assert.AreEqual(10, words.Count());

    }

    [TestMethod]
    public void GetTotalWordCount()
    {
        using var context = new TestAppDbContext(Options);
        WordService service = new WordService(context);
        Word.SeedWords(context, "WordsTest.csv");

        int count = service.GetTotalWordCount("?????");
        Assert.AreEqual(count, 72);

        int count2 = service.GetTotalWordCount("abb");
        Assert.AreEqual(count2, 5);
    }
}