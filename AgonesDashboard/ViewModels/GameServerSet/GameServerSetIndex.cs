namespace AgonesDashboard.ViewModels.GameServerSet
{
    public class GameServerSetIndex : AbstractViewModel
    {
        // key: namespace
        public required IDictionary<string, IList<GameServerSetSimple>> GameServerSets { get; init; }
    }

    public class GameServerSetSimple
    {
        public required string Name { get; init; }
        public required string Scheduling { get; init; }
        public required int ReadyReplicas { get; init; }
        public required int ReservedReplicas { get; init; }
        public required int AllocatedReplicas { get; init; }
        public required int ShutdownReplicas { get; init; }
    }
}
