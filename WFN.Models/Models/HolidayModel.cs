using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WFN.Models.Models.Base;

namespace WFN.Models.Models;

public class HolidayModel : BaseModel
{
    [Required]
    public string Month { get; set; }
    [Required]
    public DateTime Date { get; set; }
    [Required]
    public string Day { get; set; }
    [Required]
    [DisplayName("Occasion")]
    public string Occesion { get; set; }
}