using dotnet_crud_api.Entities;
using dotnet_crud_api.Models;

namespace dotnet_crud_api.Data
{

    public static class DbSeed
    {

        public static void Seed(TodoDb todoDb)
        {

            if (todoDb.TodoParents.Any())
            {
                return;
            }

            var todoParents = new TodoParent[]{
                new TodoParent{ ParentName= "Personal", ParentDesc="my personal todos go here", Deadline=null, },
                new TodoParent{ ParentName= "Work", ParentDesc="my work todos go here", Deadline= new DateOnly(2024,10,25), },
                new TodoParent{ ParentName= "Other", ParentDesc="my other todos go here", Deadline= new DateOnly(2024,12,31), },
            };

            todoDb.TodoParents.AddRange(todoParents);
            todoDb.SaveChanges();

            var personalParentId = todoDb.TodoParents.Where(p => p.ParentName == "Personal").Single().Id;
            var workParentId = todoDb.TodoParents.Where(p => p.ParentName == "Work").Single().Id;
            var otherParentId = todoDb.TodoParents.Where(p => p.ParentName == "Other").Single().Id;

            var todos = new Todo[]{
                new Todo {Name = "Cut Hair", TodoParentId=personalParentId},
                new Todo {Name = "Read the book", TodoParentId=personalParentId},
                new Todo {Name = "Book the flight to UK", TodoParentId=otherParentId},
                new Todo {Name = "Finish CRUD application", TodoParentId=workParentId},
                new Todo {Name = "Learn Clean architecture", TodoParentId=otherParentId},
                new Todo {Name = "Dockerize the app", TodoParentId=workParentId},
            };

            todoDb.Todos.AddRange(todos);
            todoDb.SaveChanges();
        }
    }
}