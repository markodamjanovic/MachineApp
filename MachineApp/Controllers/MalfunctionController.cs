using System.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using MachineApp.Models;
using MachineApp.ViewModel;
using MachineApp.Utility;
using MachineApp.Data;

namespace MachineApp.Controllers
{
    public class MalfunctionController : Controller
    {
        private readonly IDatabaseRepository _databaseRepository;
        private readonly IFileService _fileService;

        public MalfunctionController(IDatabaseRepository databaseRepository, IFileService fileService)
        {
            _databaseRepository = databaseRepository;
            _fileService = fileService;
        }
        public IActionResult Index()
        {   
            return RedirectToAction("MalfunctionTable", "Malfunction");
        }

        public async Task<IActionResult> MalfunctionsTable()
        {   
            IEnumerable<Malfunction> malfunctions = await _databaseRepository.GetAll<Malfunction>();
            IEnumerable<Machine> machines = await _databaseRepository.GetAll<Machine>();

            var model = malfunctions.Join(
                machines,
                malf => malf.MachineId,
                mach => mach.id,
                (malfunction, machine) => new MalfunctionViewModel
                {
                    id = malfunction.id,
                    Name = malfunction.Name,
                    Description = malfunction.Description,
                    MachineId = machine.id,
                    MachineName = machine.Name,
                    Status = malfunction.Status,
                    File = malfunction.File,
                    Created = malfunction.Created
                });

            return View(model);
        }

        [HttpGet]
        public IActionResult Download(string file)
        {
            if( file != null && _fileService.FileExits(file))
            {
                string fullFilePath = _fileService.ZipDirectory(file);
                var mimeType = _fileService.GetMimeType(file);
                
                return PhysicalFile(fullFilePath, mimeType, file);
            }
            else
            {
                return RedirectToAction("MalfunctionsTable", "Malfunction");
            }
        }

        [HttpPost]
        public async Task<IActionResult> InsertOrUpdate(MalfunctionEditViewModel model)
        {   
            var machines = await _databaseRepository.GetAll<Machine>();
            model.Machines = machines;

            bool result;
            if (ModelState.IsValid)
            {               
                var machineId = machines.Where(m=> m.Name == model.Malfunction.MachineName).Select(m => m.id).FirstOrDefault();
                
                Malfunction malfunction = new Malfunction
                {
                    id = model.Malfunction.id,
                    Name = model.Malfunction.Name,
                    Description = model.Malfunction.Description,
                    MachineId = machineId,
                    File = model.Malfunction.File,
                    Status = model.Malfunction.Status,
                    Created = model.Malfunction.Created,
                };

                if (model.Malfunction.Files != null)
                {
                    var zipName = _fileService.CreateZip(model.Malfunction.File, model.Malfunction.Files);
                    
                    if(zipName == null)
                    {
                        ModelState.AddModelError("Malfunction.Files","Error while uploading zip file");
                        return View("Edit", model);
                    }
                    else
                    {
                        malfunction.File = zipName;
                    }
                }

                if (malfunction.id == 0)
                {
                    malfunction.Created = DateTime.Now;
                    result = await _databaseRepository.Insert<Malfunction>(malfunction) > 0;
                }
                else
                {
                    result = await _databaseRepository.Update<Malfunction>(malfunction) != null;
                }
                
                if(result)
                {
                    return RedirectToAction("MalfunctionsTable", "Malfunction");
                }
            }
            
            return View("Edit", model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id)
        {   
            IEnumerable<Machine> machines = await _databaseRepository.GetAll<Machine>();
            MalfunctionEditViewModel model = new MalfunctionEditViewModel{Machines=machines, Malfunction = new MalfunctionViewModel()};
            
            if(ModelState.IsValid)
            {                 
                IEnumerable<Malfunction> malfunctions = new Malfunction[]{await _databaseRepository.Get<Malfunction>(id)};

                if (malfunctions.First() != null)
                {
                   var malfunctionViewModel = malfunctions.Join(
                        machines,
                        malf => malf.MachineId,
                        mach => mach.id,
                        (malfunction, machine) => new MalfunctionViewModel
                        {
                            id = malfunction.id,
                            Name = malfunction.Name,
                            Description = malfunction.Description,
                            MachineId = machine.id,
                            MachineName = machine.Name,
                            Status = malfunction.Status,
                            File = malfunction.File,
                            Created = malfunction.Created
                        });


                    if (malfunctionViewModel.Any())
                    {   
                        model.Malfunction = malfunctionViewModel.First();
                    }
                }
            }

            return View(model);
        }
        
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {   
            Malfunction model = await _databaseRepository.Get<Malfunction>(id);
            var result = await _databaseRepository.Delete<Malfunction>(model);
            
            _fileService.DeleteFile(model.File);

            return RedirectToAction("MalfunctionsTable", "Malfunction");
        }

    }
}