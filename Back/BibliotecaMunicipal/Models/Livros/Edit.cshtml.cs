using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BibliotecaMunicipal.Data;

namespace BibliotecaMunicipal.Models.Livros
{
    public class EditModel : PageModel
    {
        private readonly BibliotecaMunicipal.Data.BibliotecaMunicipalContext _context;

        public EditModel(BibliotecaMunicipal.Data.BibliotecaMunicipalContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Livro Livro { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Livro = await _context.Livro.FirstOrDefaultAsync(m => m.LivroId == id);

            if (Livro == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Livro).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LivroExists(Livro.LivroId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool LivroExists(int id)
        {
            return _context.Livro.Any(e => e.LivroId == id);
        }
    }
}
