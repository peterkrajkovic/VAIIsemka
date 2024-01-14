using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class Follow
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public int Id_Follower { get; set; }
        [ForeignKey("User")]
        public int Id_Following { get; set; }
    }
}
