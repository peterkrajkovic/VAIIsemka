using System.ComponentModel.DataAnnotations;

namespace Classes
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        public string? Bio { get; set; }
        public byte[]? Picture { get; set; }
        public DateTime? Date { get; set; }
    }
}