using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WFN.Models.Models.Base;

namespace WFN.Models.Models;

public class CompanyModel : BaseModel
{
    public string Logo { get; set; }

    [DisplayName("Store Name")]
    [Required]
    public string CompanyName { get; set; }

    [EmailAddress]
    [Required]
    public string Email { get; set; }
    [Required]
    public string Web { get; set; }

    [Phone]
    [Required]
    public string Phone { get; set; }

    [Required]
    public string Currency { get; set; }
    [Required]
    public string Address { get; set; }
}