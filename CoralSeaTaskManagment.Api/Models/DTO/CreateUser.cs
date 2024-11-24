using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CoralSeaTaskManagment.Api.Models.DTO
{
    public class CreateUser
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]

        public string Email { get; set; }
        [Required]

        public string Mobile { get; set; }
        [Required]

        public string Role { get; set; }
        [Required]

        public string Password  { get; set; }
        public int DepartmentId { get; set; }
        public int HotelId { get; set; }
        public byte[]? Pic { get; set; }
    }
}
