using dotnet_crud_api.Data;
using dotnet_crud_api.Entities;

var builder = WebApplication.CreateBuilder(args);

// setup CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy(
        name: "AllowAllOrigins",
            configurePolicy: policy =>
            {
                policy.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
            });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// in memory caching
builder.Services.AddMemoryCache();

// setup redis using Microsoft.Extensions.Caching.StackExchangeRedis
builder.Services.AddStackExchangeRedisCache(options =>{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
});

// in-memory db
// builder.Services.AddDbContext<TodoDb>(opt => opt.UseInMemoryDatabase("TodoList"));

// SQLite db
// builder.Services.AddSqlite<TodoDb>("Data Source=Todo.db");
// builder.Services.AddSqlite<PromotionsContext>("Data Source=Data/ReverseEngineering/Todo.db");

// SQL Server 2022
// connection string for docker
// var connStr = builder.Configuration.GetConnectionString("TodoDbDocker");
// var password = Environment.GetEnvironmentVariable("MSSQL_SA_PASSWORD");
// connStr = string.Format(connStr, password);
// connection string for local
var connStr = builder.Configuration.GetConnectionString("TodoDbLocal");
Console.WriteLine(connStr);
builder.Services.AddSqlServer<TodoDb>(connStr);

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddControllers();

var app = builder.Build();

// use below servicce to seed the db
app.CreateIfNotExist();

app.UseCors("AllowAllOrigins");

app.MapControllers();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.Run();
