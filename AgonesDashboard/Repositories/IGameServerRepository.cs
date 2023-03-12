using AgonesDashboard.Models.Kubernetes;
using AgonesDashboard.Models.Kubernetes.CustomResources.Agones;

namespace AgonesDashboard.Repositories
{
    public interface IGameServerRepository
    {
        public Task<CustomResourceList<V1GameServer>> ListAsync();
        public Task<V1GameServer> GetAsync(string ns, string name);
    }
}
