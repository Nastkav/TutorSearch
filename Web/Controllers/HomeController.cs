using System.Diagnostics;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.Models;
using Web.Models.Shared;

namespace Web.Controllers;

public class HomeController : Controller
{
    public HomeController(ILogger<HomeController> logger)
    {
    }

    public IActionResult Index() => View();

    public IActionResult Privacy() => View();

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error(string msg = "") => View(new ErrorViewModel
    {
        RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
        Message = msg
    });

    public IActionResult About() => View();

    public IActionResult InDevelopment() => View();
}