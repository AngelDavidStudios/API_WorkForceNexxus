using WFN.Models.Models;

namespace API_WorkForceNexxus.Data.Interfaces;

public interface IDashboardRepository
{
    int totalEmplooyee();
    int totalDepartment();
    int TotalPresent();
    int totalAbsent();
    IEnumerable<NoticeModel> LastFiveNotifications();
    IEnumerable<HolidayModel> LastFiveHolidays();
}