using dotnet_crud_api.Models;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_crud_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        [HttpPost]
        public ActionResult PostIsValidLogin([FromBody] Auth auth)
        {
            string email = "abc@abc.com";
            string password = "12345";

            if (auth != null && auth.Email.Equals(email) && auth.Password.Equals(password)) return Ok();
            return BadRequest();
        }
    }
}
