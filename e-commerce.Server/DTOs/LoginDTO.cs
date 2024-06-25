using System.ComponentModel.DataAnnotations;
namespace e_commerce.Server.DTOs
{
    public class LoginDTO
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }


    }
}
