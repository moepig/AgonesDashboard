using k8s.Models;
using System.Text.Json.Serialization;

namespace AgonesDashboard.Models.Kubernetes.CustomResources.Agones
{
    // https://agones.dev/site/docs/reference/agones_crd_api_reference/#agones.dev/v1.GameServerSet
    public class V1GameServerSet : CustomResource<V1GameServerSetSpec, V1GameServerSetStatus>
    {
    }

    // https://agones.dev/site/docs/reference/agones_crd_api_reference/#agones.dev/v1.GameServerSetSpec
    public class V1GameServerSetSpec
    {
        [JsonPropertyName("replicas")]
        public int Replicas { get; set; }

        // agones.dev/agones/pkg/apis.SchedulingStrategy	
        [JsonPropertyName("scheduling")]
        public string? Scheduling { get; set; }

        [JsonPropertyName("template")]
        public V1GameServerTemplateSpec? Template { get; set; }
    }

    // https://agones.dev/site/docs/reference/agones_crd_api_reference/#agones.dev/v1.GameServerSetStatus
    public class V1GameServerSetStatus : V1Status
    {
        [JsonPropertyName("replicas")]
        public int Replicas { get; set; }

        [JsonPropertyName("readyReplicas")]
        public int ReadyReplicas { get; set; }

        [JsonPropertyName("reservedReplicas")]
        public int ReservedReplicas { get; set; }

        [JsonPropertyName("allocatedReplicas")]
        public int AllocatedReplicas { get; set; }

        [JsonPropertyName("shutdownReplicas")]
        public int ShutdownReplicas { get; set; }

        [JsonPropertyName("players")]
        public V1AggregatedPlayerStatus? Players { get; set; }
    }
}
