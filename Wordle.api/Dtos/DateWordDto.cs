using Wordle.api.Data;

namespace Wordle.api.Dtos;
public class DateWordDto
{
    public DateTime Date { get; set; }

    public double AverageSeconds { get; set; }
    public double AverageAttempts { get; set; }
    public int GameCount { get; set; }
    public bool PlayedByPlayer { get; set; }

    public DateWordDto(DateWord dateWord)
    {
        Date = dateWord.Date;
        AverageSeconds = dateWord.AverageSeconds;
        AverageAttempts = dateWord.AverageAttempts;
        GameCount = dateWord.GameCount;
    }
}
