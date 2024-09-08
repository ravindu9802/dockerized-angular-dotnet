using dotnet_crud_api.Entities;
using dotnet_crud_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace dotnet_crud_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoDb _dbContext;

        // private readonly IMemoryCache _cache;
        private readonly IDistributedCache _distributedCache;

        public TodoController(
            TodoDb dbContext,
            // IMemoryCache memoryCache,
            IDistributedCache distributedCache
        )
        {
            _dbContext = dbContext;
            _dbContext.Database.EnsureCreated();
            // _cache = memoryCache;
            _distributedCache = distributedCache;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Todo>>> GetAllTodos()
        {
            string key = $"todo:all";
            var todos = await _distributedCache.GetStringAsync(key);

            if (string.IsNullOrEmpty(todos))
            {
                var result = await _dbContext.Todos.AsNoTracking().ToListAsync();

                var options = new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1),
                };
                await _distributedCache.SetStringAsync(
                    key,
                    JsonConvert.SerializeObject(result),
                    options
                );
                Console.WriteLine("no cache hit");
                return result;
            }
            Console.WriteLine("cache hit");
            return Ok(JsonConvert.SerializeObject(todos));
        }

        // [HttpGet("{id}")]
        // public ActionResult<Todo> GetTodoById(int id)
        // {
        //     var todo = _dbContext.Todos.AsNoTracking().SingleOrDefault(t => t.Id == id);
        //     if (todo == null) return NotFound();

        //     return Ok(todo);
        // }

        [HttpGet("{id}")]
        public async Task<Todo?> GetTodoById(int id)
        {
            // using memory cache
            // return _cache.GetOrCreateAsync(
            //     $"todo-{id}",
            //     entry =>
            //     {
            //         entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(1));
            //         return _dbContext.Todos.AsNoTracking().SingleAsync(t => t.Id == id);
            //     }
            // );

            // using distributed cache (redis)
            string key = $"todo:{id}";
            var todo = await _distributedCache.GetStringAsync(key);

            Todo result;
            if (string.IsNullOrEmpty(todo))
            {
                result = await _dbContext.Todos.AsNoTracking().SingleAsync(t => t.Id == id);

                var options = new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1),
                };
                await _distributedCache.SetStringAsync(
                    key,
                    JsonConvert.SerializeObject(result),
                    options
                );
                return result;
            }

            return JsonConvert.DeserializeObject<Todo>(todo);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Todo>> PutToggleStatus(int id, [FromBody] Todo todo)
        {
            var t = _dbContext.Todos.Find(id);
            if (t == null)
                return NotFound();

            t.Name = todo.Name;
            t.IsComplete = todo.IsComplete;
            _dbContext.SaveChanges();

            // update cache
            string key = $"todo:{todo.Id}";
            await _distributedCache.RemoveAsync(key);
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1),
            };
            await _distributedCache.SetStringAsync(key, JsonConvert.SerializeObject(todo), options);

            return Ok(todo);
        }

        [HttpPost]
        public ActionResult PostTodo(Todo todo)
        {
            _dbContext.Todos.Add(todo);
            _dbContext.SaveChanges();
            return Created($"/{todo.Id}", todo);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTodo(int id)
        {
            var t = _dbContext.Todos.Find(id);
            if (t == null)
                return NotFound();

            _dbContext.Todos.Remove(t);
            _dbContext.SaveChanges();
            string key = $"todo:{id}";
            await _distributedCache.RemoveAsync(key);
            return Ok(t);
        }
    }
}
