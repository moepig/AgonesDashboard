using System.Text.Json.Serialization;

namespace AgonesDashboard.Models.Kubernetes.CustomResources.Agones
{
    // https://agones.dev/site/docs/reference/agones_crd_api_reference/#agones.dev/v1.GameServerStatusPort
    public class V1GameServerStatusPort
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("port")]
        public int Port { get; set; }
    }
}
