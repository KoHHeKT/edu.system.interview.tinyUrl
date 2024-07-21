using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Interview.TinyUrl.ByHash.BusinessLogic;
using System.Interview.TinyUrl.ByHash.DataLayer;
using System.Interview.TinyUrl.ByHash.Models;
using Microsoft.AspNetCore.Mvc;

namespace System.Interview.TinyUrl.ByHash.Controllers;

[ApiController]
[Route("/")]
[Route("[controller]")]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;


    private IUrlLinkRepository urlLinkRepository;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
        urlLinkRepository = new PostgreUrlRepository();
    }

    [HttpGet]
    [Route("")]
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

}
