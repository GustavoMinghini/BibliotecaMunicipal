using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaMunicipal.Models.Pessoas
{
    public class Pessoa
    {
        [Key]
        public int PessoaId { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string PessoaName { get; set; }

        [Required]
        [Column(TypeName = "varchar(100)")]
        public string Endereco { get; set; }

        [Required]
        public int Cpf { get; set; }

        [Required]
        public Int64 Telefone { get; set; }

        [Required]
        public int Idade { get; set; }
    }
}
