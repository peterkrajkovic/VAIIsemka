using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class Test
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string TestId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Duration { get; set; }

        [Required]
        public string Category { get; set; }
        [Required]
        public string Result { get; set; }

        [Required]
        public string Parameters { get; set; }

        [Required]
        public string Log { get; set; }

        [Required]
        public DateTime Date { get; set; }
        public string AssertionMessage { get; set; }
        
    }
}
