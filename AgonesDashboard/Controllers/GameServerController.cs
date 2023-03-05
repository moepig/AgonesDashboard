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
        private readonly ILogger<GameServerRepository> _logger;
        private readonly IGameServerRepository _gameServerRepository;
        private readonly IGameServerService _gameServerService;

        public GameServerController(
            ILogger<GameServerRepository> logger,
            IGameServerRepository gameServerRepository,
            IGameServerService gameServerService
        )
        {
            _logger = logger;
            _gameServerRepository = gameServerRepository;
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

            var viewModel = new ViewModels.GameServer.Index
            {
                GameServers = (IDictionary<string, IList<GameServerSimple>>)gameServers,
                ContainerTotal = containerTotal,
            };

            return View("Index", viewModel);
        }
        
        public async Task<string> Test()
        {
            var list = await _gameServerRepository.ListAsync();

            return JsonSerializer.Serialize(list);
        }
    }
}
