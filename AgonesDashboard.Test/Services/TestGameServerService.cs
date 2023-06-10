using AgonesDashboard.Models.Kubernetes;
using AgonesDashboard.Models.Kubernetes.CustomResources.Agones;
using AgonesDashboard.Repositories;
using AgonesDashboard.Services;
using AgonesDashboard.ViewModels.GameServer;
using k8s.Models;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Text.Json;

namespace AgonesDashboard.Test.Services
{
    public class TestGameServerService
    {
        [Fact]
        public async Task ListValue()
        {
            Mock<IGameServerRepository> gameserverRepository = new Mock<IGameServerRepository>();
            gameserverRepository.Setup(x => x.ListAsync()).ReturnsAsync(this.GetList());
            Mock<ILogger<GameServerService>> logger = new Mock<ILogger<GameServerService>>();

            IGameServerService service = new GameServerService(logger.Object, gameserverRepository.Object);

            var viewModel = await service.ListAsync();

            Assert.NotNull(viewModel);
            Assert.Equal(4, viewModel.GameServers["default"].Count);
            Assert.Equal(1, viewModel.GameServers["namespace1"].Count);
            Assert.Equal(5, viewModel.ContainerTotal["default"]);
            Assert.Equal(1, viewModel.ContainerTotal["namespace1"]);

            foreach (var (gsKey, gsValue) in viewModel.GameServers)
            {
                foreach (var gs in gsValue)
                {
                    Assert.NotNull(gs.Name);
                    Assert.NotNull(gs.State);
                    Assert.NotNull(gs.Address);
                    Assert.NotEmpty(gs.GameServerSimpleContainer);

                    foreach (var simpleContainer in gs.GameServerSimpleContainer)
                    {
                        var t = typeof(GameServerSimpleContainer);
                        foreach (var f in t.GetFields())
                        {
                            var n = f.Name;
                            var v = f.GetValue(t);
                            Assert.NotNull(v);
                        }
                    }
                }
            }
        }

        private CustomResourceList<V1GameServer> GetList()
        {
            return new CustomResourceList<V1GameServer>
            {
                Metadata = new V1ListMeta
                {
                    ContinueProperty = "",
                    ResourceVersion = "475657"
                },
                Items = new List<V1GameServer>
                {
                    new V1GameServer
                    {
                        Spec = new V1GameServerSpec
                        {
                            Container = "simple-game-server",
                            Ports = new List<V1GameServerPort>
                            {
                                new V1GameServerPort
                                {
                                    Name = "default",
                                    PortPolicy = "Dynamic",
                                    Container = "simple-game-server",
                                    ContainerPort = 7654,
                                    HostPort = 7250,
                                    Protocol = "UDP"
                                }
                            },
                            Health = new V1Health
                            {
                                PeriodSeconds = 5,
                                FailureThreshold = 3,
                                InitialDelaySeconds = 5
                            },
                            Scheduling = "Packed",
                            SdkServer = new V1SdkServer
                            {
                                LogLevel = "Info",
                                GrpcPort = 9357,
                                HttpPort = 9358
                            },
                            Template = new V1PodTemplateSpec
                            {
                                Metadata = new V1ObjectMeta(),
                                Spec = new V1PodSpec
                                {
                                    Containers = new List<V1Container>
                                    {
                                        new V1Container
                                        {
                                            Image = "us-docker.pkg.dev/agones-images/examples/simple-game-server:0.14",
                                            Name = "simple-game-server",
                                            Resources = new V1ResourceRequirements
                                            {
                                                Limits = new Dictionary<string, ResourceQuantity>
                                                {
                                                    {
                                                        "cpu",
                                                        new ResourceQuantity
                                                        {
                                                            //Format = SuffixFormat.DecimalSI,
                                                            Value = "20m"
                                                        }
                                                    },
                                                    {
                                                        "memory",
                                                        new ResourceQuantity
                                                        {
                                                            //Format = SuffixFormat.BinarySI,
                                                            Value = "64Mi"
                                                        }
                                                    }
                                                },
                                                Requests = new Dictionary<string, ResourceQuantity>
                                                {
                                                    {
                                                        "cpu",
                                                        new ResourceQuantity
                                                        {
                                                            //Format = SuffixFormat.DecimalSI,
                                                            Value = "20m"
                                                        }
                                                    },
                                                    {
                                                        "memory",
                                                        new ResourceQuantity
                                                        {
                                                            //Format = SuffixFormat.BinarySI,
                                                            Value = "64Mi"
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        },
                        Status = new V1GameServerStatus
                        {
                            State = "Unhealthy",
                            Ports = new List<V1GameServerStatusPort>
                            {
                                new V1GameServerStatusPort
                                {
                                    Name = "default",
                                    Port = 7250
                                }
                            },
                            Address = "192.168.65.4",
                            NodeName = "docker-desktop"
                        },
                        Metadata = new V1ObjectMeta
                        {
                            Annotations = new Dictionary<string, string>
                            {
                                {
                                    "agones.dev/ready-container-id",
                                    "docker://d4552e8dd6bf1677fb22e5ebceee4a55696123b03edf571107c57968e20f01ff"
                                },
                                {
                                    "agones.dev/sdk-version",
                                    "1.29.0"
                                }
                            },
                            CreationTimestamp = new DateTime(2023, 3, 18, 9, 26, 47, 0, DateTimeKind.Utc),
                            Finalizers = new List<string>
                            {
                                "agones.dev"
                            },
                            GenerateName = "simple-game-server-",
                            Generation = 9,
                            ManagedFields = new List<V1ManagedFieldsEntry>
                            {
                                new V1ManagedFieldsEntry
                                {
                                    ApiVersion = "agones.dev/v1",
                                    FieldsType = "FieldsV1",
                                    FieldsV1 = new JsonElement(),
                                    Manager = "kubectl-create",
                                    Operation = "Update",
                                    Time = new DateTime(2023, 3, 18, 9, 26, 47, 0, DateTimeKind.Utc)
                                },
                                new V1ManagedFieldsEntry
                                {
                                    ApiVersion = "agones.dev/v1",
                                    FieldsType = "FieldsV1",
                                    FieldsV1 = new JsonElement(),
                                    Manager = "controller",
                                    Operation = "Update",
                                    Time = new DateTime(2023, 3, 19, 5, 8, 34, 0, DateTimeKind.Utc)
                                }
                            },
                            Name = "simple-game-server-b2r66",
                            NamespaceProperty = "namespace1",
                            ResourceVersion = "125507",
                            Uid = "de2c23da-58ea-411d-be6f-f9722abf7c81"
                        },
                        ApiVersion = "agones.dev/v1",
                        Kind = "GameServer"
                    },
                    new V1GameServer
                    {
                        Spec = new V1GameServerSpec
                        {
                            Container = "simple-game-server",
                            Ports = new List<V1GameServerPort>
                            {
                                new V1GameServerPort
                                {
                                    Name = "default",
                                    PortPolicy = "Dynamic",
                                    Container = "simple-game-server",
                                    ContainerPort = 7654,
                                    HostPort = 7154,
                                    Protocol = "UDP"
                                }
                            },
                            Health = new V1Health
                            {
                                PeriodSeconds = 5,
                                FailureThreshold = 3,
                                InitialDelaySeconds = 5
                            },
                            Scheduling = "Packed",
                            SdkServer = new V1SdkServer
                            {
                                LogLevel = "Info",
                                GrpcPort = 9357,
                                HttpPort = 9358
                            },
                            Template = new V1PodTemplateSpec
                            {
                                Metadata = new V1ObjectMeta(),
                                Spec = new V1PodSpec
                                {
                                    Containers = new List<V1Container>
                                    {
                                        new V1Container
                                        {
                                            Image = "us-docker.pkg.dev/agones-images/examples/simple-game-server:0.14",
                                            Name = "simple-game-server",
                                            Resources = new V1ResourceRequirements
                                            {
                                                Limits = new Dictionary<string, ResourceQuantity>
                                                {
                                                    {
                                                        "cpu",
                                                        new ResourceQuantity
                                                        {
                                                            //Format = SuffixFormat.DecimalSI,
                                                            Value = "20m"
                                                        }
                                                    },
                                                    {
                                                        "memory",
                                                        new ResourceQuantity
                                                        {
                                                            //Format = SuffixFormat.BinarySI,
                                                            Value = "64Mi"
                                                        }
                                                    }
                                                },
                                                Requests = new Dictionary<string, ResourceQuantity>
                                                {
                                                    {
                                                        "cpu",
                                                        new ResourceQuantity
                                                        {
                                                            //Format = SuffixFormat.DecimalSI,
                                                            Value = "20m"
                                                        }
                                                    },
                                                    {
                                                        "memory",
                                                        new ResourceQuantity
                                                        {
                                                            //Format = SuffixFormat.BinarySI,
                                                            Value = "64Mi"
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        },
                        Status = new V1GameServerStatus
                        {
                            State = "Ready",
                            Ports = new List<V1GameServerStatusPort>
                            {
                                new V1GameServerStatusPort
                                {
                                    Name = "default",
                                    Port = 7154
                                }
                            },
                            Address = "192.168.65.4",
                            NodeName = "docker-desktop"
                        },
                        Metadata = new V1ObjectMeta
                        {
                            Annotations = new Dictionary<string, string>
                            {
                                {
                                    "agones.dev/ready-container-id",
                                    "docker://fdbc6c39cc8bfd54174224f5f615eb2dd850ace2f8f83db3264ba812e2735e49"
                                },
                                {
                                    "agones.dev/sdk-version",
                                    "1.29.0"
                                }
                            },
                            CreationTimestamp = new DateTime(2023, 4, 30, 3, 18, 53, 0, DateTimeKind.Utc),
                            Finalizers = new List<string>
                            {
                                "agones.dev"
                            },
                            GenerateName = "simple-game-server-v9p2q-",
                            Generation = 22,
                            Labels = new Dictionary<string, string>
                            {
                                {
                                    "agones.dev/fleet",
                                    "simple-game-server"
                                },
                                {
                                    "agones.dev/gameserverset",
                                    "simple-game-server-v9p2q"
                                }
                            },
                            ManagedFields = new List<V1ManagedFieldsEntry>
                            {
                                new V1ManagedFieldsEntry
                                {
                                    ApiVersion = "agones.dev/v1",
                                    FieldsType = "FieldsV1",
                                    FieldsV1 = new JsonElement(),
                                    Manager = "controller",
                                    Operation = "Update",
                                    Time = new DateTime(2023, 5, 21, 5, 43, 16, 0, DateTimeKind.Utc)
                                }
                            },
                            Name = "simple-game-server-v9p2q-59pqk",
                            NamespaceProperty = "default",
                            OwnerReferences = new List<V1OwnerReference>
                            {
                                new V1OwnerReference
                                {
                                    ApiVersion = "agones.dev/v1",
                                    BlockOwnerDeletion = true,
                                    Controller = true,
                                    Kind = "GameServerSet",
                                    Name = "simple-game-server-v9p2q",
                                    Uid = "bd4e13c7-6d3f-4694-b940-a5dd0e51ae9e"
                                }
                            },
                            ResourceVersion = "475268",
                            Uid = "ceda268a-a056-4535-8ab7-13f99c34d0f0"
                        },
                        ApiVersion = "agones.dev/v1",
                        Kind = "GameServer"
                    },
                    new V1GameServer
                    {
                        Spec = new V1GameServerSpec
                        {
                            Container = "simple-game-server",
                            Ports = new List<V1GameServerPort>
                            {
                                new V1GameServerPort
                                {
                                    Name = "default",
                                    PortPolicy = "Dynamic",
                                    Container = "simple-game-server",
                                    ContainerPort = 7654,
                                    HostPort = 7292,
                                    Protocol = "UDP"
                                }
                            },
                            Health = new V1Health
                            {
                                PeriodSeconds = 5,
                                FailureThreshold = 3,
                                InitialDelaySeconds = 5
                            },
                            Scheduling = "Packed",
                            SdkServer = new V1SdkServer
                            {
                                LogLevel = "Info",
                                GrpcPort = 9357,
                                HttpPort = 9358
                            },
                            Template = new V1PodTemplateSpec
                            {
                                Metadata = new V1ObjectMeta(),
                                Spec = new V1PodSpec
                                {
                                    Containers = new List<V1Container>
                                    {
                                        new V1Container
                                        {
                                            Image = "us-docker.pkg.dev/agones-images/examples/simple-game-server:0.14",
                                            Name = "simple-game-server",
                                            Resources = new V1ResourceRequirements
                                            {
                                                Limits = new Dictionary<string, ResourceQuantity>
                                                {
                                                    {
                                                        "cpu",
                                                        new ResourceQuantity
                                                        {
                                                            //Format = SuffixFormat.DecimalSI,
                                                            Value = "20m"
                                                        }
                                                    },
                                                    {
                                                        "memory",
                                                        new ResourceQuantity
                                                        {
                                                            //Format = SuffixFormat.BinarySI,
                                                            Value = "64Mi"
                                                        }
                                                    }
                                                },
                                                Requests = new Dictionary<string, ResourceQuantity>
                                                {
                                                    {
                                                        "cpu",
                                                        new ResourceQuantity
                                                        {
                                                            //Format = SuffixFormat.DecimalSI,
                                                            Value = "20m"
                                                        }
                                                    },
                                                    {
                                                        "memory",
                                                        new ResourceQuantity
                                                        {
                                                            //Format = SuffixFormat.BinarySI,
                                                            Value = "64Mi"
                                                        }
                                                    }
                                                }
                                            }
                                        },
                                        new V1Container
                                        {
                                            Image = "example.com/another-image:1.0",
                                            Name = "another-container",
                                            Resources = new V1ResourceRequirements
                                            {
                                                Limits = new Dictionary<string, ResourceQuantity>
                                                {
                                                    {
                                                        "cpu",
                                                        new ResourceQuantity
                                                        {
                                                            Value = "100m"
                                                        }
                                                    },
                                                    {
                                                        "memory",
                                                        new ResourceQuantity
                                                        {
                                                            Value = "128Mi"
                                                        }
                                                    }
                                                },
                                                Requests = new Dictionary<string, ResourceQuantity>
                                                {
                                                    {
                                                        "cpu",
                                                        new ResourceQuantity
                                                        {
                                                            Value = "100m"
                                                        }
                                                    },
                                                    {
                                                        "memory",
                                                        new ResourceQuantity
                                                        {
                                                            Value = "128Mi"
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        },
                        Status = new V1GameServerStatus
                        {
                            State = "Ready",
                            Ports = new List<V1GameServerStatusPort>
                            {
                                new V1GameServerStatusPort
                                {
                                    Name = "default",
                                    Port = 7292
                                }
                            },
                            Address = "192.168.65.4",
                            NodeName = "docker-desktop"
                        },
                        Metadata = new V1ObjectMeta
                        {
                            Annotations = new Dictionary<string, string>
                            {
                                {
                                    "agones.dev/ready-container-id",
                                    "docker://69b52d8155a7bb6400fb6f4e01ffe6b15c5502f340c2f8f26d6753ece73cd747"
                                },
                                {
                                    "agones.dev/sdk-version",
                                    "1.29.0"
                                }
                            },
                            CreationTimestamp = new DateTime(2023, 5, 13, 10, 17, 54, 0, DateTimeKind.Utc),
                            Finalizers = new List<string>
                            {
                                "agones.dev"
                            },
                            GenerateName = "simple-game-server-v9p2q-",
                            Generation = 10,
                            Labels = new Dictionary<string, string>
                            {
                                {
                                    "agones.dev/fleet",
                                    "simple-game-server"
                                },
                                {
                                    "agones.dev/gameserverset",
                                    "simple-game-server-v9p2q"
                                }
                            },
                            ManagedFields = new List<V1ManagedFieldsEntry>
                            {
                                new V1ManagedFieldsEntry
                                {
                                    ApiVersion = "agones.dev/v1",
                                    FieldsType = "FieldsV1",
                                    FieldsV1 = new JsonElement(),
                                    Manager = "controller",
                                    Operation = "Update",
                                    Time = new DateTime(2023, 5, 21, 5, 43, 16, 0, DateTimeKind.Utc)
                                }
                            },
                            Name = "simple-game-server-v9p2q-whbls-with-sidecar",
                            NamespaceProperty = "default",
                            OwnerReferences = new List<V1OwnerReference>
                            {
                                new V1OwnerReference
                                {
                                    ApiVersion = "agones.dev/v1",
                                    BlockOwnerDeletion = true,
                                    Controller = true,
                                    Kind = "GameServerSet",
                                    Name = "simple-game-server-v9p2q",
                                    Uid = "bd4e13c7-6d3f-4694-b940-a5dd0e51ae9e"
                                }
                            },
                            ResourceVersion = "475258",
                            Uid = "f13e9ee8-db93-4ee1-bcbb-5e3b5b126214"
                        },
                        ApiVersion = "agones.dev/v1",
                        Kind = "GameServer"
                    },
                    new V1GameServer
                    {
                        Spec = new V1GameServerSpec
                        {
                            Container = "simple-game-server2",
                            Ports = new List<V1GameServerPort>
                            {
                                new V1GameServerPort
                                {
                                    Name = "default",
                                    PortPolicy = "Dynamic",
                                    Container = "simple-game-server2",
                                    ContainerPort = 7654,
                                    HostPort = 7663,
                                    Protocol = "UDP"
                                }
                            },
                            Health = new V1Health
                            {
                                PeriodSeconds = 5,
                                FailureThreshold = 3,
                                InitialDelaySeconds = 5
                            },
                            Scheduling = "Packed",
                            SdkServer = new V1SdkServer
                            {
                                LogLevel = "Info",
                                GrpcPort = 9357,
                                HttpPort = 9358
                            },
                            Template = new V1PodTemplateSpec
                            {
                                Metadata = new V1ObjectMeta(),
                                Spec = new V1PodSpec
                                {
                                    Containers = new List<V1Container>
                                    {
                                        new V1Container
                                        {
                                            Image = "us-docker.pkg.dev/agones-images/examples/simple-game-server:0.14",
                                            Name = "simple-game-server2",
                                            Resources = new V1ResourceRequirements
                                            {
                                                Limits = new Dictionary<string, ResourceQuantity>
                                                {
                                                    {
                                                        "cpu",
                                                        new ResourceQuantity
                                                        {
                                                            //Format = SuffixFormat.DecimalSI,
                                                            Value = "20m"
                                                        }
                                                    },
                                                    {
                                                        "memory",
                                                        new ResourceQuantity
                                                        {
                                                            //Format = SuffixFormat.BinarySI,
                                                            Value = "64Mi"
                                                        }
                                                    }
                                                },
                                                Requests = new Dictionary<string, ResourceQuantity>
                                                {
                                                    {
                                                        "cpu",
                                                        new ResourceQuantity
                                                        {
                                                            //Format = SuffixFormat.DecimalSI,
                                                            Value = "20m"
                                                        }
                                                    },
                                                    {
                                                        "memory",
                                                        new ResourceQuantity
                                                        {
                                                            //Format = SuffixFormat.BinarySI,
                                                            Value = "64Mi"
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        },
                        Status = new V1GameServerStatus
                        {
                            State = "Ready",
                            Ports = new List<V1GameServerStatusPort>
                            {
                                new V1GameServerStatusPort
                                {
                                    Name = "default",
                                    Port = 7663
                                }
                            },
                            Address = "192.168.65.4",
                            NodeName = "docker-desktop"
                        },
                        Metadata = new V1ObjectMeta
                        {
                            Annotations = new Dictionary<string, string>
                            {
                                {
                                    "agones.dev/ready-container-id",
                                    "docker://3719b4a7f78f640c83c0115caf8134db08fdcd78b77cc6b2bbe7c0bebf3d2169"
                                },
                                {
                                    "agones.dev/sdk-version",
                                    "1.29.0"
                                }
                            },
                            CreationTimestamp = new DateTime(2023, 5, 13, 10, 17, 51, 0, DateTimeKind.Utc),
                            Finalizers = new List<string>
                            {
                                "agones.dev"
                            },
                            GenerateName = "simple-game-server2-tdh82-",
                            Generation = 10,
                            Labels = new Dictionary<string, string>
                            {
                                {
                                    "agones.dev/fleet",
                                    "simple-game-server2"
                                },
                                {
                                    "agones.dev/gameserverset",
                                    "simple-game-server2-tdh82"
                                }
                            },
                            ManagedFields = new List<V1ManagedFieldsEntry>
                            {
                                new V1ManagedFieldsEntry
                                {
                                    ApiVersion = "agones.dev/v1",
                                    FieldsType = "FieldsV1",
                                    FieldsV1 = new JsonElement(),
                                    Manager = "controller",
                                    Operation = "Update",
                                    Time = new DateTime(2023, 5, 21, 5, 43, 16, 0, DateTimeKind.Utc)
                                }
                            },
                            Name = "simple-game-server2-tdh82-49gt9",
                            NamespaceProperty = "default",
                            OwnerReferences = new List<V1OwnerReference>
                            {
                                new V1OwnerReference
                                {
                                    ApiVersion = "agones.dev/v1",
                                    BlockOwnerDeletion = true,
                                    Controller = true,
                                    Kind = "GameServerSet",
                                    Name = "simple-game-server2-tdh82",
                                    Uid = "eb6066b7-4ea7-45a2-8de8-91fcfae6649d"
                                }
                            },
                            ResourceVersion = "475269",
                            Uid = "ec91462a-b23a-4b48-b682-b478a331d415"
                        },
                        ApiVersion = "agones.dev/v1",
                        Kind = "GameServer"
                    },
                    new V1GameServer
                    {
                        Spec = new V1GameServerSpec
                        {
                            Container = "simple-game-server2",
                            Ports = new List<V1GameServerPort>
                            {
                                new V1GameServerPort
                                {
                                    Name = "default",
                                    PortPolicy = "Dynamic",
                                    Container = "simple-game-server2",
                                    ContainerPort = 7654,
                                    HostPort = 7173,
                                    Protocol = "UDP"
                                }
                            },
                            Health = new V1Health
                            {
                                PeriodSeconds = 5,
                                FailureThreshold = 3,
                                InitialDelaySeconds = 5
                            },
                            Scheduling = "Packed",
                            SdkServer = new V1SdkServer
                            {
                                LogLevel = "Info",
                                GrpcPort = 9357,
                                HttpPort = 9358
                            },
                            Template = new V1PodTemplateSpec
                            {
                                Metadata = new V1ObjectMeta(),
                                Spec = new V1PodSpec
                                {
                                    Containers = new List<V1Container>
                                    {
                                        new V1Container
                                        {
                                            Image = "us-docker.pkg.dev/agones-images/examples/simple-game-server:0.14",
                                            Name = "simple-game-server2",
                                            Resources = new V1ResourceRequirements
                                            {
                                                Limits = new Dictionary<string, ResourceQuantity>
                                                {
                                                    {
                                                        "cpu",
                                                        new ResourceQuantity
                                                        {
                                                            //Format = SuffixFormat.DecimalSI,
                                                            Value = "20m"
                                                        }
                                                    },
                                                    {
                                                        "memory",
                                                        new ResourceQuantity
                                                        {
                                                            //Format = SuffixFormat.BinarySI,
                                                            Value = "64Mi"
                                                        }
                                                    }
                                                },
                                                Requests = new Dictionary<string, ResourceQuantity>
                                                {
                                                    {
                                                        "cpu",
                                                        new ResourceQuantity
                                                        {
                                                            //Format = SuffixFormat.DecimalSI,
                                                            Value = "20m"
                                                        }
                                                    },
                                                    {
                                                        "memory",
                                                        new ResourceQuantity
                                                        {
                                                            //Format = SuffixFormat.BinarySI,
                                                            Value = "64Mi"
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        },
                        Status = new V1GameServerStatus
                        {
                            State = "Ready",
                            Ports = new List<V1GameServerStatusPort>
                            {
                                new V1GameServerStatusPort
                                {
                                    Name = "default",
                                    Port = 7173
                                }
                            },
                            Address = "192.168.65.4",
                            NodeName = "docker-desktop"
                        },
                        Metadata = new V1ObjectMeta
                        {
                            Annotations = new Dictionary<string, string>
                            {
                                {
                                    "agones.dev/sdk-version",
                                    "1.29.0"
                                }
                            },
                            CreationTimestamp = new DateTime(2023, 5, 21, 5, 43, 25, 0, DateTimeKind.Utc),
                            Finalizers = new List<string>
                            {
                                "agones.dev"
                            },
                            GenerateName = "simple-game-server2-tdh82-",
                            Generation = 6,
                            Labels = new Dictionary<string, string>
                            {
                                {
                                    "agones.dev/fleet",
                                    "simple-game-server2"
                                },
                                {
                                    "agones.dev/gameserverset",
                                    "simple-game-server2-tdh82"
                                }
                            },
                            ManagedFields = new List<V1ManagedFieldsEntry>
                            {
                                new V1ManagedFieldsEntry
                                {
                                    ApiVersion = "agones.dev/v1",
                                    FieldsType = "FieldsV1",
                                    FieldsV1 = new JsonElement(),
                                    Manager = "controller",
                                    Operation = "Update",
                                    Time = new DateTime(2023, 5, 21, 5, 43, 27, 0, DateTimeKind.Utc)
                                }
                            },
                            Name = "simple-game-server2-tdh82-v5pkr",
                            NamespaceProperty = "default",
                            OwnerReferences = new List<V1OwnerReference>
                            {
                                new V1OwnerReference
                                {
                                    ApiVersion = "agones.dev/v1",
                                    BlockOwnerDeletion = true,
                                    Controller = true,
                                    Kind = "GameServerSet",
                                    Name = "simple-game-server2-tdh82",
                                    Uid = "eb6066b7-4ea7-45a2-8de8-91fcfae6649d"
                                }
                            },
                            ResourceVersion = "475467",
                            Uid = "b9211e66-5260-4c02-a1ff-354194201a44"
                        },
                        ApiVersion = "agones.dev/v1",
                        Kind = "GameServer"
                    }
                },
                ApiVersion = "agones.dev/v1",
                Kind = "GameServerList"
            };

        }
    }
}
