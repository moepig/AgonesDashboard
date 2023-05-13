using AgonesDashboard.Filters;
using AgonesDashboard.ViewModels.GameServer;
using Microsoft.AspNetCore.Mvc;

namespace AgonesDashboard.Controllers
{
    [DevelopmentOnly]
    public class MockGameServerController : Controller
    {
        private readonly ILogger<GameServerController> _logger;

        public MockGameServerController(ILogger<GameServerController> logger)
        {
            _logger = logger;
        }

        public ViewResult Index()
        {
            var gameServer1 = new GameServerSimple
            {
                Name = "gameServer1",
                State = "Healty",
                Address = "1.2.3.4",
                GameServerSimpleContainer = new List<GameServerSimpleContainer>
                {
                    new GameServerSimpleContainer { Name = "container1", Image = "image1", ContainerPort = 1080, HostPort = 10, Protocol = "TCP", },
                    new GameServerSimpleContainer { Name = "container2", Image = "image2", ContainerPort = 2080, HostPort = 20, Protocol = "UDP", },
                    new GameServerSimpleContainer { Name = "container3", Image = "image3", ContainerPort = 3080, HostPort = 30, Protocol = "TCP", }
                }
            };

            var gameServer2 = new GameServerSimple
            {
                Name = "gameServer2",
                State = "Unhealty",
                Address = "192.168.100.200",
                GameServerSimpleContainer = new List<GameServerSimpleContainer>
                {
                    new GameServerSimpleContainer { Name = "container2", Image = "image2", ContainerPort = 2080, HostPort = 20, Protocol = "UDP", },
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

            var viewModel = new ViewModels.GameServer.GameServerIndex
            {
                GameServers = (IDictionary<string, IList<GameServerSimple>>)gameServers,
                ContainerTotal = containerTotal,
            };

            return View("Views/GameServer/Index.cshtml", viewModel);
        }
    }
}
