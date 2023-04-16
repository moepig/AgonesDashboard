using AgonesDashboard.ViewModels.Fleet;

namespace AgonesDashboard.Services
{
    public interface IFleetService
    {
        public Task<FleetIndex> ListAsync();
        public Task<Detail> DetailAsync(string ns, string name);
        public Task<Allocation> AllocateAsync(string ns, string fleetName);
    }
}
