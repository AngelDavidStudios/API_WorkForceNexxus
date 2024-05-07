using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WFN.Models.Models;

namespace FrontEnd_WorkForceNexxus.Controllers;

[Authorize(Roles = "Admin")]
public class CompanyController : Controller
{
    Uri BaseAddress = new Uri("https://localhost:7124/api/");
    private readonly HttpClient _httpClient;
    
    public CompanyController()
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = BaseAddress;
    }
    
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        return View(new CompanyModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Index(CompanyModel model, IFormFile logoPostedFileBase)
    {
        if (!ModelState.IsValid) return View(model);
        if (logoPostedFileBase != null && logoPostedFileBase.Length > 0)
        {
            var path = Path.Combine(
                Directory.GetCurrentDirectory(), "wwwroot/Images",
                logoPostedFileBase.FileName);
    
            using (var stream = new FileStream(path, FileMode.Create))
            {
                logoPostedFileBase.CopyTo(stream);
            }
            model.Logo = $"/Images/{logoPostedFileBase.FileName}";
        }
        else
        {
            var response = await _httpClient.GetAsync($"Company/GetById/{model.Id}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var settings = JsonConvert.DeserializeObject<CompanyModel>(result);
                if (settings != null)
                    model.Logo = settings.Logo;
            }
        }
    
        var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
        var postResponse = await _httpClient.PostAsync("Company/Add", content);
        if (postResponse.IsSuccessStatusCode)
        {
            return RedirectToAction("index");
        }
        return View(model);
    }
    
    
    
}