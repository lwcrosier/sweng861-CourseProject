using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FlightBooking.Models;
using Newtonsoft.Json;

namespace FlightBooking.Controllers;

/*
 * Default, Boilerplate class used as the default home page
 */
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    /*
     * Index returns Views/FlightBooking/Index.cshtml as this class is meant to be unused unless testing
     */
    public IActionResult Index()
    {
        //return View();
        return RedirectToAction("Index", "FlightBooking", new { area = "" });
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
