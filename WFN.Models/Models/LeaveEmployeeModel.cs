using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using WFN.Models.Models.Base;

namespace WFN.Models.Models;

public class LeaveEmployeeModel : BaseModel
{
    [ForeignKey("Employee")]
    [DisplayName("Employee")]
    public int EmployeeId { get; set; }
    public EmployeeModel Employee { get; set; }

    [ForeignKey("LeaveGroup")]
    [DisplayName("Leave Group")]
    public int LeaveGroupId { get; set; }
    public LeaveGroupModel LeaveGroup { get; set; }
}