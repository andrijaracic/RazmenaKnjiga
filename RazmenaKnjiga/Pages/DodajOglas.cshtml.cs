using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazmenaKnjiga.Models;
using RazmenaKnjiga.Services;
using System;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RazmenaKnjiga.Pages
{
    public class DodajOglasModel : PageModel
    {
        private readonly BookService _bookService;

        [BindProperty]
        public Knjiga NovaKnjiga { get; set; }

        [BindProperty]
        public Microsoft.AspNetCore.Http.IFormFile UploadSlika { get; set; }

        public string Poruka { get; set; }

        public DodajOglasModel(BookService bookService)
        {
            _bookService = bookService;
        }

        public void OnGet()
        {
            // Opcionalno
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Poruka = "Popunite sva obavezna polja.";
                return Page();
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                // Nije ulogovan, preusmeri na login
                return RedirectToPage("/Login");
            }

            NovaKnjiga.VlasnikId = userId;
            NovaKnjiga.DatumDodavanja = DateTime.UtcNow;

            if (UploadSlika != null)
            {
                using var ms = new MemoryStream();
                await UploadSlika.CopyToAsync(ms);
                var bajtovi = ms.ToArray();
                NovaKnjiga.SlikaBase64 = Convert.ToBase64String(bajtovi);
            }

            _bookService.Create(NovaKnjiga);

            return RedirectToPage("/MojiOglasi");
        }
    }
}
