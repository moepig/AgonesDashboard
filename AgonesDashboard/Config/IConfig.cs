using k8s;

namespace AgonesDashboard.Config
{
    public interface IConfig
    {
        public KubernetesClientConfiguration GetKuberneteClientConfiguration();
    }
}
