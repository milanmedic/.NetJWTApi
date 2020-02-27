
using System.ComponentModel.DataAnnotations;
namespace JwtApi.Domain.Models
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Name {get; set;}
        [Required]
        [EmailAddress]
        [StringLength(255)]
        public string Email {get; set;}
        public string Role {get; set;}
        [Required]
        public string Password { get; set; }
    }
}