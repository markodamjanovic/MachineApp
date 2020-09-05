using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MachineApp.Models;

namespace MachineApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDatabaseRepository _databaseRepository;
        public HomeController(ILogger<HomeController> logger, IDatabaseRepository databaseRepository )
        {
            _logger = logger;
            _databaseRepository = databaseRepository;
        }

        public async Task<IActionResult> Index()
        {   
            IEnumerable<Machine> model = await _databaseRepository.GetMachines();
            return View(model);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
