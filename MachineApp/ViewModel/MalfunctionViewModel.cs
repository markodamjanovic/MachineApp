using System;
using System.ComponentModel.DataAnnotations;

namespace MachineApp.ViewModel
{
    public class MalfunctionViewModel
    {
        public int id { get; set; }
        
        [Required]
        [DataType(DataType.Text)]
        public string Name { get; set; }
        
        [DataType(DataType.Text)]
        public string Description { get; set; }
        
        [Required]
        public int MachineId { get; set; }

        [Required]
        [VerifyMachineName()]
        public string MachineName {get; set;}
        
        public bool Status { get; set; }
        
        public DateTime Created { get; set; }
    }
}