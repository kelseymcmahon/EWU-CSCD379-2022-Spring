using Microsoft.AspNetCore.Mvc;
using Wordle.Api.Data;
using Wordle.Api.Dtos;
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
    public IEnumerable<Word> Get([FromBody]WordRequest searchRequest)
    {
        return _service.GetWordList(searchRequest);
    }

    [HttpGet("GetTotalWordCount")] 
    public int Get([FromBody]string wordFilter)
    {
        return _service.GetTotalWordCount(wordFilter);
    }
}

