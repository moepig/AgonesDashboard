using System.Text.Json.Serialization;

namespace AgonesDashboard.Models.Agones
{
    public class GameServerAllocationRequest
    {
        [JsonPropertyName("namespace")]
        string Namespace { get; set; }
    }
}
