using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WFN.Models.Models.Base;

namespace WFN.Models.Models;

public class AwardModel : BaseModel
{
    [Required]
    [DisplayName("Award Title")]
    public string AwardTitle { get; set; }

    [Required]
    public string Gift { get; set; }
    [Required]
    public double Price { get; set; }
    [Required]
    public DateTime Date{ get; set; }
    public string Month { get; set; }

    [ForeignKey("EmployeeModel")]
    [Required]
    public int EmployeeId { get; set; }
    public EmployeeModel EmployeeModel { get; set; }
}