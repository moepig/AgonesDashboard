using AgonesDashboard.Models.Kubernetes.CustomResources.Agones;

namespace AgonesDashboard.ViewModels.GameServer
{
    public class Detail : AbstractViewModel
    {
        public required V1GameServer GameServer { get; init; }
    }
}
