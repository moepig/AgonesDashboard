using AgonesDashboard.Models.Kubernetes;
using AgonesDashboard.Models.Kubernetes.CustomResources.Agones;
using k8s;

namespace AgonesDashboard.Repositories.Kubernetes
{
    public class FleetRepository : IFleetRepository
    {
        private readonly ILogger<FleetRepository> _logger;

        private GenericClient _client;

        public FleetRepository(ILogger<FleetRepository> logger)
        {
            _logger = logger;

            var config = KubernetesClientConfiguration.BuildConfigFromConfigFile();
            var kubernetes = new k8s.Kubernetes(config);
            _client = new GenericClient(kubernetes, "agones.dev", "v1", "fleets");
        }

        public async Task<CustomResourceList<V1Fleet>> ListAsync()
        {
            var result = await _client.ListAsync<CustomResourceList<V1Fleet>>().ConfigureAwait(false);

            return result;
        }
    }

}
