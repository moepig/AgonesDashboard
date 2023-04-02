using AgonesDashboard.ViewModels.Allocator;

namespace AgonesDashboard.Services
{
    public interface IServiceService
    {
        public Task<AllocatorIndex> List();
    }
}
