using System.ComponentModel.DataAnnotations;

namespace CoralSeaTaskManagment.Ui.Models.DTO
{
    public class AuthRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
