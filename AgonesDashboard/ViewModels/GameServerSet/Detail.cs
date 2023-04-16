using AgonesDashboard.Models.Kubernetes.CustomResources.Agones;

namespace AgonesDashboard.ViewModels.GameServerSet
{
    public class Detail : AbstractViewModel
    {
        public required V1GameServerSet GameServerSet { get; init; }
    }
}
