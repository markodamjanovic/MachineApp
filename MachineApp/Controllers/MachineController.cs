using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MachineApp.Data;
using System.Linq;
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

        public async Task<IActionResult> MachineTable()
        {   
            IEnumerable<Machine> model = await _databaseRepository.GetAll<Machine>();
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int id)
        {   
            Machine model = new Machine{id = 0, Name=string.Empty, Description=string.Empty, Company=string.Empty};
            if(ModelState.IsValid)
            { 
                Machine machine = await _databaseRepository.Get<Machine>(id);
                
                if (machine != null)                {
                    model = machine;
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> InsertOrUpdate(Machine model)
        {   
            Machine newModel = new Machine();
            bool result;
            
            if (ModelState.IsValid)
            {   
                if (model.id == 0)
                {
                    result = await _databaseRepository.Insert<Machine>(model) > 0;
                }
                else
                {
                    result = await _databaseRepository.Update<Machine>(model) != null;
                }
                
                if(result)
                {
                    return RedirectToAction("MachineTable", "Machine");
                }
            }
            
            return View("Edit", newModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {   
            Machine model = await _databaseRepository.Get<Machine>(id);
            IEnumerable<Malfunction> malfunctions = await _databaseRepository.GetAll<Malfunction>();
            var malfunctionsToDelete = malfunctions.Where( m => m.MachineId == model.id);

            var malfunctionResult = _databaseRepository.DeleteList<Malfunction>(malfunctionsToDelete);
            
            if(malfunctionResult != null)
            {
                var result = await _databaseRepository.Delete<Machine>(model);
            }
            
            
            return RedirectToAction("MachineTable", "Machine");
        }

    }
}