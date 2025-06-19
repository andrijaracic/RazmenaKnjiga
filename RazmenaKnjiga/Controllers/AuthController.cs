using Microsoft.AspNetCore.Mvc;
using RazmenaKnjiga.Models;
using RazmenaKnjiga.Services;

namespace RazmenaKnjiga.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;

        public AuthController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            var korisnik = await _userService.RegisterUserAsync(dto);
            return Ok(korisnik);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var user = _userService.Authenticate(request.Email, request.Password);

            if (user == null)
            {
                return Unauthorized("Pogrešan email ili lozinka.");
            }

            return Ok(new
            {
                Poruka = "Uspešno prijavljivanje",
                Korisnik = new
                {
                    user.Id,
                    user.Email,
                    user.Zanrovi
                }
            });
        }
    }

    

}
