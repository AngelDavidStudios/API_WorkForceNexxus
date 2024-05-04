using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualBasic;
using WFN.Models.Models.Base;

namespace WFN.Models.Models;

public class DepartmentModel : BaseModel
{
    public DepartmentModel()
    {
        Designations = new Collection<DesignationModel>();
    }
    [Required]
    public string Name { get; set; }
    public string Description { get; set; }

    public ICollection<DesignationModel> Designations { get; set; }
}