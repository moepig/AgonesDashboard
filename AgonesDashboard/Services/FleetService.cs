using AgonesDashboard.Models.Kubernetes.CustomResources.Agones;
using AgonesDashboard.Models.Kubernetes.CustomResources.Agones.Allocation;
using AgonesDashboard.Models.Kubernetes.CustomResources.Agones.AutoScaling;
using AgonesDashboard.Repositories;
using AgonesDashboard.ViewModels.Fleet;
using k8s.Models;
using static AgonesDashboard.ViewModels.Fleet.FleetIndex;

namespace AgonesDashboard.Services
{
    public class FleetService : IFleetService
    {
        private readonly ILogger<FleetService> _logger;
        private readonly IFleetRepository _fleetRepository;
        private readonly IGameServerAllocationRepository _gameServerAllocationRepository;
        private readonly IFleetAutoscalerRepository _fleetAutoscalerRepository;

        public FleetService(ILogger<FleetService> logger, IFleetRepository fleetRepository, IGameServerAllocationRepository gameServerAllocationRepository, IFleetAutoscalerRepository fleetAutoscalerRepository)
        {
            _logger = logger;
            _fleetRepository = fleetRepository;
            _gameServerAllocationRepository = gameServerAllocationRepository;
            _fleetAutoscalerRepository = fleetAutoscalerRepository;
        }

        public async Task<FleetIndex> ListAsync()
        {
            // fleet 名 - autoscaler な組を作成
            // OPTIMIZE: リポジトリ呼び出しの並列化がよい
            var autoscalerResources = await _fleetAutoscalerRepository.ListAsync();
            var autoscalers = new Dictionary<string, V1FleetAutoscaler>();

            foreach (var autoscaler in autoscalerResources.Items ?? new List<V1FleetAutoscaler>())
            {
                var targetFleet = autoscaler?.Spec?.FleetName;
                if (targetFleet != null) {
                    autoscalers.Add(targetFleet, autoscaler);
                }
            }

            // OPTIMIZE: リポジトリ呼び出しの並列化がよい
            var fleetResources = await _fleetRepository.ListAsync();

            // <namespace, その namespace の Fleet 一覧> な Dictionary
            var fleets = new Dictionary<string, IList<FleetSimple>>();

            foreach (var item in fleetResources.Items ?? new List<V1Fleet>())
            {
                var fleetName = item?.Metadata?.Name;
                var fleet = new FleetSimple
                {
                    Name = fleetName ?? "error",
                    Scheduling = item?.Spec?.Scheduling ?? "error",
                    ReadyReplicas = item?.Status?.ReadyReplicas ?? -1,
                    ReservedReplicas = item?.Status?.ReservedReplicas ?? -1,
                    AllocatedReplicas = item?.Status?.AllocatedReplicas ?? -1,
                    IsAutoscalerEnabled = fleetName != null ? autoscalers.ContainsKey(fleetName) : false,
                };

                // 後続処理のための事前 null チェック
                if (item?.Metadata is null || item?.Metadata.NamespaceProperty is null)
                {
                    continue;
                }

                // 当該 namespace の Fleet 要素を格納するリストを取得
                IList<FleetSimple>? addTarget;
                var isExistKey = fleets.TryGetValue(item.Metadata.NamespaceProperty, out addTarget);

                // まだ存在してなかったら初期化
                if (!isExistKey || addTarget is null)
                {
                    addTarget = new List<FleetSimple>();
                    fleets.Add(item.Metadata.NamespaceProperty, addTarget);
                }

                addTarget.Add(fleet);
            }

            var viewModel = new FleetIndex()
            {
                Fleets = fleets,
            };

            return viewModel;
        }

        public async Task<Detail> DetailAsync(string ns, string name)
        {
            var fleet = await _fleetRepository.GetAsync(ns, name);

            var viewModel = new Detail
            {
                Fleet = fleet,
            };

            return viewModel;
        }

        public async Task<Allocation> AllocateAsync(string ns, string fleetName)
        {
            var gsAllocate = new V1GameServerAllocation
            {
                ApiVersion = "allocation.agones.dev/v1",
                Kind = "GameServerAllocation",
                Spec = new V1GameServerAllocationSpec
                {
                    Selectors = new List<V1GameServerSelector>
                    {
                        new V1GameServerSelector
                        {
                            MatchLabels =  new Dictionary<string, string>
                                {
                                    {"agones.dev/fleet", fleetName},
                                }
                        }
                    }
                }
            };

            var allocation = await _gameServerAllocationRepository.CreateAsync(ns, gsAllocate);

            var viewModel = new Allocation
            {
                GameServerAllocation = allocation,
            };

            return viewModel;
        }
    }
}
