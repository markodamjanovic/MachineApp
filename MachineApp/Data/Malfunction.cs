using System;
using Dapper.Contrib.Extensions;

namespace MachineApp.Models
{
    [Table("malfunctions")]
    public class Malfunction
    {
        [Key]
        public int id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int MachineId { get; set; }
        public bool Status { get; set; }
        public DateTime Created { get; set; }
    }
}