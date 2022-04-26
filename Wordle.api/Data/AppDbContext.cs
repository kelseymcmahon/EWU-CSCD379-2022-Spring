using Microsoft.EntityFrameworkCore;

namespace Wordle.api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<ScoreStat> ScoreStats { get; set; } = null!;
}
