using dotnet_crud_api.Entities;
using dotnet_crud_api.Models;

namespace dotnet_crud_api.Data
{

    public static class DbSeed
    {

        public static void Seed(TodoDb todoDb)
        {

            if (todoDb.Todos.Any())
            {
                return;
            }

            var todos = new Todo[]{
                new Todo {Name = "Cut Hair"},
                new Todo {Name = "Read the book"},
                new Todo {Name = "Book the flight to UK"},
                new Todo {Name = "Finish CRUD application"},
                new Todo {Name = "Learn Clean architecture"},
                new Todo {Name = "Dockerize the app"},
            };

            todoDb.Todos.AddRange(todos);
            todoDb.SaveChanges();
        }
    }
}