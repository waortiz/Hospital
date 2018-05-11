using Hospital.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hospital.Controllers
{
    [Authorize]
    public class PersonaController : Controller
    {
        private static Datos.HospitalContext contexto = new Datos.HospitalContext();

        public ActionResult Index()
        {
            List<Persona> personas = contexto.Personas.OrderBy(p => p.Id).Select(p => new Persona()
            {
                Id = p.Id,
                PrimerApellido = p.PrimerApellido,
                SegundoApellido = p.SegundoApellido,
                PrimerNombre = p.PrimerNombre,
                SegundoNombre = p.SegundoNombre,
                NumeroDocumento = p.NumeroDocumento,
                FechaNacimiento = p.FechaNacimiento,
                TipoDocumento = new TipoDocumento() { Nombre = p.TipoDocumento.Nombre }
            }).ToList();

            return View(personas);
        }

        public ActionResult Grafico()
        {
            var personas = contexto.Personas.GroupBy(p => p.FechaNacimiento.Year).Select(p => new { Year = p.Key, Cantidad = p.Count() });
            var datosGrafico = "[";
            if (personas.Count() > 0)
            {
                foreach (var valor in personas)
                {
                    datosGrafico += "['" + valor.Year + "', " + valor.Cantidad + "],";
                }
                datosGrafico = datosGrafico.Substring(0, datosGrafico.Length - 1);
                datosGrafico += "]";
            }
            else
            {
                datosGrafico = "[]";
            }

            ViewBag.DatosGrafico = datosGrafico;
            return View();
        }

        public ActionResult Crear()
        {
            Persona persona = new Persona();

            EstablecerTiposDocumento();

            return View(persona);
        }
               
        [HttpPost]
        public ActionResult Crear(Persona persona)
        {
            if (persona.Id == 0)
            {
                Datos.Persona personaNuevo = new Datos.Persona();
                personaNuevo.FechaNacimiento = persona.FechaNacimiento.Value;
                personaNuevo.IdTipoDocumento = persona.TipoDocumento.Id;
                personaNuevo.NumeroDocumento = persona.NumeroDocumento;
                personaNuevo.PrimerApellido = persona.PrimerApellido;
                personaNuevo.PrimerNombre = persona.PrimerNombre;
                personaNuevo.SegundoApellido = persona.SegundoApellido;
                personaNuevo.SegundoNombre = persona.SegundoNombre;
                personaNuevo.Sexo = persona.Sexo;
                contexto.Personas.Add(personaNuevo);
            }
            else
            {
                Datos.Persona personaActual = contexto.Personas.FirstOrDefault(p => p.Id == persona.Id);
                if (personaActual != null)
                {
                    personaActual.FechaNacimiento = persona.FechaNacimiento.Value;
                    personaActual.IdTipoDocumento = persona.TipoDocumento.Id;
                    personaActual.NumeroDocumento = persona.NumeroDocumento;
                    personaActual.PrimerApellido = persona.PrimerApellido;
                    personaActual.PrimerNombre = persona.PrimerNombre;
                    personaActual.SegundoApellido = persona.SegundoApellido;
                    personaActual.SegundoNombre = persona.SegundoNombre;
                    personaActual.Sexo = persona.Sexo;
                }
            }
            contexto.SaveChanges();

            EstablecerTiposDocumento();

            return View();
        }

        public ActionResult Editar(int id)
        {
            EstablecerTiposDocumento();

            var personaActual = contexto.Personas.FirstOrDefault(p => p.Id == id);
            Persona persona = new Persona();
            if (personaActual != null)
            {
                persona.Id = personaActual.Id;
                persona.FechaNacimiento = personaActual.FechaNacimiento;
                persona.TipoDocumento = new TipoDocumento() { Id = personaActual.IdTipoDocumento };
                persona.NumeroDocumento = personaActual.NumeroDocumento;
                persona.PrimerApellido = personaActual.PrimerApellido;
                persona.PrimerNombre = personaActual.PrimerNombre;
                persona.SegundoApellido = personaActual.SegundoApellido;
                persona.SegundoNombre = personaActual.SegundoNombre;
                persona.Sexo = personaActual.Sexo;
            }

            return View("Crear", persona);
        }

        public ActionResult Eliminar(int id)
        {
            EstablecerTiposDocumento();

            var personaActual = contexto.Personas.FirstOrDefault(p => p.Id == id);
            contexto.Personas.Remove(personaActual);
            contexto.SaveChanges();

            return RedirectToAction("Index");
        }

        private void EstablecerTiposDocumento()
        {
            var tiposDocumento = contexto.TiposDocumento.OrderBy(t => t.Nombre).Select(t => new TipoDocumento()
            {
                Id = t.IdTipoDocumento,
                Nombre = t.Nombre
            }).ToList();

            ViewBag.TiposDocumento = tiposDocumento.Select(x => new SelectListItem
            {
                Text = x.Nombre,
                Value = x.Id.ToString()
            });
        }
    }
}