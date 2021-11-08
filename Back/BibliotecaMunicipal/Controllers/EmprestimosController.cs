using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BibliotecaMunicipal.Data;
using BibliotecaMunicipal.Models.Emprestimos;
using BibliotecaMunicipal.Models.Livros;
using BibliotecaMunicipal.Models.Pessoas;
using BibliotecaMunicipal.Models.RequestEmprestimo;

namespace BibliotecaMunicipal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmprestimosController : ControllerBase
    {
        private readonly BibliotecaMunicipalContext _context;

        public EmprestimosController(BibliotecaMunicipalContext context)
        {
            _context = context;
        }

        // GET: api/Emprestimos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Emprestimo>>> GetEmprestimo()
        {
            return await _context.Emprestimo.ToListAsync();

        }

        // GET: api/Emprestimos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Emprestimo>> GetEmprestimo(int id)
        {
            var emprestimo = await _context.Emprestimo.FindAsync(id);

            if (emprestimo == null)
            {
                return NotFound();
            }

            return emprestimo;
        }

        // PUT: api/Emprestimos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmprestimo(int id, Emprestimo emprestimo)
        {
            if (id != emprestimo.EmprestimoId)
            {
                return BadRequest();
            }

            _context.Entry(emprestimo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmprestimoExists(id))
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

        // POST: api/Emprestimos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Emprestimo>> PostEmprestimo(RequestEmprestimo request)
        {

            if (VerificarEmprestimo(EncontrarPessoa(request.requestCpf)) == false)
            {
                Emprestimo emprestimo = new Emprestimo();

                emprestimo.LivroId = EncontrarLivroDiminuir(request.livroName);

                if(EncontrarPessoa(request.requestCpf) == 0)
                {
                    return NoContent();
                }
                else
                {
                    emprestimo.PessoaId = EncontrarPessoa(request.requestCpf);
                }


                emprestimo.DataEmprestimo = DateTime.Now;


                _context.Emprestimo.Add(emprestimo);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetEmprestimo", new { id = emprestimo.EmprestimoId }, emprestimo);
            }
            else
            {
                return NoContent();
            }
           
        }

        // DELETE: api/Emprestimos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmprestimo(int id)
        {
            var emprestimo = await _context.Emprestimo.FindAsync(id);
            if (emprestimo == null)
            {
                return NotFound();
            }

            _context.Emprestimo.Remove(emprestimo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("/Devolver")]
        public async Task<IActionResult> DeleteEmprestimoPessoa(string cpf)
        {

            var emprestimo = await _context.Emprestimo.FindAsync(EncontrarEmprestimo(EncontrarPessoa(cpf)));
            if (emprestimo == null)
            {
                return NotFound();
            }

            _context.Emprestimo.Remove(emprestimo);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        private bool EmprestimoExists(int id)
        {
            return _context.Emprestimo.Any(e => e.EmprestimoId == id);
        }

        private int EncontrarLivroDiminuir(string name)
        {
            IQueryable<Livro> model = _context.Livro;
            int id=0;
            Livro l = new Livro();

            if (!string.IsNullOrEmpty(name))
            {
                model = model.Where(row => row.LivroName.Contains(name));
                
            }
            
            foreach (var item in model)
            {
                id =  item.LivroId;
                item.Quantidade--;

            }
            _context.SaveChanges();

            return id;
        }
        private void EncontrarLivroAumentar(int id)
        {
            IQueryable<Livro> model = _context.Livro;
            

            if (!string.IsNullOrEmpty(id.ToString()))
            {
                model = model.Where(row => row.LivroId == id);

            }

            foreach (var item in model)
            {
                item.Quantidade++;

            }
            _context.SaveChanges();

        }

        private int EncontrarPessoa(string cpf)
        {
            IQueryable<Pessoa> model = _context.Pessoa;
            int id = 0;

            if (!string.IsNullOrEmpty(cpf))
            {
                model = model.Where(row => row.Cpf == Int64.Parse(cpf));
            }

            foreach (var item in model)
            {
                id = item.PessoaId;
                
            }

            return id;
        }

        private int EncontrarEmprestimo(int id_pessoa)
        {
            IQueryable<Emprestimo> model = _context.Emprestimo;
            int id = 0;
            int id_livro = 0;

            if (!string.IsNullOrEmpty(id_pessoa.ToString()))
            {
                model = model.Where(row => row.PessoaId == id_pessoa);

            }

            foreach (var item in model)
            {
                id = item.EmprestimoId;
                id_livro = item.LivroId;
            }
            EncontrarLivroAumentar(id_livro);

            return id;
        }

        private bool VerificarEmprestimo(int id_pessoa)
        {

            IQueryable<Emprestimo> model = _context.Emprestimo;

            if (!string.IsNullOrEmpty(id_pessoa.ToString()))
            {
                model = model.Where(row => row.PessoaId == id_pessoa);

            }

            
           foreach (var item in model)
             {
                return EmprestimoExists(item.EmprestimoId);
             }

            return false;
        }

    }
}
