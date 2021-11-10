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
            //esse if esta verificando se existe um emprestimo ja feito por essa pessoa
            //o metodo encontrar pessoa ele recebe um cpf e devolve o id da pessoa que tem o cpf
            // e o metodo verificar retorna um bool, se ja existe ou nao um emprestimo feito
            if (VerificarEmprestimo(EncontrarPessoa(request.requestCpf)) == false)
            {
                Emprestimo emprestimo = new Emprestimo();

                //o metodo encontrar livro ele retorna o livro que tem o nome enviado
                //esse if ele ta verificando se existe algum livro com o nome informado caso nao
                // ele retorna um badrequest
                if(EncontrarLivro(request.livroName)== 0)
                {
                    return BadRequest();
                }
                else
                {
                    emprestimo.LivroId = EncontrarLivro(request.livroName);
                    //o metodo diminuir livro, ele diminui uma quantidade do livro retornado pelo encontrar
                    LivroDiminuir(emprestimo.LivroId);
                }

                // esse if ta verificando se exite uma pessoa com esse cpf, caso nao retorna um badrequest
                if(EncontrarPessoa(request.requestCpf) == 0)
                {
                    return BadRequest(new { mensagem = "Erro cpf nao existe" });
                }
                else
                {
                    emprestimo.PessoaId = EncontrarPessoa(request.requestCpf);
                }

                //aqui esta salvando na variavel a data de agora
                emprestimo.DataEmprestimo = DateTime.Now;


                _context.Emprestimo.Add(emprestimo);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetEmprestimo", new { id = emprestimo.EmprestimoId }, emprestimo);
            }
            else
            {
                return BadRequest();
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
            // EncontrarPessoa pega o cpf da pessoa em que o metodo recebeu e devolve o ID da pessoa,
            // o encotrar emprestimo pega o ID da pessoa e retorna o id do emprestimo e tambem aumenta em 1 a quantidade do livro
            
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

        private int EncontrarLivro(string name)
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

            }
            _context.SaveChanges();

            return id;
        }
        private void LivroAumentar(int id)
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

        private void LivroDiminuir(int id)
        {
            IQueryable<Livro> model = _context.Livro;


            if (!string.IsNullOrEmpty(id.ToString()))
            {
                model = model.Where(row => row.LivroId == id);

            }

            foreach (var item in model)
            {
                item.Quantidade--;

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
            //  livroaumentar ele aumenta a quantidade da tabela livro do id do livro enviando por parametro
            LivroAumentar(id_livro);

            return id;
        }

        private bool VerificarEmprestimo(int id_pessoa)
        {
            // nesse metodo estamos verificando se existe um emprestimo com o id da pessoa informado
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
