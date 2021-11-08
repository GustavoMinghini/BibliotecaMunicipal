using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BibliotecaMunicipal.Data;

namespace BibliotecaMunicipal.Models.Pessoas
{
    public class DetailsModel : PageModel
    {
        private readonly BibliotecaMunicipal.Data.BibliotecaMunicipalContext _context;

        public DetailsModel(BibliotecaMunicipal.Data.BibliotecaMunicipalContext context)
        {
            _context = context;
        }

        public Pessoa Pessoa { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Pessoa = await _context.Pessoa.FirstOrDefaultAsync(m => m.PessoaId == id);

            if (Pessoa == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
