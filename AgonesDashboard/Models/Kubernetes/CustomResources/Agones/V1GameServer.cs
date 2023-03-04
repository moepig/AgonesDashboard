using k8s.Models;
using System.Text.Json.Serialization;

namespace AgonesDashboard.Models.Kubernetes.CustomResources.Agones
{
    // https://agones.dev/site/docs/reference/agones_crd_api_reference/#agones.dev/v1.GameServer
    public class V1GameServer : CustomResource<V1GameServerSpec, V1GameServerStatus>
    {

    }

    // https://agones.dev/site/docs/reference/agones_crd_api_reference/#agones.dev/v1.GameServerSpec
    public class V1GameServerSpec
    {
        [JsonPropertyName("container")]
        public string? Container { get; set; }

        [JsonPropertyName("ports")]
        public IEnumerable<V1GameServerPort>? Ports { get; set; }

        [JsonPropertyName("health")]
        public V1Health? Health { get; set; }

        // agones.dev/agones/pkg/apis.SchedulingStrategy	

        [JsonPropertyName("scheduling")]
        public string? Scheduling { get; set; }

        [JsonPropertyName("sdkServer")]
        public V1SdkServer? SdkServer { get; set; }

        [JsonPropertyName("template")]
        public V1PodTemplateSpec? Template { get; set; }

        [JsonPropertyName("players")]
        public V1PlayerSpec? Players { get; set; }

        [JsonPropertyName("eviction")]
        public V1Eviction? Eviction { get; set; }
    }

    // https://agones.dev/site/docs/reference/agones_crd_api_reference/#agones.dev/v1.Health
    public class V1Health
    {
        [JsonPropertyName("disabled")]
        public bool Disabled { get; set; }

        [JsonPropertyName("periodSeconds")]
        public int PeriodSeconds { get; set; }

        [JsonPropertyName("failureThreshold")]
        public int ailureThreshold { get; set; }

        [JsonPropertyName("initialDelaySeconds")]
        public int InitialDelaySeconds { get; set; }
    }

    // https://agones.dev/site/docs/reference/agones_crd_api_reference/#agones.dev/v1.GameServerPort
    public class V1GameServerPort
    {
        [JsonPropertyName("name")]
        public string? name { get; set; }

        // PortPolicy (string alias)
        [JsonPropertyName("portPolicy")]
        public string? PortPolicy { get; set; }

        [JsonPropertyName("container")]
        public string? Container { get; set; }

        [JsonPropertyName("containerPort")]
        public int ContainerPort { get; set; }

        [JsonPropertyName("hostPort")]
        public int HostPort { get; set; }

        // Kubernetes core/v1.Protocol
        [JsonPropertyName("protocol")]
        public string? Protocol { get; set; }
    }

    // https://agones.dev/site/docs/reference/agones_crd_api_reference/#agones.dev/v1.SdkServer
    public class V1SdkServer
    {
        // SdkServerLogLevel (string alias)
        [JsonPropertyName("logLevel")]
        public string? LogLevel { get; set; }

        [JsonPropertyName("grpcPort")]
        public int GrpcPort { get; set; }

        [JsonPropertyName("httpPort")]
        public int HttpPort { get; set; }
    }

    // https://agones.dev/site/docs/reference/agones_crd_api_reference/#agones.dev/v1.PlayersSpec
    public class V1PlayerSpec
    {
        [JsonPropertyName("initialCapacity")]
        public long InitialCapacity { get; set; }
    }

    // https://agones.dev/site/docs/reference/agones_crd_api_reference/#agones.dev/v1.Eviction
    public class V1Eviction
    {
        // EvictionSafe (string alias)
        [JsonPropertyName("safe")]
        public string? Safe { get; set; }
    }

    // https://agones.dev/site/docs/reference/agones_crd_api_reference/#agones.dev/v1.GameServerStatus
    public class V1GameServerStatus : V1Status
    {
        // https://agones.dev/site/docs/reference/agones_crd_api_reference/#agones.dev/v1.GameServerState
        // GameServerState (string alias)
        [JsonPropertyName("state")]
        public string? state { get; set; }

        [JsonPropertyName("ports")]
        public IEnumerable<V1GameServerStatusPort> Ports { get; set; }

        [JsonPropertyName("address")]
        public string? Address { get; set; }

        [JsonPropertyName("nodeName")]
        public string? NodeName { get; set; }

        // meta/v1.Time
        [JsonPropertyName("reservedUntil")]
        public System.DateTime? ReservedUntil { get; set; }

        [JsonPropertyName("players")]
        public V1PlayerStatus? Players { get; set; }

        [JsonPropertyName("eviction")]
        public V1Eviction? Eviction { get; set; }

    }

    // https://agones.dev/site/docs/reference/agones_crd_api_reference/#agones.dev/v1.PlayerStatus
    public class V1PlayerStatus
    {
        [JsonPropertyName("count")]
        public long count { get; set; }

        [JsonPropertyName("capacity")]
        public long capacity { get; set; }

        [JsonPropertyName("ids")]
        public IEnumerable<string>? Ids { get; set; }
    }
}
