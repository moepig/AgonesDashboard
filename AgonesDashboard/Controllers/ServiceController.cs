using AgonesDashboard.Services;
using Microsoft.AspNetCore.Mvc;

namespace AgonesDashboard.Controllers
{
    public class ServiceController : Controller
    {
        private readonly ILogger<ServiceController> _logger;
        private readonly IServiceService _serviceService;

        public ServiceController(
            ILogger<ServiceController> logger,
            IServiceService serviceService
        )
        {
            _logger = logger;
            _serviceService = serviceService;
        }
        public async Task<ViewResult> Index()
        {
            var viewModel = await _serviceService.List();

            return View(viewModel);
        }
    }
}
