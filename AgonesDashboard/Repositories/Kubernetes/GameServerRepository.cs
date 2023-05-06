using AgonesDashboard.Config;
using AgonesDashboard.Models.Kubernetes;
using AgonesDashboard.Models.Kubernetes.CustomResources.Agones;
using k8s;

namespace AgonesDashboard.Repositories.Kubernetes
{
    public class GameServerRepository : IGameServerRepository
    {
        private readonly ILogger<GameServerRepository> _logger;

        private GenericClient _client;

        public GameServerRepository(ILogger<GameServerRepository> logger, IConfig config)
        {
            _logger = logger;

            var k8sConfig = config.GetKuberneteClientConfiguration();
            var kubernetes = new k8s.Kubernetes(k8sConfig);
            _client = new GenericClient(kubernetes, "agones.dev", "v1", "gameservers");
        }

        public async Task<V1GameServer> GetAsync(string ns, string name)
        {
            var result = await _client.ReadNamespacedAsync<V1GameServer>(ns, name).ConfigureAwait(false);

            return result;
        }

        public async Task<CustomResourceList<V1GameServer>> ListAsync()
        {
            var result = await _client.ListAsync<CustomResourceList<V1GameServer>>().ConfigureAwait(false);

            return result;
        }

        public async Task<V1GameServer> DeleteAsync(string ns, string name)
        {
            var result = await _client.DeleteNamespacedAsync<V1GameServer>(ns, name).ConfigureAwait(false);

            return result;
        }
    }
}
