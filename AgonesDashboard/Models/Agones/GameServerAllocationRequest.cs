using System.Text.Json.Serialization;

namespace AgonesDashboard.Models.Agones
{
    public class GameServerAllocationRequest
    {
        [JsonPropertyName("namespace")]
        public string Namespace { get; set; }
    }
}
