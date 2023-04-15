using AgonesDashboard.Models.Kubernetes;
using AgonesDashboard.Models.Kubernetes.CustomResources.Agones.AutoScaling;

namespace AgonesDashboard.Repositories
{
    public interface IFleetAutoscalerRepository
    {
        public Task<CustomResourceList<V1FleetAutoscaler>> ListAsync();
        public Task<V1FleetAutoscaler> GetAsync(string ns, string name);
    }
}
