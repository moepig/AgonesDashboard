using AgonesDashboard.Services;
using Microsoft.AspNetCore.Mvc;

namespace AgonesDashboard.Controllers
{
    public class FleetController : Controller
    {
        private readonly ILogger<FleetController> _logger;
        private readonly IFleetService _fleetService;

        public FleetController(
            ILogger<FleetController> logger,
            IFleetService fleetService
        )
        {
            _logger = logger;
            _fleetService = fleetService;
        }

        public async Task<ViewResult> Index()
        {
            var viewModel = await _fleetService.List();

            return View(viewModel);
        }
        public async Task<ViewResult> Detail(string ns, string name)
        {
            var viewModel = await _fleetService.Detail(ns, name);

            return View(viewModel);
        }
    }
}
