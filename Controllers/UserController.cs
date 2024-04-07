using LC4_TO_DO.Data;
using LC4_TO_DO.Entities;
using LC4_TO_DO.Models;
using Microsoft.AspNetCore.Mvc;

namespace LC4_TO_DO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DataContext _context;

        public UserController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            var users =  _context.Users.ToList();

            return Ok(users);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetUserById( int id)
        {
            var user =  _context.Users.Find(id);
            if (user == null)
            {
                return NotFound($"Usuario ID {id} no encontrado");
            }
            return Ok(user);
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody]UserDto userDto)
        {
            if (userDto.name == "string" || userDto.email == "string" || userDto.password == "string") 
            {
                return BadRequest("Complete todos los campos");
            }
            try
            {
                var user = new User()
                {
                    name = userDto.name,
                    email = userDto.email,
                    password = userDto.password
                };

                _context.Users.Add(user);
                _context.SaveChanges();


                return Ok($"Usuario ID {user.id_user} creado con exito");
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message); 
            }
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateUser([FromRoute] int id, [FromBody]UserDto userDto)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                return NotFound($"Usuario ID {id} no encontrado");
            }
            else if(userDto.name == "string" || userDto.email == "string" || userDto.password == "string")
            {
                return BadRequest("Complete todos los campos");
            }

            user.name = userDto.name;
            user.email = userDto.email;
            user.password = userDto.password;

            _context.SaveChanges();
            return Ok($"Usuario ID {id} actualizado correctamente");
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteUser([FromRoute] int id)
        {
            var user =  _context.Users.Find(id);
            if (user == null)
            {
                return NotFound($"Usuario ID {id} no encontrado");
            }
            _context.Users.Remove(user);
            _context.SaveChanges();

            return Ok($"Usuario ID {id} borrado con existo");
        }

    }
}
