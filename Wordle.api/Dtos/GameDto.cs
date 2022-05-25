using Wordle.api.Data;

namespace Wordle.api.Dtos;
public class GameDto
{
    public string Word { get; set; }
    public int GameId { get; set; }
    public bool WasPlayed { get; set; }

    public GameDto(Game game)
    {
        Word = game.Word.Value;
        GameId = game.GameId;
        WasPlayed = game.DateEnded.HasValue;
    }
}


