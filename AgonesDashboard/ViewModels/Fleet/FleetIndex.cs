using System.Text.Json;

namespace AgonesDashboard.ViewModels.Fleet
{
    public class FleetIndex : AbstractViewModel
    {
        // key: namespace
        public required IDictionary<string, IList<FleetSimple>> Fleets { get; init; }
    }
    public class FleetSimple
    {
        public required string Name { get; init; }
        public required string Scheduling { get; init; }
        public required int ReadyReplicas { get; init; }
        public required int ReservedReplicas { get; init; }
        public required int AllocatedReplicas { get; init; }
        public required bool IsAutoscalerEnabled { get; init; }
    }
}
