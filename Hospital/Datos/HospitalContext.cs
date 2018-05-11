using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Hospital.Datos
{
    public class HospitalContext: DbContext
    {
        public HospitalContext()
            : base("name=Hospital") 
        {
            Database.SetInitializer(new HospitalInitializer());
        }

        public DbSet<Persona> Personas { get; set; }
        public DbSet<TipoDocumento> TiposDocumento { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TipoDocumento>()
                .HasMany(p => p.Personas)
                .WithRequired(p => p.TipoDocumento)
                .HasForeignKey(p => p.IdTipoDocumento)
                .WillCascadeOnDelete(false);
        }
    }
}