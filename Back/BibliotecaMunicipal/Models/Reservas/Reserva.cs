using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaMunicipal.Models.Reservas
{
    public class Reserva
    {
        [Key]
        public int ReservaId { get; set; }

        [Required]
        public int LivroId { get; set; }

        [Required]
        public int PessoaId { get; set; }

        [Required]
        [Column(TypeName = "DateTime")]
        public DateTime DataChegada { get; set; }
    }
}
