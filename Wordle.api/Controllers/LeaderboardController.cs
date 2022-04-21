using Microsoft.AspNetCore.Mvc;
using Wordle.api.Services;

namespace Wordle.api.Controllers;

[ApiController]
[Route("[controller]")]
public class LeaderboardController
{
    private readonly ILogger<LeaderboardController> _logger;
    private readonly ILeaderboardService _leaderboardService;

    public LeaderboardController(ILogger<LeaderboardController> logger,
        ILeaderboardService leaderboardService)
    {
        _logger = logger;
        _leaderboardService = leaderboardService;
    }

    [HttpGet]
    public IEnumerable<Scores> Get(string name)
    {
        _logger.LogInformation("LeaderboardController.Get()");

        List<Scores> results = new()
        {
            new Scores("Kelsey", 25, 2.6),
            new Scores("Ralph", 30, 3.4),
            new Scores("Gene", 50, 4.1),
        };

        return results;
    }

    [HttpPost]
    public void Post([FromBody] GameScore score)
    {

    }

}

