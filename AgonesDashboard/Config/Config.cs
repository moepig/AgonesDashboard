using k8s;
using System.Runtime.CompilerServices;

namespace AgonesDashboard.Config
{
    public class Config : IConfig
    {
        private IConfiguration _config;
        private KubernetesClientConfiguration _k8sConfig;

        public Config()
        {
            _config = Load();
            _k8sConfig = LoadKubernetesClientConfiguration();
        }

        private IConfiguration Load()
        {
            return Load("appsettings.json");
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private IConfiguration Load(string fileName)
        {
            if (_config != null)
            {
                return _config;
            }
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(fileName, true, true)
                .Build();

            return config;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private KubernetesClientConfiguration LoadKubernetesClientConfiguration()
        {
            if (_k8sConfig != null)
            {
                return _k8sConfig;
            }

            var section = _config.GetSection("Kubernetes");
            var mode = section["ConfigMode"];

            if (mode != null && mode.Equals("InCluster"))
            {
                _k8sConfig = KubernetesClientConfiguration.InClusterConfig();
                return _k8sConfig;
            }

            var filepath = section["ConfigPath"];

            if (filepath == null)
            {
                _k8sConfig = KubernetesClientConfiguration.BuildConfigFromConfigFile();
            }
            else
            {
                _k8sConfig = KubernetesClientConfiguration.BuildConfigFromConfigFile(filepath);
            }

            return _k8sConfig;
        }

        public KubernetesClientConfiguration GetKuberneteClientConfiguration()
        {
            if (_k8sConfig == null)
            {
                throw new InvalidOperationException();
            }

            return _k8sConfig;
        }
    }
}
