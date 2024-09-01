using dotnet_crud_api.Entities;
using dotnet_crud_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dotnet_crud_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoParentController : ControllerBase
    {
        private readonly TodoDb _context;

        public TodoParentController(TodoDb todoDb)
        {
            _context = todoDb;
            _context.Database.EnsureCreated();
        }

        [HttpGet]
        public IEnumerable<TodoParent> GetAllTodoParents()
        {
            return _context.TodoParents.AsNoTracking().ToArray();
        }
    }
}