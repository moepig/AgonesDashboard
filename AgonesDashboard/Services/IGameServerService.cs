using AgonesDashboard.ViewModels.GameServer;

namespace AgonesDashboard.Services
{
    public interface IGameServerService
    {
        public Task<GameServerIndex> List();
        public Task<Detail> Detail(string ns, string name);
        public Task<Delete> Delete(string ns, string name);
    }
}
