using Dapper.Contrib.Extensions;

namespace MachineApp.Models
{
    [Table("machine")]
    public class Machine
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Company { get; set; }
    }
}