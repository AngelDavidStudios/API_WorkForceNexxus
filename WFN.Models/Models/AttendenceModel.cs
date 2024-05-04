using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WFN.Models.Models.Base;

namespace WFN.Models.Models;

public class AttendenceModel : BaseModel
{
    [Required]
    [DisplayName("Attendence Date")]
    
    public DateTime AttendenceDate { get; set; }
    [Required]
    public string Status { get; set; }
    public string Reason{ get; set; }

    [ForeignKey("EmployeeModel")]
    [Required]
    public int EmployeeId { get; set; }
    public EmployeeModel EmployeeModel { get; set; }
}