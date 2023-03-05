using AgonesDashboard.Models.Kubernetes;
using AgonesDashboard.Models.Kubernetes.CustomResources.Agones;
using k8s;

namespace AgonesDashboard.Repositories.Kubernetes
{
    public class GameServerRepository : IGameServerRepository
    {
        private readonly ILogger<GameServerRepository> _logger;

        private GenericClient _client;

        public GameServerRepository(ILogger<GameServerRepository> logger)
        {
            _logger = logger;

            var config = KubernetesClientConfiguration.BuildConfigFromConfigFile();
            var kubernetes = new k8s.Kubernetes(config);
            _client = new GenericClient(kubernetes, "agones.dev", "v1", "gameservers");
        }

        public async Task<CustomResourceList<V1GameServer>> ListAsync()
        {
            var result = await _client.ListAsync<CustomResourceList<V1GameServer>>().ConfigureAwait(false);

            return result;
        }
    }
}
