using System.Collections.Concurrent;
using System.Net;
using System.Text.Json;

namespace HttpRequest.PerfTest;

internal static class Program
{
    private static readonly ConcurrentQueue<HttpResponseMessage> responseQueue = new();

    private static int cnt = 0;

    static async Task Main(string[] args)
    {
        var stat = new Dictionary<HttpStatusCode, int>();

        var taskList = new List<Task>();
        var parallelTaskCount = 10;
        for (int i = 0; i < parallelTaskCount; i++)
        {
            var task = Task.Run(() => SendRequest(new HttpClient()));
            taskList.Add(task);
        }

        _ = Task.Run(() => HandleStatsRoutine(stat));

        await Task.Delay(-1);
    }

    private static void HandleStatsRoutine(Dictionary<HttpStatusCode, int> stat, CancellationToken ct = default)
    {
        while (!ct.IsCancellationRequested)
        {
            if (!responseQueue.TryDequeue(out var httpResponseMessage))
                SpinWait.SpinUntil(() => responseQueue.TryDequeue(out httpResponseMessage));

            cnt++;
            SaveStats(stat, httpResponseMessage!);
            PrintStats(cnt, stat);
        }
    }

    private static async Task SendRequest(HttpClient httpClient)
    {
        while (true)
        {
            var content = BuildContent(Guid.NewGuid().ToString());
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, $"http://localhost:5095/tinyUrl/url") { Content = content };
            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);
            responseQueue.Enqueue(httpResponseMessage);
        }
    }

    private static void PrintStats(int cnt, Dictionary<HttpStatusCode, int> stat)
    {
        if (cnt % 100 == 0)
            Console.WriteLine($"cnt:{cnt} {JsonSerializer.Serialize(stat)}");
    }

    private static void SaveStats(Dictionary<HttpStatusCode, int> stat, HttpResponseMessage httpResponseMessage)
    {
        stat.TryAdd(httpResponseMessage.StatusCode, 0);
        stat[httpResponseMessage.StatusCode]++;
    }

    private static FormUrlEncodedContent BuildContent(string urlSuffix)
    {
        var urlTemplate = "http://google.com/{0}";
        var url = string.Format(urlTemplate, urlSuffix);
        var dictionary = new Dictionary<string, string> { ["inputUrl"] = url };
        var content = new FormUrlEncodedContent(dictionary);
        return content;
    }
}
