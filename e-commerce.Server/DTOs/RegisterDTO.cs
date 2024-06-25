
using System.ComponentModel.DataAnnotations;
namespace e_commerce.Server.DTOs

{
    public class RegisterDTO
    {

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 8)]
        public string Password { get; set; }
    }
}
