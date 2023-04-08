using AgonesDashboard.ViewModels.Service;

namespace AgonesDashboard.Services
{
    public interface IServiceService
    {
        public Task<ServiceIndex> List();
    }
}
