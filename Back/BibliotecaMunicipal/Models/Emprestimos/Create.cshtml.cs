using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BibliotecaMunicipal.Data;

namespace BibliotecaMunicipal.Models.Emprestimos
{
    public class CreateModel : PageModel
    {
        private readonly BibliotecaMunicipal.Data.BibliotecaMunicipalContext _context;

        public CreateModel(BibliotecaMunicipal.Data.BibliotecaMunicipalContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Emprestimo Emprestimo { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Emprestimo.Add(Emprestimo);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
