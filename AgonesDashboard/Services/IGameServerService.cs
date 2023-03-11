namespace AgonesDashboard.Services
{
    public interface IGameServerService
    {
        public Task<ViewModels.GameServer.GameServerList?> List();
    }
}
