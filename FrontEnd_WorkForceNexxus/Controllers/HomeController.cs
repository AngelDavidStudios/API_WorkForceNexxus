using System.Diagnostics;
using System.Security.Claims;
using System.Text;
using API_WorkForceNexxus.Data.Interfaces.Base;
using Microsoft.AspNetCore.Mvc;
using FrontEnd_WorkForceNexxus.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using WFN.Models.Models;
using WFN.Models.Models.ViewModel;

namespace FrontEnd_WorkForceNexxus.Controllers;

[AllowAnonymous]
public class HomeController : Controller
{
    Uri BaseAddress = new Uri("https://localhost:7124/api/");
    private readonly HttpClient _httpClient;

    public HomeController()
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = BaseAddress;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {

        // Serializa el modelo a JSON
        string json = JsonConvert.SerializeObject(model);
        StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await _httpClient.GetAsync("User/GetAll");
        
        if (response.IsSuccessStatusCode)
        {
            // Deserializa la respuesta
            string responseJson = await response.Content.ReadAsStringAsync();
            List<UserModel> users = JsonConvert.DeserializeObject<List<UserModel>>(responseJson);

            if (users != null)
            {
                foreach (var user in users)
                {
                    var claims = new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.Name, user.Email));
                    string[] roles = user.Role.Split(",");

                    foreach (string role in roles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role));
                    }

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    var props = new AuthenticationProperties();

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, props);

                    if (user.Role == "Admin")
                        return RedirectToAction("Index", "Admin");
                    if (user.Role == "User")
                        return RedirectToAction("Index", "Employee");
                }
            }
            else
            {
                TempData["FFMsg"] = "Invalid Email or Password!";
            }

        }
        return View();
    }

}