using AgonesDashboard.Models.Kubernetes;
using AgonesDashboard.Models.Kubernetes.CustomResources.Agones;

namespace AgonesDashboard.Repositories
{
    public interface IFleetRepository
    {
        public Task<CustomResourceList<V1Fleet>> ListAsync();
        public Task<V1Fleet> GetAsync(string ns, string name);
    }
}
