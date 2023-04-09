using AgonesDashboard.ViewModels.GameServerSet;

namespace AgonesDashboard.Services
{
    public interface IGameServerSetService
    {
        public Task<GameServerSetIndex> List();
        public Task<Detail> Detail(string ns, string name);
    }
}
