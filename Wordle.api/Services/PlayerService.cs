using Wordle.api.Data;

namespace Wordle.api.Services;

public class PlayerService
{
    //The database var for access
    private AppDbContext _context;

    public PlayerService(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Player> GetPlayers()
    {
        var result = _context.Players.OrderBy(x => x.AverageAttempts);
        return result;
    }

    public void Update(string name, int gameCount, double attemptNumber, double seconds)
    {
        if (attemptNumber < 0 || attemptNumber > 6)
        {
            throw new ArgumentException("Score must be between 1 and 6");
        }

        //chcek if the payer name is in the list
        if(_context.Players.Where(x => x.Name == name).Any())
        {
            var playerID = _context.Players.First(x => x.Name == name);
            int currentGameCount = playerID.GameCount;
            playerID.GameCount++;
            playerID.AverageAttempts = Math.Round((currentGameCount * playerID.AverageAttempts + attemptNumber) / playerID.GameCount, 2);
            playerID.AverageSeconds = Math.Round((currentGameCount * playerID.AverageSeconds + seconds) / playerID.GameCount, 2);
        }

        //if a player is not found, we need to add it
        else
        {
            _context.Players.Add(new Player()
            {
                Name = name,
                GameCount = gameCount,
                AverageAttempts = attemptNumber,
                AverageSeconds = seconds
            });
        }
        
        //After we make our changes, we need to save the changes to the DB
        _context.SaveChanges();
    }

    public void RemovePlayer(string name)
    {
        //chcek if the payer name is in the list
        if (_context.Players.Where(x => x.Name == name).Any())
        {
            _context.Remove(GetPlayers().First(x => x.Name == name));
        }

        _context.SaveChanges();
    }

    public static void Seed(AppDbContext context)
    {
        if (!context.Players.Any())
        {
            context.Players.Add(new Player()
            {
                Name = "Kelsey",
                GameCount = 10,
                AverageAttempts = 2.3
            });

            context.Players.Add(new Player()
            {
                Name = "Leona",
                GameCount = 5,
                AverageAttempts = 3.0
            });

            context.Players.Add(new Player()
            {
                Name = "Gray",
                GameCount = 3,
                AverageAttempts = 4.0
            });
        }

        context.SaveChanges();
    }
}

