using API_WorkForceNexxus.Data.Interfaces.Base;
using Microsoft.AspNetCore.Mvc;
using WFN.Models.Models;

namespace API_WorkForceNexxus.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private IBaseRepository<EmployeeModel> _employeeRepository;
    private readonly ILogger<EmployeeController> _logger;
    
    public EmployeeController(IBaseRepository<EmployeeModel> employeeRepository, ILogger<EmployeeController> logger)
    {
        _logger = logger;
        _employeeRepository = employeeRepository;
    }
    
    [HttpGet]
    [Route("Employees")]
    public async Task<IActionResult> GetEmployees()
    {
        var employees = await _employeeRepository.GetAllAsync();
        return Ok(employees);
    }

    [HttpPost]
    [Route("AddEmployee")]
    public async Task<IActionResult> AddEmployee([FromBody] EmployeeModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _employeeRepository.Add(model);
        await _employeeRepository.SaveChangesAsync();
        return Ok(model);
    }

    [HttpPut]
    [Route("EditEmployee/{id}")]
    public async Task<IActionResult> EditEmployee(Int64 id, [FromBody] EmployeeModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var employee = await _employeeRepository.GetByIdAsync(id);
        if (employee == null)
        {
            return NotFound();
        }

        _employeeRepository.Update(employee);
        await _employeeRepository.SaveChangesAsync();
        return Ok(employee);
    }

    [HttpDelete]
    [Route("DeleteEmployee/{id}")]
    public async Task<IActionResult> DeleteEmployee(Int64 id)
    {
        var employee = await _employeeRepository.GetByIdAsync(id);
        if (employee == null)
        {
            return NotFound();
        }

        await _employeeRepository.Delete(id);
        await _employeeRepository.SaveChangesAsync();
        return Ok();
    }
}