using System.ComponentModel.DataAnnotations;

namespace LC4_TO_DO.Models
{
    public class UserDto
    {
        [Required]
        public string name { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string password { get; set; }
    }
}
