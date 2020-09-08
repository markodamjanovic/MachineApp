using System.Collections.Generic;
using MachineApp.Models;

namespace MachineApp.ViewModel
{
    public class MalfunctionEditViewModel
    {
       public MalfunctionViewModel Malfunction {get; set;} 
       public IEnumerable<Machine> Machines {get; set;}
    }
}