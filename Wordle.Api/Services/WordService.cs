using Wordle.Api.Data;
using System.Linq;
using static Wordle.Api.Data.Game;
using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;
using Wordle.Api.Dtos;

namespace Wordle.Api.Services;

public class WordService
{
    private readonly AppDbContext _context;

    public WordService(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Word> GetWordList(WordRequest searchRequest)
    {
        var result = _context.Words
            .Where(x => x.Value.StartsWith(searchRequest.WordFilter))
            .Skip( (searchRequest.PageNum - 1)* searchRequest.WordsPerPage)
            .Take(searchRequest.WordsPerPage)
            .OrderBy(x => x.Value);
        return result;
    }

    public int GetTotalWordCount(string wordFilter)
    {
        var result = _context.Words.Where(x => x.Value.StartsWith(wordFilter)).Count();
        return result;
    }
}