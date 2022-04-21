namespace Wordle.api.Services
{
    public class LeaderboardServiceMemory : ILeaderboardService
    {
        private static readonly List<Scores> _scores = new();

        public LeaderboardServiceMemory()
        {
            if (!_scores.Any())
            {
                List<Scores> results = new();

                _scores.Add(new Scores("Kelsey", 25, 4.6));
                _scores.Add(new Scores("Ralph", 30, 3.4));
                _scores.Add(new Scores("Gene", 50, 4.1));
            };
        }
    }

    public void AddScore(GameScore gameScore)
    {
        var score = _scores.FirstOrDefault(f => f.name == gameScore.Name);

        if (score is not null)
        {
            score.NumberGames++;
            score.AverageGuesses = (score.NumberGames * score.AverageGuesses) + gameScore.Score);   //finish this
        }
        
    }

    public IEnumerable<Scores> GetScores()
        {
            return _scores;
        }
    }
}
