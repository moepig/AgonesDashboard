using k8s.Models;
using System.Text.Json.Serialization;

namespace AgonesDashboard.Models.Kubernetes.CustomResources.Agones
{
    // https://agones.dev/site/docs/reference/agones_crd_api_reference/#agones.dev/v1.GameServerTemplateSpec
    public class V1GameServerTemplateSpec
    {
        [JsonPropertyName("metadata")]
        public V1ObjectMeta Metadata { get; set; }

        [JsonPropertyName("spec")]
        public V1GameServerSpec Spec { get; set; }
    }

}
