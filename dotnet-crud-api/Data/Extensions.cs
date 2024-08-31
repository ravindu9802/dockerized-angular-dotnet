using dotnet_crud_api.Entities;

namespace dotnet_crud_api.Data;

public static class Extensions
{
    public static void CreateIfNotExist(this IHost host)
    {
        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<TodoDb>();
            context.Database.EnsureCreated();
            DbSeed.Seed(context);
        }
    }

}