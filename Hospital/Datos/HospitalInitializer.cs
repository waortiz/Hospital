using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace Hospital.Datos
{
    public class HospitalInitializer : DropCreateDatabaseIfModelChanges<HospitalContext>
    {
        protected override void Seed(HospitalContext context)
        {
            context.TiposDocumento.Add(new TipoDocumento() { Nombre = "Cédula de Ciudadanía", IdTipoDocumento = 1 });
            context.TiposDocumento.Add(new TipoDocumento() { Nombre = "Cédula de Extranjería", IdTipoDocumento = 2 });
            context.TiposDocumento.Add(new TipoDocumento() { Nombre = "Tarjeta de Identidad", IdTipoDocumento = 3 });
            context.Usuarios.Add(new Usuario() { Nombre = "william", Clave = "123" });
            base.Seed(context);
        }
    }
}
