using AgonesDashboard.Models.Kubernetes.CustomResources.Agones;
using AgonesDashboard.Repositories;
using AgonesDashboard.ViewModels.GameServerSet;

namespace AgonesDashboard.Services
{
    public class GameServerSetService : IGameServerSetService
    {
        private readonly ILogger<GameServerService> _logger;
        private readonly IGameServerSetRepository _gameServerSetRepository;

        public GameServerSetService(ILogger<GameServerService> logger, IGameServerSetRepository gameServerSetRepository)
        {
            _logger = logger;
            _gameServerSetRepository = gameServerSetRepository;
        }

        public async Task<GameServerSetIndex> List()
        {
            var list = await _gameServerSetRepository.ListAsync();

            // <namespace, その namespace の GameServerSet> な Dictionary
            var sets = new Dictionary<string, IList<GameServerSetSimple>>();

            foreach (var item in list.Items ?? new List<V1GameServerSet>())
            {
                var gsSet = new GameServerSetSimple
                {
                    Name = item?.Metadata?.Name,
                    Scheduling = item?.Spec?.Scheduling,
                    ReadyReplicas = item?.Status?.ReadyReplicas,
                    ReservedReplicas = item?.Status?.ReservedReplicas,
                    AllocatedReplicas = item?.Status?.AllocatedReplicas,
                    ShutdownReplicas = item?.Status?.ShutdownReplicas,

                };

                // 後続処理のための事前 null チェック
                if (item?.Metadata is null || item?.Metadata.NamespaceProperty is null)
                {
                    continue;
                }

                // 当該 namespace の GameServerSet 要素を格納するリストを取得
                IList<GameServerSetSimple>? addTarget;
                var isExistKey = sets.TryGetValue(item.Metadata.NamespaceProperty, out addTarget);

                // まだ存在してなかったら初期化
                if (!isExistKey || addTarget is null)
                {
                    addTarget = new List<GameServerSetSimple>();
                    sets.Add(item.Metadata.NamespaceProperty, addTarget);
                }

                addTarget.Add(gsSet);
            }

            var viewModel = new GameServerSetIndex()
            {
                GameServerSets = sets,
            };

            return viewModel;
        }

        public async Task<Detail> Detail(string ns, string name)
        {
            var gsSet = await _gameServerSetRepository.GetAsync(ns, name);

            var viewModel = new Detail
            {
                GameServerSet = gsSet,
            };

            return viewModel;
        }
    }
}
