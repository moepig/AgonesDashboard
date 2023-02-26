using k8s;
using k8s.Models;
using System.Text.Json.Serialization;

namespace AgonesDashboard.Models.Kubernetes.CustomResources.Agones
{


    public class V1GameServer : CustomResource<V1GameServerSpec, GameServerStatus>
    {

    }

    // https://agones.dev/site/docs/reference/agones_crd_api_reference/#agones.dev/v1.GameServer
    public class V1GameServerSpec
    {
        [JsonPropertyName("container")]
        public string Container { get; set; }

        [JsonPropertyName("ports")]
        public List<V1GameServerPortSpec> Ports { get; set; }

        [JsonPropertyName("health")]
        public V1HealthSpec Health { get; set; }

        // agones.dev/agones/pkg/apis.SchedulingStrategy	

        [JsonPropertyName("scheduling")]
        public string Scheduling { get; set; }

        [JsonPropertyName("sdkServer")]
        public V1SdkServerSpec SdkServer { get; set; }

        [JsonPropertyName("template")]
        public V1PodTemplateSpec Template { get; set; }

        [JsonPropertyName("players")]
        public V1PlayerSpec Players { get; set; }

        [JsonPropertyName("eviction")]
        public V1EvictionSpec Eviction { get; set; }
    }

    // https://agones.dev/site/docs/reference/agones_crd_api_reference/#agones.dev/v1.Health
    public class V1HealthSpec
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
    public class V1GameServerPortSpec
    {
        [JsonPropertyName("name")]
        public string name { get; set; }

        // PortPolicy (string alias)
        [JsonPropertyName("portPolicy")]
        public string PortPolicy { get; set; }

        [JsonPropertyName("container")]
        public string Container { get; set; }

        [JsonPropertyName("containerPort")]
        public int ContainerPort { get; set; }

        [JsonPropertyName("hostPort")]
        public int HostPort { get; set; }

        // Kubernetes core/v1.Protocol
        [JsonPropertyName("protocol")]
        public string Protocol { get; set; }
    }

    // https://agones.dev/site/docs/reference/agones_crd_api_reference/#agones.dev/v1.SdkServer
    public class V1SdkServerSpec
    {
        // SdkServerLogLevel (string alias)
        [JsonPropertyName("logLevel")]
        public string LogLevel { get; set; }

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
    public class V1EvictionSpec
    {
        // EvictionSafe (string alias)
        [JsonPropertyName("safe")]
        public string Safe { get; set; }
    }

    public class GameServerStatus : V1Status
    {
        [JsonPropertyName("temperature")]
        public string Temperature { get; set; }
    }
}
