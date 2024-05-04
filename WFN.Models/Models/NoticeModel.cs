using System.ComponentModel.DataAnnotations;
using WFN.Models.Models.Base;

namespace WFN.Models.Models;

public class NoticeModel : BaseModel
{
    [Required]
    public string Subject { get; set; }

    [Required]
    public string Message { get; set; }
}