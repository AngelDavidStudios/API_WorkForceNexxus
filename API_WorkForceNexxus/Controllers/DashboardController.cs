using API_WorkForceNexxus.Data.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WFN.Models.Models.ViewModel;

namespace API_WorkForceNexxus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardRepository _dashboardRepository;
        
        public DashboardController(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }
        
        [HttpGet]
        [Route("GetAllInfo")]
        public async Task<IActionResult> GetDashboard()
        {
            DashboardViewModel dashboard = new DashboardViewModel();
            dashboard.TotalEmployee = _dashboardRepository.totalEmplooyee();
            dashboard.TotalDept = _dashboardRepository.totalDepartment();
            dashboard.PresentCountToday = _dashboardRepository.TotalPresent();
            dashboard.AbsenceCountToday = _dashboardRepository.totalAbsent();
            dashboard.Notices = _dashboardRepository.LastFiveNotifications().ToList();
            dashboard.Holidays = _dashboardRepository.LastFiveHolidays().ToList();
            return Ok(dashboard);
        }
    }
}
