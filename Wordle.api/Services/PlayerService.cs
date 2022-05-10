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
        var result = _context.Players.OrderBy(x => x.Name);
        return result;
    }

    public void Update(string name, int gameCount, double averageAttempts)
    {
        if (averageAttempts < 0 || averageAttempts > 6)
        {
            throw new ArgumentException("Score must be between 1 and 6");
        }

        //Get the score that we want to update
        //find the row where the var scoreStat = the item in the first attribute
        var playerID = _context.Players.First(x => x.Name == name);

        //if a player is not found, we need to add it
        if(playerID == null)
        {
            _context.Players.Add(new Player()
            {
                Name = name,
                GameCount = gameCount,
                AverageAttempts = averageAttempts
            });
        }
        else
        {
            playerID.GameCount++;
            playerID.AverageAttempts = playerID.AverageAttempts + averageAttempts / playerID.GameCount;
        }
        
        //After we make our changes, we need to save the changes to the DB
        _context.SaveChanges();
    }

    public static void Seed(AppDbContext context)
    {
        if (!context.Players.Any())
        {
            for (int i = 1; i <= 10; i++)
            {
                context.Players.Add(new Player()
                {
                    Name = "",
                    GameCount = 0,
                    AverageAttempts = 0
                });
            }
            context.SaveChanges();
        }
    }
}

