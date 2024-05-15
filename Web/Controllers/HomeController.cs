using System.Diagnostics;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.Models;
using Web.Models.Shared;

namespace Web.Controllers;

public class HomeController : Controller
{
    public IActionResult Privacy()
    {
        var result = View();
        return result;
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error(string msg = "") => View(new ErrorVm
    {
        RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
        Message = msg
    });

    public IActionResult About() => View();

    public IActionResult InDevelopment() => View();
}