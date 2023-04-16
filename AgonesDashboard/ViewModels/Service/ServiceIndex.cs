namespace AgonesDashboard.ViewModels.Service
{
    public class ServiceIndex : AbstractViewModel
    {
        public required string Namespace;
        public required IList<ServiceSimple> Allocators;
    }

    public class ServiceSimple
    {
        public required string Name;
        public required string ExternalName;
        public required IList<string> ClusterIPs;
        public required IList<string> ExternalIPs;
        public required IList<string> Ports;
    }
}
