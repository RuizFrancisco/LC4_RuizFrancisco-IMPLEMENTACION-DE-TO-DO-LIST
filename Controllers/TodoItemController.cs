using LC4_TO_DO.Data;
using LC4_TO_DO.Entities;
using LC4_TO_DO.Models;
using Microsoft.AspNetCore.Mvc;

namespace LC4_TO_DO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemController : ControllerBase
    {
        private readonly DataContext _context;

        public TodoItemController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("GetAllTodoItems")]
        public IActionResult GetAllTodoItems()
        {
            var todoItems = _context.TodoItems
                .Select(item => new TodoItemGetDto
                {
                    id_todo_item = item.id_todo_item,
                    title = item.title,
                    description = item.description,
                    UserId = item.UserId,
                })
                .ToList();

            return Ok(todoItems);
        }

        [HttpGet("GetTodoItemById/{id_todo}")]
        public IActionResult GetTodoItemById(int id_todo)
        {
            var todoItem = _context.TodoItems.Find(id_todo);
            if (todoItem == null)
            {
                return NotFound($"TodoItem ID {id_todo} no encontrado");
            }
            var todoItemDto = new TodoItemGetDto
            {
                id_todo_item = todoItem.id_todo_item,
                title = todoItem.title,
                description = todoItem.description,
                UserId = todoItem.UserId
            };

            return Ok(todoItemDto);
        }

        [HttpGet("GetTodoItemsByUserId/{UserId}")]
        public IActionResult GetTodoItemsByUserId(int UserId) {

            var todoItems = _context.TodoItems
                .Where(item =>  item.UserId == UserId)
                .Select(item => new TodoItemGetDto
                {
                    id_todo_item = item.id_todo_item,
                    title = item.title,
                    description = item.description,
                    UserId = item.UserId,
                })
                .ToList();
            if (todoItems == null || todoItems.Count == 0)
            {
                return NotFound($"No se encontraron TodoItems relacionados al Usuario ID {UserId}");
            }
            return Ok(todoItems);
        }

        [HttpPost("CreateTodoItem")]
        public IActionResult CreateTodoItem([FromBody] TodoItemDto todoItemDto)
        {
            if (todoItemDto.title == "string" || todoItemDto.description == "string" || todoItemDto.UserId == 0)
            {
                return BadRequest("Complete todos los campos");
            }

            var user = _context.Users.Find(todoItemDto.UserId);
            if (user == null)
            {
                return NotFound($"Usuario ID {todoItemDto.UserId} no encontrado");
            }

            var todoItem = new TodoItem()
            {
                title = todoItemDto.title,
                description = todoItemDto.description,
                UserId = todoItemDto.UserId
            };

            _context.TodoItems.Add(todoItem);
            _context.SaveChanges();

            return Ok($"TodoItem ID {todoItem.id_todo_item} para el usuario ID {todoItem.UserId} asignado exitosamente");
        }

        [HttpPut("UpdateTodoItem/{id_todo}")]
        public IActionResult UpdateTodoItem([FromRoute] int id_todo, [FromBody] TodoItemDto todoItemDto)
        {
            var todoItem = _context.TodoItems.Find(id_todo);
            if (todoItem == null)
            {
                return NotFound($"TodoItem ID {id_todo} no encontrado");
            }
            else if (todoItemDto.title == "string" || todoItemDto.description == "string" || todoItemDto.UserId == 0)
            {
                return BadRequest("Complete todos los campos");
            }

            todoItem.title = todoItemDto.title;
            todoItem.description = todoItemDto.description;
            todoItemDto.UserId = todoItemDto.UserId;

            _context.SaveChanges();
            return Ok($"TodoItem ID {id_todo} actualizado correctamente");
        }

        [HttpDelete("DeleteTodoItem/{id_todo}")]
        public IActionResult DeleteTodoItem(int id_todo)
        {
            var todoItem = _context.TodoItems.Find(id_todo);
            if (todoItem == null)
            {
                return NotFound($"TodoItem ID {id_todo} no encontrado");
            }
            _context.TodoItems.Remove(todoItem);
            _context.SaveChanges();

            return Ok($"TodoItem ID {id_todo} borrado con existo");
        }
    }
}
