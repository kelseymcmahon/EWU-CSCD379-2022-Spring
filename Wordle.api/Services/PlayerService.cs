using Wordle.api.Data;

namespace Wordle.api.Services;

public class PlayersService
{
    private readonly AppDbContext _context;

    public PlayersService(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Player> GetPlayers()
    {
        var result = _context.Players.OrderBy(x => x.Name);
        return result;
    }

    public IEnumerable<Player> GetTop10Players()
    {
        var result = _context.Players
            .OrderBy(x => x.AverageAttempts)
            .Take(10);
        return result;
    }

    public void Update(string name, int attempts, int seconds)
    {
        if (attempts < 1 || attempts > 6)
        {
            throw new ArgumentException("attempts not in proper range");
        }
        if (seconds < 0)
        {
            throw new ArgumentException("seconds not in proper range");
        }

        var player2 = _context.Players;

        var player = player2.FirstOrDefault(x => x.Name == name);


        if (player == null)
        {
            _context.Players.Add(new Player()
            {
                Name = name,
                GameCount = 1,
                AverageAttempts = attempts,
                AverageSecondsPerGame = seconds
            });
        }
        else
        {
            player.AverageSecondsPerGame = (player.AverageSecondsPerGame * player.GameCount + seconds) / (player.GameCount + 1);
            player.AverageAttempts = (player.AverageAttempts * player.GameCount + attempts) / ++player.GameCount;

        }

        _context.SaveChanges();
    }

    public static void Seed(AppDbContext context)
    {
        if (!context.Players.Any())
        {
            context.Players.Add(new Player()
            { Name = "Inigo Montoya", GameCount = 2, AverageAttempts = 2, AverageSecondsPerGame = 31 });

            context.Players.Add(new Player()
            { Name = "Chase", GameCount = 12, AverageAttempts = 2, AverageSecondsPerGame = 90 });

            context.Players.Add(new Player()
            { Name = "Georgie", GameCount = 20, AverageAttempts = 4, AverageSecondsPerGame = 18 });

            context.Players.Add(new Player()
            { Name = "Gunther", GameCount = 15, AverageAttempts = 3, AverageSecondsPerGame = 155 });

            context.Players.Add(new Player()
            { Name = "Leona", GameCount = 8, AverageAttempts = 4, AverageSecondsPerGame = 111 });

            context.Players.Add(new Player()
            { Name = "Orange", GameCount = 20, AverageAttempts = 1, AverageSecondsPerGame = 222 });

            context.Players.Add(new Player()
            { Name = "Saria", GameCount = 5, AverageAttempts = 2, AverageSecondsPerGame = 105 });

            context.Players.Add(new Player()
            { Name = "Zelda", GameCount = 3, AverageAttempts = 5, AverageSecondsPerGame = 18 });

            context.Players.Add(new Player()
            { Name = "Midori", GameCount = 102, AverageAttempts = 6, AverageSecondsPerGame = 20 });

            context.Players.Add(new Player()
            { Name = "Agnis", GameCount = 4, AverageAttempts = 2, AverageSecondsPerGame = 31 });
          
            context.SaveChanges();
        }
    }
}

