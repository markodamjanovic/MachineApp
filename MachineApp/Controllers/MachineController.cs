using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MachineApp.Models;

namespace MachineApp.Controllers
{
    public class MachineController : Controller
    {
        private readonly IDatabaseRepository _databaseRepository;

        public MachineController(IDatabaseRepository databaseRepository)
        {
            _databaseRepository = databaseRepository;
        }

        [HttpGet]
        public async Task<IActionResult> MachineTable()
        {   
            IEnumerable<Machine> model = await _databaseRepository.GetAll<Machine>();
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {   
            Machine model = await _databaseRepository.Get<Machine>(id);
            return View(model);
        }

    }
}