using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("User")]
        public string Username { get; set; }
        [ForeignKey("Post")]
        public int PostId { get; set; }
        [Required]
        public string Message { get; set; }
        [Required]
        public DateTime Created { get; set; }

    }
}
