
using Dapper.Contrib.Extensions;

namespace MachineApp.Models
{
    [Table("machine")]
    public class Machine
    {
        [Key]
        public int id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Company { get; set; }
    }
}