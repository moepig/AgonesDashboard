namespace AgonesDashboard.Services
{
    public interface IGameServerService
    {
        public Task<ViewModels.GameServer.Index?> List();
    }
}
