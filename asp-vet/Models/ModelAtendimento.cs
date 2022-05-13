using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace asp_vet.Models
{
    public class ModelAtendimento
    {
        public string codAtendimento { get; set; }
        public string dataAtendimento { get; set; }
        public string horaAtendimento { get; set; }
        public string codAnimal { get; set; }
        public string codVet { get; set; }
        public string Diag { get; set; }
        public string confAgendamento { get; set; }
    }
}