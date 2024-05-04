using System.ComponentModel.DataAnnotations;

namespace WFN.Models.Models.Base;

public class BaseModel
{
    [Key]
    public int Id { get; set; }
    
    public DateTime CreateDate { get; set; }
    
    public DateTime UpdateDate { get; set; }

    public bool IsDelete { get; set; }
}