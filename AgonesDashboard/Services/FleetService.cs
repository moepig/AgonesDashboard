using AgonesDashboard.Models.Kubernetes.CustomResources.Agones;
using AgonesDashboard.Models.Kubernetes.CustomResources.Agones.Allocation;
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

        public FleetService(ILogger<FleetService> logger, IFleetRepository fleetRepository, IGameServerAllocationRepository gameServerAllocationRepository)
        {
            _logger = logger;
            _fleetRepository = fleetRepository;
            _gameServerAllocationRepository = gameServerAllocationRepository;
        }

        public async Task<FleetIndex> List()
        {
            var list = await _fleetRepository.ListAsync();

            // <namespace, その namespace の Fleet 一覧> な Dictionary
            var fleets = new Dictionary<string, IList<FleetSimple>>();

            foreach (var item in list.Items ?? new List<V1Fleet>())
            {
                var fleet = new FleetSimple
                {
                    Name = item?.Metadata?.Name,
                    Scheduling = item?.Spec?.Scheduling,
                    ReadyReplicas = item?.Status?.ReadyReplicas,
                    ReservedReplicas = item?.Status?.ReservedReplicas,
                    AllocatedReplicas = item?.Status?.AllocatedReplicas,
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

        public async Task<Detail> Detail(string ns, string name)
        {
            var fleet = await _fleetRepository.GetAsync(ns, name);

            var viewModel = new Detail
            {
                Fleet = fleet,
            };

            return viewModel;
        }

        public async Task<Allocation> Allocate(string ns, string fleetName)
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
