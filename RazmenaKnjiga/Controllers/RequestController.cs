using Microsoft.AspNetCore.Mvc;
using RazmenaKnjiga.Models;
using RazmenaKnjiga.Services; 
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RazmenaKnjiga.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RequestsController : ControllerBase
    {
        private readonly RequestService _requestService;

        public RequestsController(RequestService requestService)
        {
            _requestService = requestService;
        }

        
        [HttpPost]
        public async Task<IActionResult> SendRequest([FromBody] Zahtevi zahtev)
        {
            await _requestService.KreirajAsync(zahtev);
            return Ok();
        }

        [HttpGet("sender/{posiljalacId}")]
        public async Task<ActionResult<List<Zahtevi>>> GetRequestsBySender(string posiljalacId)
        {
            var zahtevi = await _requestService.VratiZahtevePosiljaocaAsync(posiljalacId);
            return Ok(zahtevi);
        }

        
        [HttpGet("book/{knjigaId}")]
        public async Task<ActionResult<List<Zahtevi>>> GetRequestsByBook(string knjigaId)
        {
            var zahtevi = await _requestService.VratiZahteveZaKnjiguAsync(knjigaId);
            return Ok(zahtevi);
        }

        
        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(string id, [FromBody] string noviStatus)
        {
            await _requestService.PromeniStatusAsync(id, noviStatus);
            return Ok();
        }
    }
}
