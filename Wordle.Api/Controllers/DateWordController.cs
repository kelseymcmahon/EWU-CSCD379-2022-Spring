using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;
using Wordle.Api.Data;
using Wordle.Api.Dtos;
using Wordle.Api.Services;
using static Wordle.Api.Data.Game;

namespace Wordle.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class DateWordController : Controller
{
    private readonly GameService _gameService;


    public DateWordController(GameService gameService)
    {
        _gameService = gameService;
    }

    [HttpGet]
    public string Get(int Month, int Day, int Year)
    {
        DateTime wordDate = new(Year, Month, Day);
        Word? dailyWord = _gameService.GetDailyWord(wordDate);
        if(dailyWord is null) throw new ArgumentNullException(nameof(dailyWord));
        return dailyWord.Value;
    }

    [HttpPost]
    public GameDto CreateGame([FromBody] CreateGameDto newGame)
    {
        var game = _gameService.CreateGame(new Guid(newGame.PlayerGuid), GameTypeEnum.WordOfTheDay, newGame.Date);
        return new GameDto(game);
    }

    public class CreateGameDto
    {
        public DateTime? Date { get; set; }
        public string PlayerGuid { get; set; } = null!;
    }

    public class CreateDateDTO
    {
        public int Month { get; set; }
        public int Day { get; set; }
        public int Year { get; set; }   
    }
}

internal record struct NewStruct(object Item1, object Item2)
{
    public static implicit operator (object, object)(NewStruct value)
    {
        return (value.Item1, value.Item2);
    }

    public static implicit operator NewStruct((object, object) value)
    {
        return new NewStruct(value.Item1, value.Item2);
    }
}