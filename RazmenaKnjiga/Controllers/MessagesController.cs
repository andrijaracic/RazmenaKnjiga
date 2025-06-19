using Microsoft.AspNetCore.Mvc;
using RazmenaKnjiga.Models;
using RazmenaKnjiga.Services;  
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RazmenaKnjiga.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessagesController : ControllerBase
    {
        private readonly MessagesService _messagesService;

        public MessagesController(MessagesService messagesService)
        {
            _messagesService = messagesService;
        }

        [HttpGet("{korisnik1}/{korisnik2}")]
        public async Task<ActionResult<List<Poruke>>> GetMessages(string korisnik1, string korisnik2)
        {
            var messages = await _messagesService.VratiPrepiskeAsync(korisnik1, korisnik2);
            return Ok(messages);
        }

        
        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] Poruke poruka)
        {
            await _messagesService.PosaljiAsync(poruka);
            return Ok();
        }
    }
}
