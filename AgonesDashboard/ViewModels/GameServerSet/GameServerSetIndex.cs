namespace AgonesDashboard.ViewModels.GameServerSet
{
    public class GameServerSetIndex : AbstractViewModel
    {
        // key: namespace
        public IDictionary<string, IList<GameServerSetSimple>>? GameServerSets { get; set; }
    }

    public class GameServerSetSimple
    {
        public string? Name { get; set; }
        public string? Scheduling { get; set; }
        public int? ReadyReplicas { get; set; }
        public int? ReservedReplicas { get; set; }
        public int? AllocatedReplicas { get; set; }
        public int? ShutdownReplicas { get; set; }
    }
}
