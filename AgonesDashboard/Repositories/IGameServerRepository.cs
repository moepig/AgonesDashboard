using AgonesDashboard.Models.Kubernetes;
using AgonesDashboard.Models.Kubernetes.CustomResources;

namespace AgonesDashboard.Repositories
{
    public interface IGameServerRepository
    {
        public Task<CustomResourceList<GameServerResource>> ListAsync();
    }
}
