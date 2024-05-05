using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FrontEnd_WorkForceNexxus.Models;

namespace FrontEnd_WorkForceNexxus.Controllers;

public class HomeController : Controller
{
private readonly HttpClient _httpClient;

    public HomeController()
    {
        _httpClient = new HttpClient();
    }

    public IActionResult Index()
    {
        return View();
    }
    
}