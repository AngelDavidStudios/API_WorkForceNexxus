using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WFN.Models.Models.ViewModel;

public class ChangePasswordViewModel
{
    [Required]
    [DisplayName("Current Password")]
    public string CurrentPass { get; set; }

    [Required]
    [DisplayName("New Password")]
    public string NewPass { get; set; }

    [Required]
    [DisplayName("Confirm Password")]
    [Compare("NewPass")]
    public string ConfirmPass { get; set; }
}