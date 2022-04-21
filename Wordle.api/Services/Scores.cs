namespace Wordle.api.Services
{
    public class Scores
    {

        public string Name { get; set; }    
        public int NumberGames { get; set; }
        public double AverageGuesses { get; set; }

        public Scores(string name, int numberGames, double averageGuesses)
        {
            Name = name;
            NumberGames = numberGames;
            AverageGuesses = averageGuesses;
        }
    }
}
