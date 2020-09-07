
using System.ComponentModel.DataAnnotations;

namespace MachineApp.Models
{
    [Dapper.Contrib.Extensions.Table("machine")]
    public class Machine
    {
        [Dapper.Contrib.Extensions.Key]
        public int id { get; set; }
        
        [Required]
        [DataType(DataType.Text)]
        public string Name { get; set; }
        
        [DataType(DataType.Text)]
        public string Description { get; set; }
        
        [Required]
        [DataType(DataType.Text)]
        public string Company { get; set; }
    }
}