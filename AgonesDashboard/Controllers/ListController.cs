using AgonesDashboard.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace AgonesDashboard.Controllers
{
    public class ListController : Controller
    {
        private readonly IGameServerRepository _gameServerRepository;

        public ListController(IGameServerRepository gameServerRepository)
        {
            _gameServerRepository = gameServerRepository;
        }

        public async Task<string> Index()
        {
            var list = await _gameServerRepository.ListAsync();

            return JsonSerializer.Serialize(list);
        }
    }
}
