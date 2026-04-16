using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Task_for_KSK_hr.Monitors;

namespace Task_for_KSK_hr.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatsController : ControllerBase
    {
        private readonly IOptionsMonitor<TopCategoryOptions> _monitor;
        public StatsController(IOptionsMonitor<TopCategoryOptions> monitor)
        {
            _monitor = monitor;
        }

        [HttpGet]
        public IActionResult Get() => Ok(_monitor.CurrentValue);
    }
}
