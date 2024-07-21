namespace System.Interview.TinyUrl.ByHash.DataLayer;

public interface IUrlLinkRepository
{
    bool TryAdd(string hash, string url);
    string? Get(string hash);
}

//create DbContext for postgreSQL db
