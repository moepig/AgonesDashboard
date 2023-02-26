using AgonesDashboard.Models.Kubernetes;
using AgonesDashboard.Models.Kubernetes.CustomResources;
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
        }

        public async Task<CustomResourceList<GameServerResource>> ListAsync()
        {
            var client = GetClient();
            var result = await client.ListAsync<CustomResourceList<GameServerResource>>().ConfigureAwait(false);

            return result;
        }

        private GenericClient GetClient()
        {
            if (_client is not null)
            {
                return _client;
            }

            var config = KubernetesClientConfiguration.BuildConfigFromConfigFile();
            var kubernetes = new k8s.Kubernetes(config);
            _client = new GenericClient(kubernetes, "agones.dev", "v1", "gameservers");

            return _client;
        }
    }
}
