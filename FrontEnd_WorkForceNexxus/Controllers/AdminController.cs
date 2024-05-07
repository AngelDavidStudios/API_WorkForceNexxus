using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WFN.Models.Models;
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

    public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
    {
        // Serializa el modelo a un objeto JSON
        var json = JsonConvert.SerializeObject(model);
        var data = new StringContent(json, Encoding.UTF8, "application/json");

        // Realiza la llamada a la API
        HttpResponseMessage response = await _httpClient.PostAsync("User/ChangePassword", data);

        if (response.IsSuccessStatusCode)
        {
            TempData["Msg"] = "Password changed successfully!";
            return RedirectToAction("Index");
        }

        TempData["FMsg"] = "Current password doesn't match, Failed to changed password!";
        return View(model);
    }

    #region Holidays
    
    public async Task<IActionResult> Holidays()
    {
        List<HolidayModel> holidays = new List<HolidayModel>();
        HttpResponseMessage response = await _httpClient.GetAsync("Holiday/Holidays");
        
        if (response.IsSuccessStatusCode)
        {
            string data = await response.Content.ReadAsStringAsync();
            holidays = JsonConvert.DeserializeObject<List<HolidayModel>>(data);
        }
        
        return View(holidays);
    }
    
    public async Task<IActionResult> AddHoliday(HolidayModel model)
    {
        // Serializa el modelo a un objeto JSON
        var json = JsonConvert.SerializeObject(model);
        var data = new StringContent(json, Encoding.UTF8, "application/json");

        // Realiza la llamada a la API
        HttpResponseMessage response = await _httpClient.PostAsync("Holiday/AddHoliday", data);

        if (response.IsSuccessStatusCode)
        {
            TempData["Msg"] = "Holiday added successfully!";
            return RedirectToAction("Holidays");
        }

        TempData["FMsg"] = "Failed to add holiday!";
        return View(model);
    }
    
    public async Task<IActionResult> EditHoliday(Int64 id)
    {
        HolidayModel holiday = new HolidayModel();
        HttpResponseMessage response = await _httpClient.GetAsync($"Holiday/GetHoliday/{id}");
        
        if (response.IsSuccessStatusCode)
        {
            string data = await response.Content.ReadAsStringAsync();
            holiday = JsonConvert.DeserializeObject<HolidayModel>(data);
        }
        
        return View(holiday);
    }
    
    #endregion

    #region Awards
    
    public async Task<IActionResult> AwardList()
    {
        List<AwardModel> awards = new List<AwardModel>();
        HttpResponseMessage response = await _httpClient.GetAsync("Award/AwardList");
        
        if (response.IsSuccessStatusCode)
        {
            string data = await response.Content.ReadAsStringAsync();
            awards = JsonConvert.DeserializeObject<List<AwardModel>>(data);
        }
        
        return View(awards);
    }
    
    public async Task<IActionResult> AddAward(AwardModel model)
    {
        // Serializa el modelo a un objeto JSON
        var json = JsonConvert.SerializeObject(model);
        var data = new StringContent(json, Encoding.UTF8, "application/json");

        // Realiza la llamada a la API
        HttpResponseMessage response = await _httpClient.PostAsync("Award/AddAward", data);

        if (response.IsSuccessStatusCode)
        {
            TempData["Msg"] = "Award added successfully!";
            return RedirectToAction("AwardList");
        }

        TempData["FMsg"] = "Failed to add award!";
        return View(model);
    }
    
    public async Task<IActionResult> EditAward(Int64 id)
    {
        AwardModel award = new AwardModel();
        HttpResponseMessage response = await _httpClient.GetAsync($"Award/GetAward/{id}");
        
        if (response.IsSuccessStatusCode)
        {
            string data = await response.Content.ReadAsStringAsync();
            award = JsonConvert.DeserializeObject<AwardModel>(data);
        }
        
        return View(award);
    }
    
    #endregion
    
    #region Notice
    
    public async Task<IActionResult> NoticeList()
    {
        List<NoticeModel> notices = new List<NoticeModel>();
        HttpResponseMessage response = await _httpClient.GetAsync("Notice/NoticeList");
        
        if (response.IsSuccessStatusCode)
        {
            string data = await response.Content.ReadAsStringAsync();
            notices = JsonConvert.DeserializeObject<List<NoticeModel>>(data);
        }
        
        return View(notices);
    }
    
    public async Task<IActionResult> AddNotice(NoticeModel model)
    {
        // Serializa el modelo a un objeto JSON
        var json = JsonConvert.SerializeObject(model);
        var data = new StringContent(json, Encoding.UTF8, "application/json");

        // Realiza la llamada a la API
        HttpResponseMessage response = await _httpClient.PostAsync("Notice/AddNotice", data);

        if (response.IsSuccessStatusCode)
        {
            TempData["Msg"] = "Notice added successfully!";
            return RedirectToAction("NoticeList");
        }

        TempData["FMsg"] = "Failed to add notice!";
        return View(model);
    }
    
    public async Task<IActionResult> EditNotice(Int64 id)
    {
        NoticeModel notice = new NoticeModel();
        HttpResponseMessage response = await _httpClient.GetAsync($"Notice/GetNotice/{id}");
        
        if (response.IsSuccessStatusCode)
        {
            string data = await response.Content.ReadAsStringAsync();
            notice = JsonConvert.DeserializeObject<NoticeModel>(data);
        }
        
        return View(notice);
    }
    
    #endregion
}