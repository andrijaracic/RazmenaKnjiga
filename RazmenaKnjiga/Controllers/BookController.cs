using Microsoft.AspNetCore.Mvc;
using RazmenaKnjiga.Models;
using RazmenaKnjiga.Services;
using System.Collections.Generic;
using System.IO;
using System;
using System.Threading.Tasks;

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

        [HttpGet("{id}", Name = "GetBook")]
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

        [HttpGet("zanr/{zanr}")]
        public ActionResult<List<Knjiga>> GetByZanr(string zanr)
        {
            var knjige = _bookService.GetByZanr(zanr);

            if (knjige == null || knjige.Count == 0)
            {
                return NotFound($"Nema knjiga za žanr: {zanr}");
            }

            return knjige;
        }

        // Novi endpoint za upload slike i kreiranje knjige
        [HttpPost("upload")]
        public async Task<ActionResult<Knjiga>> UploadBookWithImage([FromForm] string naslov, [FromForm] string autor,
            [FromForm] string zanr, [FromForm] string opis, [FromForm] string stanje, [FromForm] string grad,
            [FromForm] string vlasnikId, [FromForm] Microsoft.AspNetCore.Http.IFormFile slika)
        {
            if (slika == null || slika.Length == 0)
                return BadRequest("Slika je obavezna.");

            // Putanja gde se slike čuvaju (wwwroot/uploads)
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            // Jedinstveno ime fajla (GUID + ekstenzija)
            var fileName = Guid.NewGuid() + Path.GetExtension(slika.FileName);
            var filePath = Path.Combine(uploadsFolder, fileName);

            // Čuvanje fajla na disk
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await slika.CopyToAsync(stream);
            }

            // Relativna putanja koja se čuva u bazi
            var relativePath = Path.Combine("uploads", fileName).Replace("\\", "/");

            var knjiga = new Knjiga
            {
                Naslov = naslov,
                Autor = autor,
                Zanr = zanr,
                Opis = opis,
                Stanje = stanje,
                Grad = grad,
                VlasnikId = vlasnikId,
                PutanjaSlike = relativePath,    
                DatumDodavanja = DateTime.UtcNow
            };

            _bookService.Create(knjiga);

            return CreatedAtRoute("GetBook", new { id = knjiga.Id }, knjiga);
        }
    }
}
