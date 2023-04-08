namespace AgonesDashboard.ViewModels.Service
{
    public class ServiceIndex
    {
        public string Namespace;
        public IList<ServiceSimple> Allocators;
    }

    public class ServiceSimple
    {
        public string Name;
        public string ExternalName;
        public IList<string> ClusterIPs;
        public IList<string> ExternalIPs;
        public IList<string> Ports;
    }
}
