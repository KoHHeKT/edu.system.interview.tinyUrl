using System.Collections.Concurrent;

namespace System.Interview.TinyUrl.ByHash.DataLayer;

public class InMemoryUrlRepository : IUrlLinkRepository
{
    private readonly ConcurrentDictionary<string, string> hashToUrls = new();

    public bool TryAdd(string hash, string url)
    {
        return hashToUrls.TryAdd(hash, url);
    }

    public string? Get(string hash)
    {
        return hashToUrls.GetValueOrDefault(hash);
    }

    public static InMemoryUrlRepository Instance => instance ??= new InMemoryUrlRepository();

    private static InMemoryUrlRepository? instance;
}
