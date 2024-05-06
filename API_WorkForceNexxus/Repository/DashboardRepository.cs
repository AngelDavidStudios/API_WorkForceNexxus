using API_WorkForceNexxus.Data;
using API_WorkForceNexxus.Data.Interfaces;
using WFN.Models.Models;

namespace API_WorkForceNexxus.Repository;

public class DashboardRepository : IDashboardRepository
{
    private readonly AppDBContext _context;
    public DashboardRepository(AppDBContext context)
    {
        _context = context;
    }
    public IEnumerable<NoticeModel> LastFiveNotifications()
    {
        return _context.Notices.Take(5).OrderByDescending(x => x.CreateDate);
    }
    public IEnumerable<HolidayModel> LastFiveHolidays()
    {
        return _context.Holidays.Take(5).OrderByDescending(x => x.CreateDate);
    }

    public int totalAbsent()
    {
        var today = DateTime.Now.Date;
        return _context.Attendences
            .Where(x => x.Status == "Absense" && x.AttendenceDate.Date == today)
            .Count();
    }

    public int totalDepartment()
    {
        return _context.Depertments.ToList().Count();
    }

    public int totalEmplooyee()
    {
        return _context.Employees.ToList().Count();
    }

    public int TotalPresent()
    {
        var today = DateTime.Now.Date;
        return _context.Attendences
            .Where(x => x.Status == "Present" && x.AttendenceDate.Date == today)
            .Count();
    }
}