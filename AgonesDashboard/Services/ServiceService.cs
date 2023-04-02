using AgonesDashboard.Repositories;
using AgonesDashboard.ViewModels.Allocator;

namespace AgonesDashboard.Services
{
    public class ServiceService : IServiceService
    {
        private readonly IConfiguration _configuration;
        private readonly IServiceRepository _serviceRepository;

        public ServiceService(IConfiguration configuration, IServiceRepository serviceRepository)
        {
            _configuration = configuration;
            _serviceRepository = serviceRepository;
        }

        public async Task<AllocatorIndex> List()
        {
            var ns = _configuration.GetValue("Agones:Namespace", "agones-system");
            var services = await _serviceRepository.ListAsync(ns);
            var list = new List<AllocatorSimple>();

            foreach (var service in services.Items)
            {
                var item = new AllocatorSimple
                {
                    Name = service.Metadata.Name,
                    ExternalName = service.Spec.ExternalName,
                    ExternalIPs = service.Spec.ExternalIPs ?? new List<string>(),
                    ClusterIPs = service.Spec.ClusterIPs ?? new List<string>(),
                    Ports = service.Spec.Ports?.Select(x => x.Port).ToList() ?? new List<int>(),
                };

                list.Add(item);
            }

            var viewModel = new AllocatorIndex
            {
                Namespace = ns,
                Allocators = list,
            };

            return viewModel;
        }
    }
}
