using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WFN.Models.Models.Base;

namespace WFN.Models.Models;

public class EmployeeModel : BaseModel
{
    // Personal 
    [Required]
    public string Name { get; set; }
    public string Mobile { get; set; }
    [EmailAddress]
    [Required]
    public string Email { get; set; }
    public string ImagePath { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Gender { get; set; }
    [DisplayName("Present Address")]
    public string PresentAddress { get; set; }
    [DisplayName("Permanent Address")]
    public string PermanentAddress { get; set; }


// Official RH 
    [DisplayName("Basic Salary")]
    public double BasicSalary { get; set; }
    public bool Status { get; set; }
    [DisplayName("Join Date")]
    [Required]
    public DateTime JoiningDate { get; set; }
    [DisplayName("Resign Date")]
    public DateTime ResignDate { get; set; }

    [ForeignKey("DepartmentModel")]
    [DisplayName("Depertment")]
    public int DepertmentId { get; set; }
    public DepartmentModel DepertmentModel { get; set; }

    [ForeignKey("DesignationModel")]
    [DisplayName("Designation")]
    public int DegisnationId { get; set; }
    public DesignationModel DesignationModel { get; set; }


// Bank Account
    [DisplayName("Account Name")]
    public string AccountName { get; set; }
    [DisplayName("Account Number")]
    public string AccountNumber { get; set; }
    [DisplayName("SWIFT Code")]
    public string SWIFTCode { get; set; }
    public string Branch { get; set; }


// Files or Documents
    public string CV { get; set; }
    public string NationalId{ get; set; }
    public string Other { get; set; }
    
    
    public EmployeeModel()
    {
        DateOfBirth = DateTime.Now.AddYears(-30);
        JoiningDate = DateTime.UtcNow;
        ResignDate = DateTime.UtcNow;
        ImagePath = AppData.defaultUser;
    }
}