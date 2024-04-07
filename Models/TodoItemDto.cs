using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LC4_TO_DO.Models
{
    public class TodoItemDto
    {
        [Required]
        public string title { get; set; }
        [Required]
        public string description { get; set; }
        [Required]
        [ForeignKey("UserId")]
        public int UserId { get; set; }
    }
}
