namespace AgonesDashboard.ViewModels.Allocator
{
    public class AllocatorIndex
    {
        public string Namespace;
        public IList<AllocatorSimple> Allocators;
    }

    public class AllocatorSimple
    {
        public string Name;
        public string ExternalName;
        public IList<string> ClusterIPs;
        public IList<string> ExternalIPs;
        public IList<string> Ports;
    }
}
