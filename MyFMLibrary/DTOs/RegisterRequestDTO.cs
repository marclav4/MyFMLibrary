using System.ComponentModel.DataAnnotations;

namespace MyFMLibrary.DTOs
{
    public class RegisterRequestDTO
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
