using System.ComponentModel.DataAnnotations;

namespace dotnet_crud_api.Models
{
    public class Todo
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool IsComplete { get; set; }
    }
}
