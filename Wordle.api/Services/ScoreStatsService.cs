using Wordle.api.Data;

namespace Wordle.api.serviecs;

public class ScoreStatsService
{
    private AppDbContext _context;

    public ScoreStatsService(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<ScoreStat> GetScoreStats()
    {
        var result = _context.ScoreStats.OrderBy(x => x.Score);
        return result;
    }

    public static void Seed(AppDbContext context)
    {
        if (!context.ScoreStats.Any())
        {
            for (int i = 0; i < 6; i++)
            {
                context.ScoreStats.Add(new ScoreStat()
                {
                    Score = i,
                    AverageSeconds = 0,
                    TotalGames = 0
                });
            }
        }
}