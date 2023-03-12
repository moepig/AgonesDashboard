using AgonesDashboard.ViewModels.GameServer;

namespace AgonesDashboard.Services
{
    public interface IGameServerService
    {
        public Task<GameServerIndex?> List();
    }
}
