using Microsoft.EntityFrameworkCore;

namespace System.Interview.TinyUrl.ByHash.DataLayer;

public class PostgreUrlRepository : IUrlLinkRepository
{
    public bool TryAdd(string hash, string url)
    {
        using var dbContext = new PostgreDbContext();

        var any = dbContext.UrlLinks.Any(x => x.Hash == hash);

        if (any)
            return false;

        dbContext.UrlLinks.Add(new UrlLinkEntity { Hash = hash, Url = url, CreatedAt = DateTimeOffset.UtcNow });
        dbContext.SaveChanges();

        return true;
    }

    public string? Get(string hash)
    {
        using var dbContext = new PostgreDbContext();

        return dbContext.UrlLinks.AsNoTracking().FirstOrDefault(x => x.Hash == hash)?.Url;
    }
}
