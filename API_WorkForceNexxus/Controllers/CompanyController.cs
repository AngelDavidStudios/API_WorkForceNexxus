using API_WorkForceNexxus.Data.Interfaces.Base;
using Microsoft.AspNetCore.Mvc;
using WFN.Models.Models;

namespace API_WorkForceNexxus.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CompanyController : ControllerBase
{
    private IBaseRepository<CompanyModel> _companyRepository;
    private readonly ILogger<CompanyController> _logger;
    
    public CompanyController(IBaseRepository<CompanyModel> companyRepository, ILogger<CompanyController> logger)
    {
        _logger = logger;
        _companyRepository = companyRepository;
    }

    #region Read All

    [HttpGet]
    [Route("GetAll")]
    public async Task<ActionResult<IEnumerable<CompanyModel>>> GetAll()
    {
        _logger.LogInformation("Hello, this is Get All Async: Company");
        return Ok(await _companyRepository.GetAllAsync());
    }
    [HttpGet]
    [Route("GetById/{id}")]
    public async Task<ActionResult<CompanyModel>> GetById(Int64 id)
    {
        return Ok(await _companyRepository.GetByIdAsync(id));
    }
    [HttpGet]
    [Route("GetByName/{companyName}")]
    public async Task<ActionResult<CompanyModel>> GetByName(string companyName)
    {
        return Ok(await _companyRepository.FindByConditionAsync(x => x.CompanyName == companyName));
    }
    
    #endregion
    
    #region Create
    
    [HttpPost]
    [Route("Add")]
    public async Task<IActionResult> Add([FromBody] CompanyModel company)
    {
        _companyRepository.Add(company);
        return Ok(await _companyRepository.SaveChangesAsync());
    }
    
    #endregion
    
    #region Update
    
    [HttpPut]
    [Route("Update")]
    public async Task<IActionResult> Update([FromBody] CompanyModel company)
    {
        _companyRepository.Update(company);
        return Ok(await _companyRepository.SaveChangesAsync());
    }
    
    #endregion
    
    #region Delete
    
    [HttpDelete]
    [Route("Delete/{id}")]
    public async Task<IActionResult> Delete(Int64 id)
    {
        await _companyRepository.Delete(id);
        return Ok(await _companyRepository.SaveChangesAsync());
    }
    
    #endregion
}