using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BibliotecaMunicipal.Data;
using BibliotecaMunicipal.Models.Livros;

namespace BibliotecaMunicipal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivrosController : ControllerBase
    {
        private readonly BibliotecaMunicipalContext _context;

        public LivrosController(BibliotecaMunicipalContext context)
        {
            _context = context;
        }

        // GET: api/Livros
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Livro>>> GetLivro()
        {
            return await _context.Livro.ToListAsync();
        }

        // GET: api/Livros/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Livro>> GetLivro(int id)
        {
            var livro = await _context.Livro.FindAsync(id);

            if (livro == null)
            {
                return NotFound();
            }

            return livro;
        }

        // PUT: api/Livros/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLivro(int id, Livro livro)
        {
            if (id != livro.LivroId)
            {
                return BadRequest();
            }

            _context.Entry(livro).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LivroExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Livros
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Livro>> PostLivro(Livro livro)
        {
            //verificando se existe um livro com este nome ja
            if (EncontrarLivro(livro.LivroName) == 0)
            {
                return BadRequest();
            }
            
             _context.Livro.Add(livro);
             await _context.SaveChangesAsync();

             return CreatedAtAction("GetLivro", new { id = livro.LivroId }, livro);
            
        }

        // DELETE: api/Livros/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLivro(int id)
        {
            var livro = await _context.Livro.FindAsync(id);
            if (livro == null)
            {
                return NotFound();
            }

            _context.Livro.Remove(livro);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        
        private bool LivroExists(int id)
        {
            return _context.Livro.Any(e => e.LivroId == id);
        }

        private int EncontrarLivro(string name)
        {
            IQueryable<Livro> model = _context.Livro;
            int id = 0;
            Livro l = new Livro();

            if (!string.IsNullOrEmpty(name))
            {
                model = model.Where(row => row.LivroName.Contains(name));

            }

            foreach (var item in model)
            {
                id = item.LivroId;

            }
            _context.SaveChanges();

            return id;
        }
    }
}
