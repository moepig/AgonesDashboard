using AgonesDashboard.ViewModels.GameServerSet;

namespace AgonesDashboard.Services
{
    public interface IGameServerSetService
    {
        public Task<GameServerSetIndex> ListAsync();
        public Task<Detail> DetailAsync(string ns, string name);
    }
}
