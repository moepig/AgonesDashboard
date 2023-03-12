using AgonesDashboard.ViewModels.Fleet;

namespace AgonesDashboard.Services
{
    public interface IFleetService
    {
        public Task<FleetIndex?> List();
    }
}
