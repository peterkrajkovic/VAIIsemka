using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class Process
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("User")]
        public int Id_User { get; set; }
        [Required]
        public string Guid { get; set; }
        [Required]
        public DateTime Last_Update { get; set; }

    }
}
