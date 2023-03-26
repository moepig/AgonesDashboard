using AgonesDashboard.ViewModels.Fleet;

namespace AgonesDashboard.Services
{
    public interface IFleetService
    {
        public Task<FleetIndex> List();
        public Task<Detail> Detail(string ns, string name);
        public Task<Allocation> Allocate(string ns, string fleetName);
    }
}
