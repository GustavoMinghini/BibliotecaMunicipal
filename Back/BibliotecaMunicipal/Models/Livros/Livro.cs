using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaMunicipal.Models.Livros
{
    public class Livro
    {
        [Key]
        public int LivroId { get; set; }

        [Required]
        [Column(TypeName = "varchar(100)")]
        public string LivroName { get; set; }

        [Required]
        [Column(TypeName = "varchar(100)")]
        public string Autor { get; set; }

        [Required]
        [Column(TypeName = "varchar(100)")]
        public string Genero { get; set; }

        [Required]
        [Column(TypeName = "varchar(100)")]
        public string Editora { get; set; }

        [Required]
        public int ISBN { get; set; }

        [Required]
        public int Quantidade { get; set; }
    }
}
