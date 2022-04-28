using Wordle.api.Data;

namespace Wordle.api.Services
{
    public class ScoreStatsService
    {
        //The database var for access
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
                for (int i = 1; i <= 6; i++)
                {
                    context.ScoreStats.Add(new ScoreStat()
                    {
                        Score = i,
                        AverageSeconds = 0,
                        TotalGames = 0
                    });
                }
                context.SaveChanges();
            }
        }

        public void Update(int score, int seconds)
        {
            //Get the score that we want to update
            //find the row where the var scoreStat = the item in the first attribute
            var scoreStatId = _context.ScoreStats.First(x => x.Score == score);

            if(score < 0 || score > 6)
            {
                throw new ArgumentException("Score must be between 0 and 6");
            }

            if (seconds < 0)
            {
                throw new ArgumentException("Seconds must be greater than 0");
            }

            if (scoreStatId != null)
            {
                throw new ArgumentNullException("That user does not exist!");
            }

            scoreStatId.AverageSeconds = (scoreStatId.AverageSeconds * scoreStatId.TotalGames + seconds) / ++scoreStatId.TotalGames;

            //After we make our changes, we need to save the changes to the DB
            _context.SaveChanges();
            
        }
    }
}