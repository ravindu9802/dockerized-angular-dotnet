using dotnet_crud_api.Entities;
using dotnet_crud_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace dotnet_crud_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoDb _dbContext;
        private readonly IMemoryCache _cache;

        public TodoController(TodoDb dbContext, IMemoryCache memoryCache)
        {
            _dbContext = dbContext;
            _dbContext.Database.EnsureCreated();
            _cache = memoryCache;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Todo>> GetAllTodos()
        {
            return Ok(_dbContext.Todos.AsNoTracking().ToArray());
        }

        // [HttpGet("{id}")]
        // public ActionResult<Todo> GetTodoById(int id)
        // {
        //     var todo = _dbContext.Todos.AsNoTracking().SingleOrDefault(t => t.Id == id);
        //     if (todo == null) return NotFound();

        //     return Ok(todo);
        // }

        [HttpGet("{id}")]
        public Task<Todo?> GetTodoById(int id)
        {
            return _cache.GetOrCreateAsync(
                $"todo-{id}",
                entry =>
                {
                    entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(1));
                    return _dbContext.Todos.AsNoTracking().SingleAsync(t => t.Id == id);
                }
            );
        }

        [HttpPut("{id}")]
        public ActionResult<Todo> PutToggleStatus(int id, [FromBody] Todo todo)
        {
            var t = _dbContext.Todos.Find(id);
            if (t == null)
                return NotFound();

            t.Name = todo.Name;
            t.IsComplete = todo.IsComplete;
            _dbContext.SaveChanges();
            return Ok(todo);
        }

        [HttpPost]
        public ActionResult PostTodo(Todo todo)
        {
            _dbContext.Todos.Add(todo);
            _dbContext.SaveChanges();
            _cache.Remove($"todo-{todo.Id}");
            return Created($"/{todo.Id}", todo);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteTodo(int id)
        {
            var t = _dbContext.Todos.Find(id);
            if (t == null)
                return NotFound();

            _dbContext.Todos.Remove(t);
            _dbContext.SaveChanges();
            return Ok(t);
        }
    }
}
