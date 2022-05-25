using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;
using Wordle.api.Data;
using Wordle.api.Dtos;
using Wordle.api.Services;
using static Wordle.api.Data.Game;

namespace Wordle.api.Controllers;

[ApiController]
[Route("[controller]")]
public class DateWordController : Controller
{
    private readonly GameService _gameService;


    public DateWordController(GameService gameService)
    {
        _gameService = gameService;
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
