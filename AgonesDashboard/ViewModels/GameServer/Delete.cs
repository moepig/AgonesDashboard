using AgonesDashboard.Models.Kubernetes.CustomResources.Agones;

namespace AgonesDashboard.ViewModels.GameServer
{
    public class Delete : AbstractViewModel
    {
        public V1GameServer GameServer { get; set; }
    }
}
