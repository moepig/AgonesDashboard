using AgonesDashboard.Models.Kubernetes.CustomResources.Agones;
using AgonesDashboard.Repositories;
using AgonesDashboard.ViewModels.Fleet;
using static AgonesDashboard.ViewModels.Fleet.FleetIndex;

namespace AgonesDashboard.Services
{
    public class FleetService : IFleetService
    {
        private readonly ILogger<FleetService> _logger;
        private readonly IFleetRepository _fleetRepository;

        public FleetService(ILogger<FleetService> logger, IFleetRepository fleetRepository)
        {
            _logger = logger;
            _fleetRepository = fleetRepository;
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
                    GameServerName = item?.Spec?.Template?.Metadata?.Name,
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

    }
}
