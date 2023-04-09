using AgonesDashboard.Models.Kubernetes;
using AgonesDashboard.Models.Kubernetes.CustomResources.Agones;

namespace AgonesDashboard.Repositories
{
    public interface IGameServerSetRepository
    {
        public Task<CustomResourceList<V1GameServerSet>> ListAsync();
        public Task<V1GameServerSet> GetAsync(string ns, string name);
    }
}
