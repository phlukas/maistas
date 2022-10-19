using Maistas.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Maistas.Controllers;
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly MaistasContext context;

    public HomeController(ILogger<HomeController> logger, MaistasContext context)
    {
        _logger = logger;
        this.context = context;
    }

    public IActionResult Index()
    {
        var x = context.Dishes.Any(x => x.Id == 1);
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
