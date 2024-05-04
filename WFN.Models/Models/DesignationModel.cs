using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WFN.Models.Models.Base;

namespace WFN.Models.Models;

public class DesignationModel : BaseModel
{
    [Required]
    public string Name { get; set; }

    [ForeignKey("DepartmentModel")]
    [DisplayName("Department")]
    public int DepertmentId { get; set; }
    public DepartmentModel DepertmentModel { get; set; }
}