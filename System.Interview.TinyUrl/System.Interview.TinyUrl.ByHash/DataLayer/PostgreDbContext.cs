using Microsoft.EntityFrameworkCore;

namespace System.Interview.TinyUrl.ByHash.DataLayer;

public class PostgreDbContext : DbContext
{
    public DbSet<UrlLinkEntity> UrlLinks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Database=UrlShortener;Username=postgres;Password=postgresPsw");
        base.OnConfiguring(optionsBuilder);
    }
}