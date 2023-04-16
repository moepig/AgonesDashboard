using AgonesDashboard.Filters;
using AgonesDashboard.Models.Kubernetes.CustomResources.Agones;
using AgonesDashboard.ViewModels.Fleet;
using k8s.Models;
using Microsoft.AspNetCore.Mvc;

namespace AgonesDashboard.Controllers
{
    [DevelopmentOnly]
    public class MockFleetController : Controller
    {
        private readonly ILogger<MockFleetController> _logger;

        public MockFleetController(ILogger<MockFleetController> logger)
        {
            _logger = logger;
        }

        public ViewResult Index()
        {
            var fleets = new Dictionary<string, IList<FleetSimple>>
            {
                {
                    "namespace1", new List<FleetSimple>
                    {
                        new FleetSimple
                        {
                            Name = "fleet1",
                            Scheduling = "Packed",
                            ReadyReplicas = 5,
                            ReservedReplicas = 1,
                            AllocatedReplicas = 6,
                            IsAutoscalerEnabled = true,
                        },
                        new FleetSimple
                        {
                            Name = "fleet2",
                            Scheduling = "Distributed",
                            ReadyReplicas = 3,
                            ReservedReplicas = 0,
                            AllocatedReplicas = 3,
                            IsAutoscalerEnabled = false,
                        }
                    }
                },
                {
                    "namespace2", new List<FleetSimple>
                    {
                        new FleetSimple
                        {
                            Name = "fleet3",
                            Scheduling = "Packed",
                            ReadyReplicas = 2,
                            ReservedReplicas = 1,
                            AllocatedReplicas = 3,
                            IsAutoscalerEnabled = true,
                        }
                    }
                }
            };

            var viewModel = new FleetIndex
            {
                Fleets = fleets,
            };

            return View("Views/Fleet/Index.cshtml", viewModel);
        }

        public ViewResult Detail()
        {
            var fleet = new V1Fleet
            {
                Metadata = new V1ObjectMeta
                {
                    Name = "my-fleet"
                },
                Spec = new V1FleetSpec
                {
                    Replicas = 5,
                    Strategy = new V1DeploymentStrategy
                    {
                        Type = "RollingUpdate",
                        RollingUpdate = new V1RollingUpdateDeployment
                        {
                            MaxSurge = "25%",
                            MaxUnavailable = "25%"
                        }
                    },
                    Scheduling = "Packed",
                    Template = new V1GameServerTemplateSpec
                    {
                        Metadata = new V1ObjectMeta
                        {
                            Name = "my-template"
                        },
                        Spec = new V1GameServerSpec
                        {
                            Container = "my-container",
                            Ports = new List<V1GameServerPort>{
                                new V1GameServerPort
                                {
                                    Name = "port1",
                                    ContainerPort = 12345
                                }
                            },
                            Health = new V1Health
                            {
                                Disabled = false,
                                PeriodSeconds = 10,
                                ailureThreshold = 3,
                                InitialDelaySeconds = 60
                            },
                            Scheduling = "Packed",
                            SdkServer = new V1SdkServer
                            {
                                LogLevel = "info",
                                GrpcPort = 76543,
                                HttpPort = 76542
                            },
                            Template = new V1PodTemplateSpec
                            {
                                Spec = new V1PodSpec
                                {
                                    Containers = new List<V1Container>{
                                        new V1Container
                                        {
                                            Name = "my-container",
                                            Image = "my-image"
                                        }
                                    }
                                }
                            },
                            Players = new V1PlayerSpec
                            {
                                InitialCapacity = 100
                            },
                            Eviction = new Models.Kubernetes.CustomResources.Agones.V1Eviction
                            {
                                Safe = "true"
                            }
                        }
                    }
                },
                Status = new V1FleetStatus
                {
                    Replicas = 5,
                    ReadyReplicas = 5,
                    ReservedReplicas = 0,
                    AllocatedReplicas = 5,
                    Players = new V1AggregatedPlayerStatus
                    {
                        Count = 100,
                        Capacity = 50,
                    }
                }
            };

            var viewModel = new Detail()
            {
                Fleet = fleet,
            };

            return View("Views/Fleet/Detail.cshtml", viewModel);
        }
    }
}