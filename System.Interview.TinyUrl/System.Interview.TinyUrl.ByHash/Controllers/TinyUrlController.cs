using System.ComponentModel.DataAnnotations;
using System.Interview.TinyUrl.ByHash.BusinessLogic;
using System.Interview.TinyUrl.ByHash.DataLayer;
using Microsoft.AspNetCore.Mvc;

namespace System.Interview.TinyUrl.ByHash.Controllers;

[ApiController]
[Route("[controller]")]
public class TinyUrlController : Controller
{
    private readonly ILogger<TinyUrlController> _logger;
    private readonly IUrlLinkRepository urlLinkRepository;

    public TinyUrlController(ILogger<TinyUrlController> logger)
    {
        _logger = logger;
        urlLinkRepository = new PostgreUrlRepository();
    }

    [HttpGet("{hash}")]
    public IActionResult Get(string hash)
    {
        var fullUrl = urlLinkRepository.Get(hash);

        if (string.IsNullOrEmpty(fullUrl))
            return NotFound($"Url '{hash}' not found");

        return RedirectPermanent(fullUrl);
    }


    [HttpPost("url")]
    public IActionResult CreateUrl([Required] [FromForm] string inputUrl)
    {
        const string ShortUrlPrefix = "http://localhost:5095/tinyUrl/";
        var hash = HashGenerator.GenerateHash(inputUrl);

        var shortUri = $"{ShortUrlPrefix}{hash}";

        if (urlLinkRepository.TryAdd(hash, inputUrl))
            return Created(shortUri, new { OrigignalUrl = inputUrl, ShortUrl = shortUri });

        return Conflict(new { message = $"Hash '{hash}' already registered to '{inputUrl}'", inputUrl, shortUri });
    }
}
