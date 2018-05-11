using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Hospital.Datos
{
    [Table("TiposDocumento")]
    public class TipoDocumento
    {
        public TipoDocumento()
        {
            this.Personas = new HashSet<Persona>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdTipoDocumento { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Persona> Personas { get; set; }
    }
}
