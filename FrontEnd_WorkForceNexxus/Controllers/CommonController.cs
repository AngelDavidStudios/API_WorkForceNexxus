using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WFN.Models.Models;

namespace FrontEnd_WorkForceNexxus.Controllers;

public class CommonController : Controller
{
    Uri BaseAddress = new Uri("https://localhost:7124/api/");
    private readonly HttpClient _httpClient;
    
    public CommonController()
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = BaseAddress;
    }
    
    public IActionResult Index()
    {
        return View();
    }

    #region Department
    
    public async Task<IActionResult> DepartmentList()
    {
        List<DepartmentModel> departments = new List<DepartmentModel>();
        HttpResponseMessage response = await _httpClient.GetAsync("Department/Departments");
        
        if (response.IsSuccessStatusCode)
        {
            string data = await response.Content.ReadAsStringAsync();
            departments = JsonConvert.DeserializeObject<List<DepartmentModel>>(data);
        }
        
        return View(departments);
    }
    
    #endregion

    #region Designation
    
public async Task<IActionResult> DesignationList()
{
    List<DepartmentModel> departments = new List<DepartmentModel>();
    HttpResponseMessage response = await _httpClient.GetAsync("Department/Departments");

    if (response.IsSuccessStatusCode)
    {
        string data = await response.Content.ReadAsStringAsync();
        departments = JsonConvert.DeserializeObject<List<DepartmentModel>>(data);
    }

    return View(departments);
}
    
    public async Task<IActionResult> AddDesignation(DesignationModel model)
    {
        // Serializa el modelo a un objeto JSON
        var json = JsonConvert.SerializeObject(model);
        var data = new StringContent(json, Encoding.UTF8, "application/json");

        // Realiza la llamada a la API
        HttpResponseMessage response = await _httpClient.PostAsync("Designation/AddDesignation", data);

        if (response.IsSuccessStatusCode)
        {
            TempData["Msg"] = "Designation added successfully!";
            return RedirectToAction("DesignationList");
        }

        TempData["FMsg"] = "Failed to add designation!";
        return View(model);
    }
    
    public async Task<IActionResult> EditDesignation(Int64 id)
    {
        DesignationModel designation = new DesignationModel();
        HttpResponseMessage response = await _httpClient.GetAsync($"Designation/GetDesignation/{id}");
        
        if (response.IsSuccessStatusCode)
        {
            string data = await response.Content.ReadAsStringAsync();
            designation = JsonConvert.DeserializeObject<DesignationModel>(data);
        }
        
        return View(designation);
    }
    
    #endregion
    
    #region Employee
    
    public async Task<IActionResult> EmployeeList()
    {
        List<EmployeeModel> employees = new List<EmployeeModel>();
        HttpResponseMessage response = await _httpClient.GetAsync("Employee/Employees");
        
        if (response.IsSuccessStatusCode)
        {
            string data = await response.Content.ReadAsStringAsync();
            employees = JsonConvert.DeserializeObject<List<EmployeeModel>>(data);
        }
        
        return View(employees);
    }
    
    public async Task<IActionResult> AddEmployee(EmployeeModel model)
    {
        // Serializa el modelo a un objeto JSON
        var json = JsonConvert.SerializeObject(model);
        var data = new StringContent(json, Encoding.UTF8, "application/json");

        // Realiza la llamada a la API
        HttpResponseMessage response = await _httpClient.PostAsync("Employee/AddEmployee", data);

        if (response.IsSuccessStatusCode)
        {
            TempData["Msg"] = "Employee added successfully!";
            return RedirectToAction("EmployeeList");
        }

        TempData["FMsg"] = "Failed to add employee!";
        return View(model);
    }
    
    public async Task<IActionResult> EditEmployee(Int64 id)
    {
        EmployeeModel employee = new EmployeeModel();
        HttpResponseMessage response = await _httpClient.GetAsync($"Employee/GetEmployee/{id}");
        
        if (response.IsSuccessStatusCode)
        {
            string data = await response.Content.ReadAsStringAsync();
            employee = JsonConvert.DeserializeObject<EmployeeModel>(data);
        }
        
        return View(employee);
    }
    
    #endregion
    
}