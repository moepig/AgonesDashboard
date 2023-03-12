using AgonesDashboard.Services;
using Microsoft.AspNetCore.Mvc;

namespace AgonesDashboard.Controllers
{
    public class GameServerController : Controller
    {
        private readonly ILogger<GameServerController> _logger;
        private readonly IGameServerService _gameServerService;

        public GameServerController(
            ILogger<GameServerController> logger,
            IGameServerService gameServerService
        )
        {
            _logger = logger;
            _gameServerService = gameServerService;
        }

        public async Task<ViewResult> Index()
        {
            var viewModel = await _gameServerService.List();

            return View(viewModel);
        }
    }
}
