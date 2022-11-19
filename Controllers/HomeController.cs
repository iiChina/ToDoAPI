using Microsoft.AspNetCore.Mvc;
using ToDo.Data;
using ToDo.Models;

namespace ToDo.Controllers
{
    [ApiController]
    public class HomeController : ControllerBase 
    {       
        // Todo método público dentro de uma controller é também conhecido por Action
        [HttpGet("/")]
        // [FromServices] -> Indica que a instância do AppDbContext será passada pelos serviços do ASP.NET -> Também conhecido como injeção de depedência.
        public IActionResult Get([FromServices] AppDbContext context) => Ok(context.Todos.ToList());

        [HttpGet("/{id:int}")]
        public IActionResult GetById([FromRoute] int id, [FromServices] AppDbContext context) 
        {
            var toDo = context.Todos.FirstOrDefault(x => x.Id == id);

            if(toDo == null)
                return NotFound();

            return Ok(toDo);
        }

        [HttpPost("/")]
        public IActionResult Post(
            [FromBody] TodoModel toDo,
            [FromServices] AppDbContext context)
        {
            context.Todos.Add(toDo);
            context.SaveChanges();

            return Created($"/{toDo.Id}", toDo);
        }

        [HttpPut("/{id:int}")]
        public IActionResult Put(
            [FromRoute] int id,
            [FromBody] TodoModel toDo,
            [FromServices] AppDbContext context)
        {
            var model = context.Todos.FirstOrDefault(x => x.Id == id);
            if(model == null)
                return NotFound();

            model.Title = toDo.Title;
            model.Done = toDo.Done;

            context.Todos.Update(model);
            context.SaveChanges();
            return Ok(model);
        }

        [HttpDelete("/{id:int}")]
        public IActionResult Delete(
            [FromRoute] int id,
            [FromServices] AppDbContext context)
        {
            var model = context.Todos.FirstOrDefault(x => x.Id == id);
            if(model == null)
                return NotFound();

            context.Remove(model);
            context.SaveChanges();
            return Ok(model);
        }
    }
}