using Microsoft.AspNetCore.Mvc;
using RazmenaKnjiga.Models;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    [HttpGet("testuser")]
    public ActionResult<User> GetTestUser()
    {
        var testUser = new User
        {
            Username = "testuser",
            Email = "test@example.com",
            Password = "testpassword",
            Zanrovi = new List<string> { "Fantastika", "Drama" }
        };

        return Ok(testUser);
    }
}
