using k8s.Models;
using System.Text.Json.Serialization;

namespace AgonesDashboard.Models.Kubernetes.CustomResources.Agones
{
    // https://agones.dev/site/docs/reference/agones_crd_api_reference/#agones.dev/v1.Fleet
    public class V1Fleet : CustomResource<V1FleetSpec, V1FleetStatus>
    {

    }

    // https://agones.dev/site/docs/reference/agones_crd_api_reference/#agones.dev/v1.FleetSpec
    public class V1FleetSpec
    {
        [JsonPropertyName("replicas")]
        public int Replicas { get; set; }

        [JsonPropertyName("strategy")]
        public V1DeploymentStrategy? Strategy { get; set; }

        // agones.dev/agones/pkg/apis.SchedulingStrategy	
        [JsonPropertyName("scheduling")]
        public string? Scheduling { get; set; }

        [JsonPropertyName("template")]
        public V1GameServerTemplateSpec? Template { get; set; }
    }

    // https://agones.dev/site/docs/reference/agones_crd_api_reference/#agones.dev/v1.FleetStatus
    public class V1FleetStatus : V1Status
    {
        [JsonPropertyName("replicas")]
        public int Replicas { get; set; }

        [JsonPropertyName("readyReplicas")]
        public int ReadyReplicas { get; set; }

        [JsonPropertyName("reservedReplicas")]
        public int ReservedReplicas { get; set; }

        [JsonPropertyName("allocatedReplicas")]
        public int AllocatedReplicas { get; set; }

        [JsonPropertyName("players")]
        public V1AggregatedPlayerStatus? Players { get; set; }
    }
}
