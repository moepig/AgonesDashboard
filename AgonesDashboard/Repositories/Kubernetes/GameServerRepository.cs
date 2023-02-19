
namespace AgonesDashboard.Repositories.Kubernetes
{
    public class GameServerRepository
    {
        private readonly ILogger<GameServerRepository> _logger;

        public GameServerRepository(ILogger<GameServerRepository> logger)
        {
            _logger = logger;
        }
    } 
}
