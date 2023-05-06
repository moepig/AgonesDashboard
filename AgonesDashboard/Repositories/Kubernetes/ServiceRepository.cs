using AgonesDashboard.Config;
using k8s;
using k8s.Models;

namespace AgonesDashboard.Repositories.Kubernetes
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly ILogger<ServiceRepository> _logger;

        private k8s.Kubernetes _kube;

        public ServiceRepository(ILogger<ServiceRepository> logger, IConfig config)
        {
            _logger = logger;

            var k8sConfig = config.GetKuberneteClientConfiguration();
            _kube = new k8s.Kubernetes(k8sConfig);
        }

        public async Task<V1ServiceList> ListAsync(string ns)
        {
            var result = await _kube.ListNamespacedServiceAsync(ns);

            return result;
        }

        public async Task<V1Service> GetAsync(string ns, string name)
        {
            var result = await _kube.ReadNamespacedServiceAsync(name, ns);

            return result;
        }
    }
}
