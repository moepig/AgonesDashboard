using System.Text.Json.Serialization;

namespace AgonesDashboard.Models
{
    public class GameServerAllocationResponse
    {
        [JsonPropertyName("gameServerName")]
        string GameServerName { get; set; }

        [JsonPropertyName("ports")]
        IEnumerable<GameServerAllocationResponsePort> Ports { get; set; }

        [JsonPropertyName("address")]
        string Address { get; set; }

        [JsonPropertyName("nodeName")]
        string NodeName { get; set; }

    }

    public class GameServerAllocationResponsePort
    {
        [JsonPropertyName("name")]
        string Name { get; set; }

        [JsonPropertyName("port")]
        string Port { get; set; }
    }
}
