using AgonesDashboard.Config;
using AgonesDashboard.Models.Kubernetes;
using AgonesDashboard.Models.Kubernetes.CustomResources.Agones.AutoScaling;
using k8s;

namespace AgonesDashboard.Repositories.Kubernetes
{
    public class FleetAutoscalerRepository : IFleetAutoscalerRepository
    {

        private readonly ILogger<FleetAutoscalerRepository> _logger;

        private GenericClient _client;

        public FleetAutoscalerRepository(ILogger<FleetAutoscalerRepository> logger, IConfig config)
        {
            _logger = logger;

            var k8sConfig = config.GetKuberneteClientConfiguration();
            var kubernetes = new k8s.Kubernetes(k8sConfig);
            _client = new GenericClient(kubernetes, "autoscaling.agones.dev", "v1", "fleetautoscalers");
        }

        public async Task<CustomResourceList<V1FleetAutoscaler>> ListAsync()
        {
            var result = await _client.ListAsync<CustomResourceList<V1FleetAutoscaler>>().ConfigureAwait(false);

            return result;
        }
        public async Task<V1FleetAutoscaler> GetAsync(string ns, string name)
        {
            var result = await _client.ReadNamespacedAsync<V1FleetAutoscaler>(ns, name).ConfigureAwait(false);

            return result;
        }
    }
}
