using API_WorkForceNexxus.Data.Interfaces.Base;
using Microsoft.AspNetCore.Mvc;
using WFN.Models.Models;

namespace API_WorkForceNexxus.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NoticeController : ControllerBase
{
    private IBaseRepository<NoticeModel> _noticeRepository;
    private readonly ILogger<NoticeController> _logger;
    
    public NoticeController(IBaseRepository<NoticeModel> noticeRepository, ILogger<NoticeController> logger)
    {
        _noticeRepository = noticeRepository;
        _logger = logger;
    }
    
    [HttpGet]
    [Route("Notices")]
    public async Task<IActionResult> GetNotices()
    {
        var notices = await _noticeRepository.GetAllAsync();
        return Ok(notices);
    }

    [HttpPost]
    [Route("AddNotice")]
    public async Task<IActionResult> AddNotice([FromBody] NoticeModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _noticeRepository.Add(model);
        await _noticeRepository.SaveChangesAsync();
        return Ok(model);
    }

    [HttpPut]
    [Route("EditNotice/{id}")]
    public async Task<IActionResult> EditNotice(Int64 id, [FromBody] NoticeModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var notice = await _noticeRepository.GetByIdAsync(id);
        if (notice == null)
        {
            return NotFound();
        }

        _noticeRepository.Update(notice);
        await _noticeRepository.SaveChangesAsync();
        return Ok(notice);
    }

    [HttpDelete]
    [Route("DeleteNotice/{id}")]
    public async Task<IActionResult> DeleteNotice(Int64 id)
    {
        var notice = await _noticeRepository.GetByIdAsync(id);
        if (notice == null)
        {
            return NotFound();
        }

        await _noticeRepository.Delete(id);
        await _noticeRepository.SaveChangesAsync();
        return Ok();
    }
}