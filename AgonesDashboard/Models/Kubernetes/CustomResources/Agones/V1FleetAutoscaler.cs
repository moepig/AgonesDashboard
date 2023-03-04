using System.Text.Json.Serialization;

namespace AgonesDashboard.Models.Kubernetes.CustomResources.Agones
{
    // https://agones.dev/site/docs/reference/agones_crd_api_reference/#autoscaling.agones.dev/v1.FleetAutoscaler
    public class V1FleetAutoscaler<V1FleetAutoscalerSpec, V1FleetAutoscalerStatus>
    {
    }

    // https://agones.dev/site/docs/reference/agones_crd_api_reference/#autoscaling.agones.dev/v1.FleetAutoscalerSpec
    public class V1FleetAutoscalerSpec
    {
        [JsonPropertyName("fleetName")]
        public string? FleetName { get; set; }

        [JsonPropertyName("policy")]
        public V1FleetAutoscalerPolicy? Policy { get; set; }

        [JsonPropertyName("sync")]
        public V1FleetAutoscalerSync? Sync { get; set; }
    }

    // https://agones.dev/site/docs/reference/agones_crd_api_reference/#autoscaling.agones.dev/v1.FleetAutoscalerStatus
    public class V1FleetAutoscalerStatus
    {
        [JsonPropertyName("currentReplicas")]
        public int CurrentReplicas { get; set; }

        [JsonPropertyName("desiredReplicas")]
        public int DesiredReplicas { get; set; }

        [JsonPropertyName("lastScaleTime")]
        public System.DateTime? LastScaleTime { get; set; }

        [JsonPropertyName("ableToScale")]
        public bool AbleToScale { get; set; }

        [JsonPropertyName("scalingLimited")]
        public bool ScalingLimited { get; set; }
    }

    // https://agones.dev/site/docs/reference/agones_crd_api_reference/#autoscaling.agones.dev/v1.FleetAutoscalerPolicy
    public class V1FleetAutoscalerPolicy
    {
        // FleetAutoscalerPolicyType(string alias)
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("buffer")]
        public V1BufferPolicy? Buffer { get; set; }

        [JsonPropertyName("webhook")]
        public V1WebhookPolicy? Webhook { get; set; }

    }

    // https://agones.dev/site/docs/reference/agones_crd_api_reference/#autoscaling.agones.dev/v1.FleetAutoscalerSync
    public class V1FleetAutoscalerSync
    {
        // FleetAutoscalerSyncType (string alias)
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("fixedInterval")]
        public V1FixedIntervalSync? FixedInterval { get; set; }
    }

    // https://agones.dev/site/docs/reference/agones_crd_api_reference/#autoscaling.agones.dev/v1.FixedIntervalSync
    public class V1FixedIntervalSync
    {
        [JsonPropertyName("seconds")]
        public int Seconds { get; set; }
    }

    public class V1BufferPolicy
    {
        [JsonPropertyName("maxReplicas")]
        public int MaxReplicas { get; set; }

        [JsonPropertyName("minReplicas")]
        public int MinReplicas { get; set; }

        // k8s.io/apimachinery/pkg/util/intstr.IntOrString
        [JsonPropertyName("bufferSize")]
        public string? BufferSize { get; set; }
    }

    // https://agones.dev/site/docs/reference/agones_crd_api_reference/#autoscaling.agones.dev/v1.WebhookPolicy
    public class V1WebhookPolicy
    {
        [JsonPropertyName("url")]
        public string? Url { get; set; }

        [JsonPropertyName("service")]
        public V1ServiceReference? service { get; set; }

        [JsonPropertyName("caBundle")]
        public byte[]? caBundle; 
    }

    // https://v1-24.docs.kubernetes.io/docs/reference/generated/kubernetes-api/v1.24/#servicereference-v1-admissionregistration
    public class V1ServiceReference
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("namespace")]
        public string? Namespace { get; set; }

        [JsonPropertyName("path")]
        public string? Path { get; set; }

        [JsonPropertyName("port")]
        public int Port { get; set; }
    }
}
