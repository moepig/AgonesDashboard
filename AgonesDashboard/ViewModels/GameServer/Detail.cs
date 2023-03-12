using AgonesDashboard.Models.Kubernetes.CustomResources.Agones;

namespace AgonesDashboard.ViewModels.GameServer
{
    public class Detail : AbstractViewModel
    {
        public V1GameServer GameServer { get; set; }
    }
}
