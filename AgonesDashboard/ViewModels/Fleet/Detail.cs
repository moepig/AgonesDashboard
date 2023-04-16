using AgonesDashboard.Models.Kubernetes.CustomResources.Agones;
using AgonesDashboard.Models.Kubernetes.CustomResources.Agones.AutoScaling;

namespace AgonesDashboard.ViewModels.Fleet
{
    public class Detail : AbstractViewModel
    {
        public required V1Fleet Fleet { get; init; }
        public required V1FleetAutoscaler? FleetAutoscaler { get; init; }
    }
}
