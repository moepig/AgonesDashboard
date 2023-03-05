using AgonesDashboard.Repositories;
using AgonesDashboard.Repositories.Kubernetes;
using AgonesDashboard.Services;
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
        public async Task<string> Test()
        {
            var list = await _gameServerRepository.ListAsync();

            return JsonSerializer.Serialize(list);
        }
    }
}
