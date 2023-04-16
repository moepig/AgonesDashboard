using AgonesDashboard.Models.Kubernetes.CustomResources.Agones.Allocation;

namespace AgonesDashboard.ViewModels.Fleet
{
    public class Allocation : AbstractViewModel
    {
        public required V1GameServerAllocation GameServerAllocation { get; init; }
    }
}
