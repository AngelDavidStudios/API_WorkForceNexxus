using API_WorkForceNexxus.Data.Interfaces.Base;
using Microsoft.AspNetCore.Mvc;
using WFN.Models.Models;

namespace API_WorkForceNexxus.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HolidayController : ControllerBase
{
    private IBaseRepository<HolidayModel> _holidayRepository;
    private readonly ILogger<HolidayController> _logger;
    
    public HolidayController(IBaseRepository<HolidayModel> holidayRepository, ILogger<HolidayController> logger)
    {
        _holidayRepository = holidayRepository;
        _logger = logger;
    }

    [HttpGet]
    [Route("Holidays")]
    public async Task<IActionResult> Holidays()
    {
        DateTime dt = DateTime.Now;
        string currentMonth = dt.ToString("MMMM");
        var holidays = await _holidayRepository.GetAllAsync();
        return Ok(holidays.Where(x => x.Month == currentMonth));
    }

    [HttpPost]
    [Route("GetHolidays")]
    public async Task<IActionResult> GetHolidays(string month)
    {
        var holidays = await _holidayRepository.GetAllAsync();
        return Ok(holidays.Where(x => x.Month == month));
    }

    [HttpPost]
    [Route("AddHoliday")]
    public async Task<IActionResult> AddHoliday(HolidayModel model)
    {
        if (ModelState.IsValid)
        {
            model.Month = model.Date.ToString("MMMM");
            model.Day = model.Date.DayOfWeek.ToString();
            _holidayRepository.Add(model);
            await _holidayRepository.SaveChangesAsync();
            return Ok(model);
        }

        return BadRequest(ModelState);
    }

    [HttpPut]
    [Route("EditHoliday/{id}")]
    public async Task<IActionResult> EditHoliday(Int64 id, [FromBody] HolidayModel model)
    {
        model.Month = model.Date.ToString("MMMM");
        model.Day = model.Date.DayOfWeek.ToString();
        var holiday = await _holidayRepository.GetByIdAsync(id);

        if (ModelState.IsValid && holiday != null)
        {
            holiday.Date = model.Date;
            holiday.Day = model.Day;
            holiday.Month = model.Month;
            _holidayRepository.Update(holiday);
            await _holidayRepository.SaveChangesAsync();
            return Ok(holiday);
        }

        return BadRequest(ModelState);
    }

[HttpDelete]
[Route("DeleteHoliday/{id}")]
public async Task<IActionResult> DeleteHoliday(Int64 id)
{
    var holiday = await _holidayRepository.GetByIdAsync(id);
    if (holiday == null)
    {
        return NotFound();
    }

    await _holidayRepository.Delete(id);
    await _holidayRepository.SaveChangesAsync();
    return Ok();
    }
}
