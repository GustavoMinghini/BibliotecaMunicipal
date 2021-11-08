using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BibliotecaMunicipal.Models.Pessoas;
using BibliotecaMunicipal.Models.Livros;
using BibliotecaMunicipal.Models.Emprestimos;
using BibliotecaMunicipal.Models.Reservas;

namespace BibliotecaMunicipal.Data
{
    public class BibliotecaMunicipalContext : DbContext
    {
        public BibliotecaMunicipalContext (DbContextOptions<BibliotecaMunicipalContext> options)
            : base(options)
        {
        }

        public DbSet<BibliotecaMunicipal.Models.Pessoas.Pessoa> Pessoa { get; set; }

        public DbSet<BibliotecaMunicipal.Models.Livros.Livro> Livro { get; set; }

        public DbSet<BibliotecaMunicipal.Models.Emprestimos.Emprestimo> Emprestimo { get; set; }

        public DbSet<BibliotecaMunicipal.Models.Reservas.Reserva> Reserva { get; set; }
    }
}
