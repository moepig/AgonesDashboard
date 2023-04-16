using AgonesDashboard.ViewModels.GameServer;

namespace AgonesDashboard.Services
{
    public interface IGameServerService
    {
        public Task<GameServerIndex> ListAsync();
        public Task<Detail> DetailAsync(string ns, string name);
        public Task<Delete> DeleteAsync(string ns, string name);
    }
}
