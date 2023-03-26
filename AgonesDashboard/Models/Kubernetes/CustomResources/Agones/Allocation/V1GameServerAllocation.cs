using k8s.Models;
using System.Text.Json.Serialization;

namespace AgonesDashboard.Models.Kubernetes.CustomResources.Agones.Allocation
{
    // https://agones.dev/site/docs/reference/agones_crd_api_reference/#allocation.agones.dev/v1.GameServerAllocation
    public class V1GameServerAllocation : CustomResource<V1GameServerAllocationSpec, V1GameServerAllocationStatus>
    {
    }

    // https://agones.dev/site/docs/reference/agones_crd_api_reference/#allocation.agones.dev/v1.GameServerAllocationSpec
    public class V1GameServerAllocationSpec
    {
        [JsonPropertyName("multiClusterSetting")]
        public V1MultiClusterSetting? MultiClusterSetting { get; set; }

        [JsonPropertyName("required")]
        public V1GameServerSelector? Required { get; set; }

        [JsonPropertyName("preferred")]
        public IEnumerable<V1GameServerSelector>? Preferred { get; set; }

        [JsonPropertyName("selectors")]
        public IEnumerable<V1GameServerSelector>? Selectors { get; set; }

        // agones.dev/agones/pkg/apis.GameServerAllocationState	
        [JsonPropertyName("scheduling")]
        public string? Scheduling { get; set; }


        [JsonPropertyName("metadata")]
        public V1MetaPatch? Metadata { get; set; }
    }

    // https://agones.dev/site/docs/reference/agones_crd_api_reference/#allocation.agones.dev/v1.GameServerAllocationStatus
    public class V1GameServerAllocationStatus : V1Status
    {
        // GameServerAllocationState (string alias)
        [JsonPropertyName("state")]
        public string? State { get; set; }

        [JsonPropertyName("gameServerName")]
        public string? GameServerName { get; set; }

        [JsonPropertyName("ports")]
        public IEnumerable<V1GameServerStatusPort>? Ports { get; set; }

        [JsonPropertyName("address")]
        public string? Address { get; set; }


        [JsonPropertyName("nodeName")]
        public string? NodeName { get; set; }

        [JsonPropertyName("source")]
        public string? Source { get; set; }
    }

    // https://agones.dev/site/docs/reference/agones_crd_api_reference/#allocation.agones.dev/v1.GameServerSelector
    public class V1GameServerSelector
    {
        [JsonPropertyName("LabelSelector")]
        public V1LabelSelector? LabelSelector { get; set; }

        // GameServerAllocationState (string alias)
        [JsonPropertyName("gameServerState")]
        public string? GameServerState { get; set; }


        [JsonPropertyName("players")]
        public V1PlayerSelector? Players { get; set; }
    }

    // https://agones.dev/site/docs/reference/agones_crd_api_reference/#allocation.agones.dev/v1.MultiClusterSetting
    public class V1MultiClusterSetting
    {
        [JsonPropertyName("enabled")]
        public bool Enabled { get; set; }

        [JsonPropertyName("policySelector")]
        public V1LabelSelector? PolicySelector { get; set; }
    }

    // https://agones.dev/site/docs/reference/agones_crd_api_reference/#allocation.agones.dev/v1.MetaPatch
    public class V1MetaPatch
    {
        [JsonPropertyName("labels")]
        public Dictionary<string, string>? Labels { get; set; }

        [JsonPropertyName("annotations")]
        public Dictionary<string, string>? Annotations { get; set; }
    }

    // https://agones.dev/site/docs/reference/agones_crd_api_reference/#allocation.agones.dev/v1.PlayerSelector
    public class V1PlayerSelector
    {
        [JsonPropertyName("minAvailable")]
        public long MinAvailable { get; set; }

        [JsonPropertyName("maxAvailable")]
        public long MaxAvailable { get; set; }
    }
}
