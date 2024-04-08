using System.ComponentModel.DataAnnotations.Schema;

namespace LC4_TO_DO.Models
{
    public class TodoItemGetDto
    {
        public int id_todo_item { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public int UserId { get; set; }
    }
}
