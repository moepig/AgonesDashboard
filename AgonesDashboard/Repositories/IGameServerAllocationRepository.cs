using AgonesDashboard.Models.Kubernetes.CustomResources.Agones.Allocation;

namespace AgonesDashboard.Repositories
{
    public interface IGameServerAllocationRepository
    {
        public Task<V1GameServerAllocation> CreateAsync(string ns, V1GameServerAllocation allocation);
    }
}
