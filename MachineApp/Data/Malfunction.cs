using System;
using System.ComponentModel.DataAnnotations;


namespace MachineApp.Models
{
    [Dapper.Contrib.Extensions.Table("malfunctions")]
    public class Malfunction
    {
        [Dapper.Contrib.Extensions.Key]
        public int id { get; set; }
        
        [Required]
        [DataType(DataType.Text)]
        public string Name { get; set; }
        
        [DataType(DataType.Text)]
        public string Description { get; set; }
        
        [Required]
        public int MachineId { get; set; }

        public string File {get; set;}
    
        public bool Status { get; set; }
        
        public DateTime Created { get; set; }
    }
}