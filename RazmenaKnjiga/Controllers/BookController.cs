using Microsoft.AspNetCore.Mvc;
using RazmenaKnjiga.Models;
using RazmenaKnjiga.Services;
using System.Collections.Generic;

namespace RazmenaKnjiga.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly BookService _bookService;

        public BookController(BookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public ActionResult<List<Knjiga>> Get() =>
            _bookService.Get();

        [HttpGet("{id:length(24)}", Name = "GetBook")]
        public ActionResult<Knjiga> Get(string id)
        {
            var knjiga = _bookService.Get(id);

            if (knjiga == null)
            {
                return NotFound();
            }

            return knjiga;
        }

        [HttpPost]
        public ActionResult<Knjiga> Create([FromBody] Knjiga knjiga)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _bookService.Create(knjiga);

            return CreatedAtRoute("GetBook", new { id = knjiga.Id.ToString() }, knjiga);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, [FromBody] Knjiga knjigaIn)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var knjiga = _bookService.Get(id);

            if (knjiga == null)
            {
                return NotFound();
            }

            _bookService.Update(id, knjigaIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var knjiga = _bookService.Get(id);

            if (knjiga == null)
            {
                return NotFound();
            }

            _bookService.Remove(id);

            return NoContent();
        }
    }
}
