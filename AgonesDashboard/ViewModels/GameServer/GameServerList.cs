using System.Text.Json;

namespace AgonesDashboard.ViewModels.GameServer
{
    public class GameServerList
    {
        // key: namespace
        public IDictionary<string, IList<GameServerSimple>> GameServers { get; set; }
        public IDictionary<string, int> ContainerTotal { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
        }
    }

    public class GameServerSimple
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
        public int? ContainerPort { get; set; }
        public int? HostPort { get; set; }
        public IEnumerable<GameServerSimpleContainer>? GameServerSimpleContainer { get; set; }
        public string? Protocol { get; set; }
        public string? State { get; set; }

    }

    public class GameServerSimpleContainer
    {
        public string? Name { get; set; }
        public string? Image { get; set; }
    }
}
