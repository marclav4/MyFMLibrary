using System.ComponentModel.DataAnnotations;

namespace MyFMLibrary.DTOs
{
    public class LoginResultDTO
    {
        public string UserName { get; set; }
        public string Token { get; set; }
        public int TokenMinutes { get; set; }
    }
}
