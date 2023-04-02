using k8s.Models;

namespace AgonesDashboard.Repositories
{
    public interface IServiceRepository
    {
        public Task<V1ServiceList> ListAsync(string ns);
        public Task<V1Service> GetAsync(string ns, string name);
    }
}
