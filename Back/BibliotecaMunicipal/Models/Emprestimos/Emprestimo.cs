using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaMunicipal.Models.Emprestimos
{
    public class Emprestimo
    {
        [Key]
        public int EmprestimoId { get; set; }

        [Required]
        public int LivroId { get; set; }

        [Required]
        public int PessoaId { get; set; }

        [Required]
        [Column(TypeName = "DateTime")]
        public DateTime DataEmprestimo { get; set; }

        public bool Emprestado { get; set; } = false;

    }
}
