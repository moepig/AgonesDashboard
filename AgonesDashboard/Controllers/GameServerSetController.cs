using AgonesDashboard.Services;
using Microsoft.AspNetCore.Mvc;

namespace AgonesDashboard.Controllers
{
    public class GameServerSetController : Controller
    {
        private readonly ILogger<FleetController> _logger;
        private readonly IGameServerSetService _gameServerSetService;

        public GameServerSetController(
            ILogger<FleetController> logger,
            IGameServerSetService gameServerSetService
        )
        {
            _logger = logger;
            _gameServerSetService = gameServerSetService;
        }

        public async Task<ViewResult> Index()
        {
            var viewModel = await _gameServerSetService.List();

            return View(viewModel);
        }
        public async Task<ViewResult> Detail(string ns, string name)
        {
            var viewModel = await _gameServerSetService.Detail(ns, name);

            return View(viewModel);
        }
    }
}
