using AgonesDashboard.Filters;
using AgonesDashboard.Repositories;
using AgonesDashboard.Repositories.Kubernetes;
using AgonesDashboard.Services;
using AgonesDashboard.ViewModels.GameServer;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace AgonesDashboard.Controllers
{
    public class GameServerController : Controller
    {
        private readonly ILogger<GameServerController> _logger;
        private readonly IGameServerService _gameServerService;

        public GameServerController(
            ILogger<GameServerController> logger,
            IGameServerRepository gameServerRepository,
            IGameServerService gameServerService
        )
        {
            _logger = logger;
            _gameServerService = gameServerService;
        }

        public async Task<ViewResult> Index()
        {
            var viewModel = await _gameServerService.List();

            if (viewModel is null)
            {
                _logger.LogError("viewModel is null");
                throw new Exception();
            }

            return View(viewModel);
        }

        [DevelopmentOnly]
        public ViewResult IndexViewTest()
        {
            var gameServer1 = new GameServerSimple
            {
                Name = "gameServer1",
                ContainerPort = 8080,
                HostPort = 80,
                Protocol = "TCP",
                State = "Healty",
                Address = "1.2.3.4",
                GameServerSimpleContainer = new List<GameServerSimpleContainer>
                {
                    new GameServerSimpleContainer { Name = "container1", Image = "image1" },
                    new GameServerSimpleContainer { Name = "container2", Image = "image2" },
                    new GameServerSimpleContainer { Name = "container3", Image = "image3" }
                }
            };

            var gameServer2 = new GameServerSimple
            {
                Name = "gameServer2",
                ContainerPort = 9090,
                HostPort = 90,
                Protocol = "UDP",
                State = "Unhealty",
                Address = "192.168.100.200",
                GameServerSimpleContainer = new List<GameServerSimpleContainer>
                {
                    new GameServerSimpleContainer { Name = "container3", Image = "image3" },
                }
            };

            var gameServers = new Dictionary<string, IList<GameServerSimple>>
            {
                { "namespace1", new List<GameServerSimple> { gameServer1 } },
                { "namespace2", new List<GameServerSimple> { gameServer2 } }
            };

            var containerTotal = new Dictionary<string, int>();

            foreach (var (k, v) in gameServers)
            {
                var count = v.SelectMany(x => x.GameServerSimpleContainer).Count();
                containerTotal.Add(k, count);
            }

            var viewModel = new ViewModels.GameServer.GameServerList
            {
                GameServers = (IDictionary<string, IList<GameServerSimple>>)gameServers,
                ContainerTotal = containerTotal,
            };

            return View("Index", viewModel);
        }
    }
}
