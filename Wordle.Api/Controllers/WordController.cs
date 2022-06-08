using Microsoft.AspNetCore.Mvc;
using Wordle.Api.Data;
using Wordle.Api.Services;

namespace Wordle.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WordController : ControllerBase
{
    private readonly WordService _service;

    public WordController(WordService service)
    {
        _service = service;
    }

    [HttpGet("GetWordsPerPage")]
    public IEnumerable<Word> Get(int wordsPerPage, int pageNum, string wordFilter)
    {
        return _service.GetWordList(wordsPerPage, pageNum, wordFilter);
    }

    [HttpGet("GetTotalWordCount")] 
    public int Get(string wordFilter)
    {
        return _service.GetTotalWordCount(wordFilter);
    }
}

