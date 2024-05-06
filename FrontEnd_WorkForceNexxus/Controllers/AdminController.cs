using System.Text;
using API_WorkForceNexxus.Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WFN.Models.Models.ViewModel;

namespace FrontEnd_WorkForceNexxus.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    Uri BaseAddress = new Uri("https://localhost:7124/api/");
    private readonly HttpClient _httpClient;
    
    public AdminController()
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = BaseAddress;
    }
    
    public async Task<IActionResult> Index()
    {
        DashboardViewModel dashboard = new DashboardViewModel();
        HttpResponseMessage response = await _httpClient.GetAsync("Dashboard/GetAllInfo");
        
        if (response.IsSuccessStatusCode)
        {
            string data = await response.Content.ReadAsStringAsync();
            dashboard = JsonConvert.DeserializeObject<DashboardViewModel>(data);
        }
        
        return View(dashboard);
    }
    
    [HttpGet]
    public IActionResult ChangePassword()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
    {
        string json = JsonConvert.SerializeObject(model);
        StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await _httpClient.PostAsync("User/GetAll", content);

        if (response.IsSuccessStatusCode)
        {
            TempData["Msg"] = "Password changed successfully!";
            return View();
        }
        else
        {
            TempData["FMsg"] = "Current password doesn't match, Failed to changed password!";
            return View(model);
        }
    }

}