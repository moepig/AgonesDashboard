using System.Text.Json.Serialization;

namespace AgonesDashboard.Models.Kubernetes.CustomResources.Agones
{
    // https://agones.dev/site/docs/reference/agones_crd_api_reference/#agones.dev/v1.AggregatedPlayerStatus
    public class V1AggregatedPlayerStatus
    {
        [JsonPropertyName("count")]
        public long Count { get; set; }

        [JsonPropertyName("capacity")]
        public long Capacity { get; set; }
    }
}
