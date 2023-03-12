using AgonesDashboard.Models.Kubernetes.CustomResources.Agones;
using AgonesDashboard.Repositories;
using AgonesDashboard.ViewModels.GameServer;

namespace AgonesDashboard.Services
{
    public class GameServerService : IGameServerService
    {
        private readonly ILogger<GameServerService> _logger;
        private readonly IGameServerRepository _gameServerRepository;

        public GameServerService(ILogger<GameServerService> logger, IGameServerRepository gameServerRepository)
        {
            _logger = logger;
            _gameServerRepository = gameServerRepository;
        }

        public async Task<Detail> Detail(string ns, string name)
        {
            var gameServer = await _gameServerRepository.GetAsync(ns, name);

            var viewModel = new Detail
            {
                GameServer = gameServer,
            };

            return viewModel;
        }

        public async Task<GameServerIndex> List()
        {
            var list = await _gameServerRepository.ListAsync();

            // <namespace, その namespace の GameServer 一覧> な Dictionary
            var gameServers = new Dictionary<string, IList<GameServerSimple>>();

            foreach (var item in list.Items ?? new List<V1GameServer>())
            {
                // 後続処理のための事前 null チェック
                if (item.Spec?.Template?.Spec is null || item.Spec.Template.Spec.Containers is null)
                {
                    continue;
                }

                // 1 GameServer に含まれるコンテナをまとめるリスト
                var simpleContainers = new List<GameServerSimpleContainer>();

                // 各コンテナを ViewModel で扱う形式に変換
                foreach (var container in item.Spec.Template.Spec.Containers)
                {
                    var simpleContainer = new GameServerSimpleContainer
                    {
                        Name = container.Name,
                        Image = container.Image,
                    };
                    simpleContainers.Add(simpleContainer);
                }

                // ゲームサーバコンテナに対応するポート情報を取得
                var port = item.Spec.Ports?
                    .FirstOrDefault(x => x.Container == item.Spec.Container);

                var gameServer = new GameServerSimple
                {
                    Name = item.Metadata?.Name,
                    GameServerSimpleContainer = simpleContainers,
                    Address = item.Status?.Address,
                    ContainerPort = port?.ContainerPort,
                    HostPort = port?.HostPort,
                    Protocol = port?.Protocol,
                    State = item.Status?.State,
                };

                // 後続処理のための事前 null チェック
                if (item.Metadata is null || item.Metadata.NamespaceProperty is null)
                {
                    continue;
                }

                // 当該 namespace の GameServer 要素を格納するリストを取得
                IList<GameServerSimple>? addTarget;
                var isExistKey = gameServers.TryGetValue(item.Metadata.NamespaceProperty, out addTarget);

                // まだ存在してなかったら初期化
                if (!isExistKey || addTarget is null)
                {
                    addTarget = new List<GameServerSimple>();
                    gameServers.Add(item.Metadata.NamespaceProperty, addTarget);
                }

                addTarget.Add(gameServer);
            }

            // namespace ごとのコンテナ数を求めて保存
            var containerTotal = new Dictionary<string, int>();

            foreach (var (k, v) in gameServers){
                var count = v.SelectMany(x => x.GameServerSimpleContainer).Count();
                containerTotal.Add(k, count);
            }

            var viewModel = new ViewModels.GameServer.GameServerIndex()
            {
                GameServers = gameServers,
                ContainerTotal = containerTotal,
            };

            return viewModel;
        }
    }
}
