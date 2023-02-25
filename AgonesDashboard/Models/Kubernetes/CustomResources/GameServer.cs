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
        [JsonPropertyName("cityName")]
        public string CityName { get; set; }
    }

    public class GameServerStatus : V1Status
    {
        [JsonPropertyName("temperature")]
        public string Temperature { get; set; }
    }
}
