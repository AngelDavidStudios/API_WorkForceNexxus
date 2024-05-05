using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WFN.Models.Models.Base;

namespace WFN.Models.Models;

public class LeaveTypeModel : BaseModel
{
    [Required]
    [DisplayName("Leave Type Name")]
    public string LeaveTypeName { get; set; }
}