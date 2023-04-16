using AgonesDashboard.Repositories;
using AgonesDashboard.ViewModels.Service;

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

        public async Task<ServiceIndex> ListAsync()
        {
            var ns = _configuration.GetValue("Agones:Namespace", "agones-system");
            var services = await _serviceRepository.ListAsync(ns);
            var list = new List<ServiceSimple>();

            foreach (var service in services.Items)
            {
                var item = new ServiceSimple
                {
                    Name = service.Metadata.Name,
                    ExternalName = service.Spec.ExternalName,
                    ExternalIPs = service.Spec.ExternalIPs ?? new List<string>(),
                    ClusterIPs = service.Spec.ClusterIPs ?? new List<string>(),
                    Ports = service.Spec.Ports?.Select(x => $"{x.Port}/{x.Protocol}").ToList() ?? new List<string>(),
                };

                list.Add(item);
            }

            var viewModel = new ServiceIndex
            {
                Namespace = ns,
                Allocators = list,
            };

            return viewModel;
        }
    }
}
