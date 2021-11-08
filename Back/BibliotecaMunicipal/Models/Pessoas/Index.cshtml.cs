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
    public class IndexModel : PageModel
    {
        private readonly BibliotecaMunicipal.Data.BibliotecaMunicipalContext _context;

        public IndexModel(BibliotecaMunicipal.Data.BibliotecaMunicipalContext context)
        {
            _context = context;
        }

        public IList<Pessoa> Pessoa { get;set; }

        public async Task OnGetAsync()
        {
            Pessoa = await _context.Pessoa.ToListAsync();
        }
    }
}
