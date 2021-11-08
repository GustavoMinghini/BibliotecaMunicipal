using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaMunicipal.Models.RequestEmprestimo
{
    public class RequestEmprestimo
    {
        [Key]
        public int RequestId { get; set; }

        public string requestCpf { get; set; }

        public string livroName { get; set; }
    }
}
