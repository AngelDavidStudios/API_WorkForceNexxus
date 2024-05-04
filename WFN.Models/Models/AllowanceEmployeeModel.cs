using System.ComponentModel.DataAnnotations.Schema;
using WFN.Models.Models.Base;

namespace WFN.Models.Models;

public class AllowanceEmployeeModel : BaseModel
{
    [ForeignKey("Employee")]
    public int EmployeeId { get; set; }
    public EmployeeModel Employee { get; set; }

    [ForeignKey("Allowance")]
    public int AllowanceId { get; set; }
    public AllowanceModel Allowance{ get; set; }
}