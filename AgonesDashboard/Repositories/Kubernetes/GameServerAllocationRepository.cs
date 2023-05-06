using AgonesDashboard.Config;
using AgonesDashboard.Models.Kubernetes.CustomResources.Agones.Allocation;
using k8s;

namespace AgonesDashboard.Repositories.Kubernetes
{
    public class GameServerAllocationRepository : IGameServerAllocationRepository
    {
        private readonly ILogger<GameServerAllocationRepository> _logger;

        private GenericClient _client;

        public GameServerAllocationRepository(ILogger<GameServerAllocationRepository> logger, IConfig config)
        {
            _logger = logger;

            var k8sConfig = config.GetKuberneteClientConfiguration();
            var kubernetes = new k8s.Kubernetes(k8sConfig);
            _client = new GenericClient(kubernetes, "allocation.agones.dev", "v1", "gameserverallocations");
        }

        public async Task<V1GameServerAllocation> CreateAsync(string ns, V1GameServerAllocation allocation)
        {
            var result = await _client.CreateNamespacedAsync(allocation, ns).ConfigureAwait(false);

            return result;
        }
    }
}
