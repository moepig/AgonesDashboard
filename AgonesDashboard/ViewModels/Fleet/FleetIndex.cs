using System.Text.Json;

namespace AgonesDashboard.ViewModels.Fleet
{
    public class FleetIndex : AbstractViewModel
    {
        // key: namespace
        public IDictionary<string, IList<FleetSimple>> Fleets { get; set; }
    }
    public class FleetSimple
    {
        public string? Name { get; set; }
        public string? Scheduling { get; set; }
        public int? ReadyReplicas { get; set; }
        public int? ReservedReplicas { get; set; }
        public int? AllocatedReplicas { get; set; }
        public bool IsAutoscalerEnabled { get; set; }
    }
}
