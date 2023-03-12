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

            if (viewModel is null)
            {
                _logger.LogError("viewModel is null");
                throw new Exception();
            }

            return View(viewModel);
        }
    }
}
