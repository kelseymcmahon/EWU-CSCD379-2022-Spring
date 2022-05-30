using System.ComponentModel.DataAnnotations.Schema;

namespace Wordle.api.Data;
public class DateWord
{
    public int DateWordId { get; set; }
    public DateTime Date { get; set; }

    public Word Word { get; set; } = null!;
    public int WordId { get; set; }
    public double AverageSeconds { get; set; }  
    public double AverageAttempts { get; set; }
    public int GameCount { get; set; }

}