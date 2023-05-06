using AgonesDashboard.Config;
using AgonesDashboard.Models.Kubernetes;
using AgonesDashboard.Models.Kubernetes.CustomResources.Agones;
using k8s;

namespace AgonesDashboard.Repositories.Kubernetes
{
    public class GameServerSetRepository : IGameServerSetRepository
    {
        private readonly ILogger<GameServerSetRepository> _logger;

        private GenericClient _client;

        public GameServerSetRepository(ILogger<GameServerSetRepository> logger, IConfig config)
        {
            _logger = logger;

            var k8sConfig = config.GetKuberneteClientConfiguration();
            var kubernetes = new k8s.Kubernetes(k8sConfig);
            _client = new GenericClient(kubernetes, "agones.dev", "v1", "gameserversets");
        }


        public async Task<CustomResourceList<V1GameServerSet>> ListAsync()
        {
            var result = await _client.ListAsync<CustomResourceList<V1GameServerSet>>().ConfigureAwait(false);

            return result;
        }

        public async Task<V1GameServerSet> GetAsync(string ns, string name)
        {
            var result = await _client.ReadNamespacedAsync<V1GameServerSet>(ns, name).ConfigureAwait(false);

            return result;
        }
    }
}
