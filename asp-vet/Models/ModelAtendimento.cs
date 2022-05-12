using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace asp_vet.Models
{
    public class ModelAtendimento
    {
        public int codAtendimento { get; set; }
        public int dataAtendimento { get; set; }
        public int horaAtendimento { get; set; }
        public int codAnimal { get; set; }
        public int codVet { get; set; }
        public int Diag { get; set; }
    }
}