using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace dotnet_crud_api.Models
{
    public class TodoParent
    {
        public int Id { get; set; }

        public string ParentName { get; set; } = null!;

        [MaxLength(300)]
        public string? ParentDesc { get; set; }

        public DateOnly? Deadline { get; set; }

        [JsonIgnore]
        public ICollection<Todo> todos { get; set; } = new List<Todo>();
    }
}

