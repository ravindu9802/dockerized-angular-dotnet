using dotnet_crud_api.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_crud_api.Entities
{
    public class TodoDb : DbContext
    {
        public TodoDb(DbContextOptions<TodoDb> options) : base(options) { }
        public DbSet<Todo> Todos => Set<Todo>();
        public DbSet<TodoParent> TodoParents => Set<TodoParent>();
    }
}
