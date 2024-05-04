using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WFN.Models.Models.Base;

namespace WFN.Models.Models;

public class AllowanceTypeModel : BaseModel
{
    [Required]
    [DisplayName("Allowance Type Name")]
    public string AllowanceTypeName { get; set; }
}