using API_WorkForceNexxus.Data.Interfaces.Base;
using Microsoft.AspNetCore.Mvc;
using WFN.Models.Models;

namespace API_WorkForceNexxus.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AwardController : ControllerBase
{
    private IBaseRepository<AwardModel> _awardRepository;
    private readonly ILogger<AwardController> _logger;
    
    public AwardController(IBaseRepository<AwardModel> awardRepository, ILogger<AwardController> logger)
    {
        _awardRepository = awardRepository;
        _logger = logger;
    }
    
    [HttpGet]
    [Route("Awards")]
    public async Task<IActionResult> GetAwards()
    {
        var awards = await _awardRepository.GetAllAsync();
        return Ok(awards);
    }

    [HttpPost]
    [Route("AddAward")]
    public async Task<IActionResult> AddAward([FromBody] AwardModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        model.Month = model.Date.ToString("MMMM");
        _awardRepository.Add(model);
        await _awardRepository.SaveChangesAsync();
        return Ok(model);
    }

    [HttpPut]
    [Route("EditAward/{id}")]
    public async Task<IActionResult> EditAward(Int64 id, [FromBody] AwardModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var award = await _awardRepository.GetByIdAsync(id);
        if (award == null)
        {
            return NotFound();
        }

        award.Gift = model.Gift;
        award.Price = model.Price;
        award.Date = model.Date;
        award.EmployeeId = model.EmployeeId;
        award.Month = model.Date.ToString("MMMM");

        _awardRepository.Update(award);
        await _awardRepository.SaveChangesAsync();
        return Ok(award);
}

    [HttpDelete]
    [Route("DeleteAward/{id}")]
    public async Task<IActionResult> DeleteAward(Int64 id)
    {
        var award = await _awardRepository.GetByIdAsync(id);
        if (award == null)
        {
            return NotFound();
        }

        await _awardRepository.Delete(id);
        await _awardRepository.SaveChangesAsync();
        return Ok();
    }
}