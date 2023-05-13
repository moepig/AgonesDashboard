using System.Text.Json;

namespace AgonesDashboard.ViewModels.GameServer
{
    public class GameServerIndex : AbstractViewModel
    {
        // key: namespace
        public required IDictionary<string, IList<GameServerSimple>> GameServers { get; init; }
        public required IDictionary<string, int> ContainerTotal { get; init; }
    }

    public class GameServerSimple
    {
        public required string Name { get; init; }
        public required string Address { get; init; }
        public required IEnumerable<GameServerSimpleContainer> GameServerSimpleContainer { get; init; }
        public required string State { get; init; }
    }

    public class GameServerSimpleContainer
    {
        public required string Name { get; init; }
        public required string Image { get; init; }
        public required int ContainerPort { get; init; }
        public required int HostPort { get; init; }
        public required string Protocol { get; init; }
    }
}
