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
            var viewModel = await _gameServerService.ListAsync();

            return View(viewModel);
        }

        public async Task<ViewResult> Detail(string ns, string name)
        {
            var viewModel = await _gameServerService.DetailAsync(ns, name);

            return View(viewModel);
        }

        [HttpPost]
        public async Task<ViewResult> Delete(string ns, string name)
        {
            var viewModel = await _gameServerService.DeleteAsync(ns, name);

            return View(viewModel);
        }
    }
}
