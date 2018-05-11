using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hospital.Models
{
    public class Persona
    {
        public int Id { get; set; }
        public string PrimerNombre { get; set; }
        public string SegundoNombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public string NumeroDocumento { get; set; }
        public TipoDocumento TipoDocumento { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string Sexo { get; set; }
    }
}