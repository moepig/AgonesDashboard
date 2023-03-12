using AgonesDashboard.Filters;
using AgonesDashboard.ViewModels.Fleet;
using Microsoft.AspNetCore.Mvc;

namespace AgonesDashboard.Controllers
{

    [DevelopmentOnly]
    public class MockFleetController : Controller
    {
        private readonly ILogger<MockFleetController> _logger;

        public MockFleetController(ILogger<MockFleetController> logger)
        {
            _logger = logger;
        }

        public ViewResult Index()
        {
            var fleets = new Dictionary<string, IList<FleetSimple>>
            {
                {
                    "namespace1", new List<FleetSimple>
                    {
                        new FleetSimple
                        {
                            Name = "fleet1",
                            GameServerName = "server1",
                            Scheduling = "Packed",
                            ReadyReplicas = 5,
                            ReservedReplicas = 1,
                            AllocatedReplicas = 6
                        },
                        new FleetSimple
                        {
                            Name = "fleet2",
                            GameServerName = "server2",
                            Scheduling = "Distributed",
                            ReadyReplicas = 3,
                            ReservedReplicas = 0,
                            AllocatedReplicas = 3
                        }
                    }
                },
                {
                    "namespace2", new List<FleetSimple>
                    {
                        new FleetSimple
                        {
                            Name = "fleet3",
                            GameServerName = "server1",
                            Scheduling = "Packed",
                            ReadyReplicas = 2,
                            ReservedReplicas = 1,
                            AllocatedReplicas = 3
                        }
                    }
                }
            };

            var viewModel = new FleetIndex
            {
                Fleets = fleets,
            };

            return View("Views/Fleet/Index.cshtml", viewModel);
        }
    }
}