using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LC4_TO_DO.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_user { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }

        public ICollection<TodoItem> TodoItems { get; set; } = new List<TodoItem>();
    }
}
