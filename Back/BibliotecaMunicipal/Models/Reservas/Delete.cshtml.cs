using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BibliotecaMunicipal.Data;

namespace BibliotecaMunicipal.Models.Reservas
{
    public class DeleteModel : PageModel
    {
        private readonly BibliotecaMunicipal.Data.BibliotecaMunicipalContext _context;

        public DeleteModel(BibliotecaMunicipal.Data.BibliotecaMunicipalContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Reserva Reserva { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Reserva = await _context.Reserva.FirstOrDefaultAsync(m => m.ReservaId == id);

            if (Reserva == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Reserva = await _context.Reserva.FindAsync(id);

            if (Reserva != null)
            {
                _context.Reserva.Remove(Reserva);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
