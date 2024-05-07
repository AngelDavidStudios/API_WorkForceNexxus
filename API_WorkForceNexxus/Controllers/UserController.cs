using API_WorkForceNexxus.Data.Interfaces.Base;
using Microsoft.AspNetCore.Mvc;
using WFN.Models.Models;
using WFN.Models.Models.ViewModel;

namespace API_WorkForceNexxus.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private IBaseRepository<UserModel> _userRepository;
    private readonly ILogger<UserController> _logger;
    
    public UserController(IBaseRepository<UserModel> userRepository, ILogger<UserController> logger)
    {
        _logger = logger;
        _userRepository = userRepository;
    }
    
    #region Read All
    
    [HttpGet]
    [Route("GetAll")]
    public async Task<ActionResult<IEnumerable<UserModel>>> GetAll()
    {
        _logger.LogInformation("Hello, this is Get All Async: User");
        return Ok(await _userRepository.GetAllAsync());
    }
    [HttpGet]
    [Route("GetById/{id}")]
    public async Task<ActionResult<UserModel>> GetById(Int64 id)
    {
        return Ok(await _userRepository.GetByIdAsync(id));
    }
    [HttpGet]
    [Route("GetByName/{email}")]
    public async Task<ActionResult<UserModel>> GetByName(string email)
    {
        return Ok(await _userRepository.FindByConditionAsync(x => x.Email == email));
    }
    
    #endregion
    
    #region Create
    
    [HttpPost]
    [Route("Add")]
    public async Task<IActionResult> Add([FromBody] UserModel user)
    {
        _userRepository.Add(user);
        return Ok(await _userRepository.SaveChangesAsync());
    }
    
    #endregion
    
    #region Update
    
    [HttpPut]
    [Route("Update")]
    public async Task<IActionResult> Update([FromBody] UserModel user)
    {
        _userRepository.Update(user);
        return Ok(await _userRepository.SaveChangesAsync());
    }
    
    #endregion
    
    #region Delete
    
    [HttpDelete]
    [Route("Delete/{id}")]
    public async Task<IActionResult> Delete(Int64 id)
    {
        await _userRepository.Delete(id);
        return Ok(await _userRepository.SaveChangesAsync());
    }
    
    #endregion
    
    #region Change Password
    
    [HttpPost]
    [Route("ChangePassword")]
    public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
    {
        var currentUserId = await CurrentUserId();
        var user = (await _userRepository.GetAllAsync()).FirstOrDefault(x => x.Id == currentUserId);

        if (user != null && model.CurrentPass == user.Password)
        {
            user.Password = model.NewPass;
            _userRepository.Update(user);
            return Ok(new { message = "Password changed successfully!" });
        }
        return BadRequest(new { message = "Current password doesn't match, Failed to changed password!" });
    }

    private async Task<int> CurrentUserId()
    {
        var user = (await _userRepository.GetAllAsync()).SingleOrDefault(x => x.Email == User.Identity.Name);
        if (user == null)
        {
            throw new Exception("User not found");
        }
        return user.Id;
    }

    private async Task<UserModel> CurrentUser()
    {
        var user = (await _userRepository.GetAllAsync()).SingleOrDefault(x => x.Email == User.Identity.Name);
        return user;
}
    
    #endregion
}