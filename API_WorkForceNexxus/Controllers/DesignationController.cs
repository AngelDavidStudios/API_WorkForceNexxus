using API_WorkForceNexxus.Data.Interfaces.Base;
using Microsoft.AspNetCore.Mvc;
using WFN.Models.Models;

namespace API_WorkForceNexxus.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DesignationController : ControllerBase
{
    private IBaseRepository<DesignationModel> _designationRepository;
    private readonly ILogger<DesignationController> _logger;
    
    public DesignationController(IBaseRepository<DesignationModel> designationRepository, ILogger<DesignationController> logger)
    {
        _logger = logger;
        _designationRepository = designationRepository;
    }
    
    [HttpGet]
    [Route("Designations")]
    public async Task<IActionResult> GetDesignations()
    {
        var designations = await _designationRepository.GetAllAsync();
        return Ok(designations);
    }

    [HttpPost]
    [Route("AddDesignation")]
    public async Task<IActionResult> AddDesignation([FromBody] DesignationModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _designationRepository.Add(model);
        await _designationRepository.SaveChangesAsync();
        return Ok(model);
    }

    [HttpPut]
    [Route("EditDesignation/{id}")]
    public async Task<IActionResult> EditDesignation(Int64 id, [FromBody] DesignationModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var designation = await _designationRepository.GetByIdAsync(id);
        if (designation == null)
        {
            return NotFound();
        }

        _designationRepository.Update(model);
        await _designationRepository.SaveChangesAsync();
        return Ok(designation);
    }

    [HttpDelete]
    [Route("DeleteDesignation/{id}")]
    public async Task<IActionResult> DeleteDesignation(Int64 id)
    {
        var designation = await _designationRepository.GetByIdAsync(id);
        if (designation == null)
        {
            return NotFound();
        }

        await _designationRepository.Delete(id);
        await _designationRepository.SaveChangesAsync();
        return Ok();
    }
}