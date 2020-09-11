using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace MachineApp.ViewModel
{
    public class MalfunctionViewModel
    {
        public int id { get; set; }
        
        [Required]
        [DataType(DataType.Text)]
        [MaxLength(20)]
        public string Name { get; set; }
        
        [DataType(DataType.Text)]
        public string Description { get; set; }
        
        [Required]
        public int MachineId { get; set; }

        [Required]
        [VerifyMachineName]
        public string MachineName {get; set;}
        
        public string File { get; set; }
        public bool Status { get; set; }
        public DateTime Created { get; set; }
        public List<IFormFile> Files {get; set;}
    }
}