using System.Text.Json.Serialization;

namespace AgonesDashboard.Models.Agones
{
    public class GameServerAllocationResponse
    {
        [JsonPropertyName("gameServerName")]
        public string GameServerName { get; set; }

        [JsonPropertyName("ports")]
        IEnumerable<GameServerAllocationResponsePort> Ports { get; set; }

        [JsonPropertyName("address")]
        public string Address { get; set; }

        [JsonPropertyName("nodeName")]
        public string NodeName { get; set; }

    }

    public class GameServerAllocationResponsePort
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("port")]
        public string Port { get; set; }
    }
}
