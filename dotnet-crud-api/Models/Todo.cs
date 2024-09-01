using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace dotnet_crud_api.Models
{
    public class Todo
    {
        [Key]
        public int Id { get; set; }

        public string? Name { get; set; }

        public bool IsComplete { get; set; } = false;

        public int TodoParentId { get; set; }

        [JsonIgnore]
        public TodoParent? TodoParent { get; set; }
    }
}
