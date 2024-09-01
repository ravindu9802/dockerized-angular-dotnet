using System.ComponentModel.DataAnnotations;

namespace dotnet_crud_api.Models
{
    public class Auth
    {
        [EmailAddress]
        public string? Email { get; set; }
        
        public string? Password { get; set; }
    }
}

