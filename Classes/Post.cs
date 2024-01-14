using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("User")]
        public int Id_User { get; set; }
        public string? Caption { get; set; }
        public string? Location { get; set; }
        [Required]
        public byte[] Image { get; set; }
    }
}
