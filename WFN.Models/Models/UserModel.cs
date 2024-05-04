using System.ComponentModel.DataAnnotations;
using WFN.Models.Models.Base;

namespace WFN.Models.Models;

public class UserModel : BaseModel
{
    [EmailAddress]
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public string Role { get; set; }
    public int? UserId { get; set; }
}