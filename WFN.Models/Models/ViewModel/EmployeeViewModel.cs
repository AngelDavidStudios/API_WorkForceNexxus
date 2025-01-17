using System.Collections.ObjectModel;

namespace WFN.Models.Models.ViewModel;

public class EmployeeViewModel
{
    public EmployeeViewModel()
    {
        Notices = new Collection<NoticeModel>();
        Holidays = new Collection<HolidayModel>();
        RecentPaySlipList = new Collection<EmployeePaySlipModel>();
        RecentAwardList = new Collection<AwardModel>();
    }
    public int TotalAttendencInMonth { get; set; }
    public int TotalAbsenceInMonth { get; set; }
    public int TotalLeaveInMonth { get; set; }
    public EmployeeModel Employee { get; set; }
    public ICollection<NoticeModel> Notices { get; set; }
    public ICollection<HolidayModel> Holidays { get; set; }
    public ICollection<EmployeePaySlipModel> RecentPaySlipList{ get; set; }
    public ICollection<AwardModel> RecentAwardList { get; set; }
}