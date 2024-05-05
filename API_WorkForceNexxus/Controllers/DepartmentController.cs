using API_WorkForceNexxus.Data.Interfaces.Base;
using Microsoft.AspNetCore.Mvc;
using WFN.Models.Models;

namespace API_WorkForceNexxus.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DepartmentController : ControllerBase
{
    private IBaseRepository<DepartmentModel> _departmentRepository;
    private readonly ILogger<DepartmentController> _logger;
    
    public DepartmentController(IBaseRepository<DepartmentModel> departmentRepository, ILogger<DepartmentController> logger)
    {
        _logger = logger;
        _departmentRepository = departmentRepository;
    }
    
    [HttpGet]
    [Route("Departments")]
    public async Task<IActionResult> GetDepartments()
    {
        var departments = await _departmentRepository.GetAllAsync();
        return Ok(departments);
    }

    [HttpPost]
    [Route("AddDepartment")]
    public async Task<IActionResult> AddDepartment([FromBody] DepartmentModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _departmentRepository.Add(model);
        await _departmentRepository.SaveChangesAsync();
        return Ok(model);
    }

    [HttpPut]
    [Route("EditDepartment/{id}")]
    public async Task<IActionResult> EditDepartment(Int64 id, [FromBody] DepartmentModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var department = await _departmentRepository.GetByIdAsync(id);
        if (department == null)
        {
            return NotFound();
        }

        _departmentRepository.Update(model);
        await _departmentRepository.SaveChangesAsync();
        return Ok(department);
    }

    [HttpDelete]
    [Route("DeleteDepartment/{id}")]
    public async Task<IActionResult> DeleteDepartment(Int64 id)
    {
        var department = await _departmentRepository.GetByIdAsync(id);
        if (department == null)
        {
            return NotFound();
        }

        await _departmentRepository.Delete(id);
        await _departmentRepository.SaveChangesAsync();
        return Ok();
    }
    
}