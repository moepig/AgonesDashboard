using k8s;
using k8s.Models;
using System.Text.Json.Serialization;

namespace AgonesDashboard.Models.Kubernetes.CustomResources
{


    public class GameServerResource : CustomResource<GameServerSpec, GameServerStatus>
    {

    }

    public class GameServerSpec
    {
        [JsonPropertyName("container")]
        public string Container { get; set; }

        [JsonPropertyName("ports")]
        public List<GameServerSpecPort> Ports { get; set; }

        [JsonPropertyName("health")]
        public GameServerSpecHealth Health { get; set; }

        // agones.dev/agones/pkg/apis.SchedulingStrategy	

        [JsonPropertyName("scheduling")]
        public string Scheduling { get; set; }
    }


    public class GameServerSpecHealth
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

    public class GameServerSpecPort
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

    public class GameServerStatus : V1Status
    {
        [JsonPropertyName("temperature")]
        public string Temperature { get; set; }
    }
}
