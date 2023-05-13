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

        public async Task<GameServerIndex> ListAsync()
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

                // コンテナ名: ポート情報 な Dictionary
                // "unknown" は無意味な文字列で、p.Container -> null の時の動作は諦めている
                var ports = item.Spec.Ports?.ToDictionary(p => p.Container ?? "unknown");

                // 1 GameServer に含まれるコンテナをまとめるリスト
                var simpleContainers = new List<GameServerSimpleContainer>();

                // 各コンテナを ViewModel で扱う形式に変換
                foreach (var container in item.Spec.Template.Spec.Containers)
                {
                    var simpleContainer = new GameServerSimpleContainer
                    {
                        Name = container.Name,
                        Image = container.Image,
                        ContainerPort = ports != null ? ports[container.Name].ContainerPort : -1,
                        HostPort = ports != null ? ports[container.Name].HostPort : -1,
                        Protocol = ports != null ? (ports[container.Name].Protocol ?? "error") : "error",
                    };
                    simpleContainers.Add(simpleContainer);
                }

                var gameServer = new GameServerSimple
                {
                    Name = item.Metadata?.Name ?? "error",
                    GameServerSimpleContainer = simpleContainers,
                    Address = item.Status?.Address ?? "error",
                    State = item.Status?.State ?? "error",
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
        public async Task<Detail> DetailAsync(string ns, string name)
        {
            var gameServer = await _gameServerRepository.GetAsync(ns, name);

            var viewModel = new Detail
            {
                GameServer = gameServer,
            };

            return viewModel;
        }

        public async Task<Delete> DeleteAsync(string ns, string name)
        {
            var gameServer = await _gameServerRepository.DeleteAsync(ns, name);

            var viewModel = new Delete
            {
                GameServer = gameServer,
            };

            return viewModel;
        }
    }
}
