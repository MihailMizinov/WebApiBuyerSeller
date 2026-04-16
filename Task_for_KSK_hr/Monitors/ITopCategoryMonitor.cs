using Microsoft.Extensions.Options;

namespace Task_for_KSK_hr.Monitors
{
    public interface ITopCategoryMonitor : IOptionsMonitor<TopCategoryOptions>
    {
        void Recalculate();
    }
}
