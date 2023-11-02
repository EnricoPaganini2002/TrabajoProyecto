using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TrabajoProyecto.Data;
using TrabajoProyecto.Models;

namespace TrabajoProyecto.Controllers
{
    [Authorize]
    public class ProfesoresController : Controller
    {

        private readonly ILogger<ProfesoresController> _logger;
        private readonly ApplicationDbContext _context;


        public ProfesoresController(ILogger<ProfesoresController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }


        public IActionResult Index()
        {
            var profesor = _context.Profesores
                .Where(c => c.EliminarProfesor == 0)
                .OrderBy(c => c.NombreCompletoProfesor)
                .ToList();

            ViewBag.ProfesorID = new SelectList(profesor, "ProfesorID", "NombreCompletoProfesor");

            // var asignaturasDisponibles = _context.Asignaturas
            //     .Where(a => a.Eliminar == 0 && a.ProfesorAsignaturas.Any(pa => pa.AsignaturaID == a.AsignaturaID))
            //     .OrderBy(a => a.NombreAsignatura)
            //     .ToList();
            var asignaturasDisponibles = _context.Asignaturas
    .Where(a => a.Eliminar == 0 &&
                !_context.ProfesorAsignaturas.Any(pa => pa.AsignaturaID == a.AsignaturaID))
    .OrderBy(a => a.NombreAsignatura)
    .ToList();


            ViewBag.AsignaturaID = new SelectList(asignaturasDisponibles, "AsignaturaID", "NombreAsignatura");

            return View();
        }


        public JsonResult BuscarProfesores(int ProfesorID = 0)
        {
            List<VistaProfesor> profesoresMostrar = new List<VistaProfesor>();

            var profesores = _context.Profesores.ToList();

            if (ProfesorID > 0)
            {
                profesores = profesores.Where(p => p.ProfesorID == ProfesorID).ToList();
            }

            // Ordena la lista de profesores por DNI
            profesores = profesores.OrderBy(p => p.DNI).ToList();

            foreach (var profesor in profesores)
            {
                var profesorMostrar = new VistaProfesor
                {
                    ProfesorID = profesor.ProfesorID,
                    NombreCompletoProfesor = profesor.NombreCompletoProfesor,
                    DNI = profesor.DNI,
                    FechaNacimiento = profesor.FechaNacimiento,
                    Dirrecion = profesor.Dirrecion,
                    Correo = profesor.Correo,
                    EliminarProfesor = profesor.EliminarProfesor
                };
                profesoresMostrar.Add(profesorMostrar);
            }

            return Json(profesoresMostrar);
        }


        public JsonResult GuardarProfesor(int ProfesorID, string NombreCompletoProfesor, string DNI, DateTime FechaNacimiento, string Dirrecion, string Correo)
        {
            bool resultado = false;

            if (!string.IsNullOrEmpty(NombreCompletoProfesor))
            {
                // Verificar si el DNI ya existe en la base de datos
                var profesorExistente = _context.Profesores.Count(p => p.DNI == DNI && p.NombreCompletoProfesor == NombreCompletoProfesor);

                if (ProfesorID == 0)
                {
                    // Si es un nuevo profesor, asegurarse de que el DNI no exista
                    if (profesorExistente == 0)
                    {
                        var profesorGuardar = new Profesor
                        {
                            NombreCompletoProfesor = NombreCompletoProfesor,
                            DNI = DNI,
                            FechaNacimiento = FechaNacimiento,
                            Dirrecion = Dirrecion,
                            Correo = Correo
                        };

                        _context.Add(profesorGuardar);
                        _context.SaveChanges();
                        resultado = true;
                    }
                }
                else
                {
                    var ProfesorOriginal = _context.Profesores
                       .Count(c => c.NombreCompletoProfesor == NombreCompletoProfesor && c.DNI == DNI && c.ProfesorID != ProfesorID);

                    // Si es una edición, verificar si el DNI existe en otro profesor
                    if (ProfesorOriginal == 0)
                    {
                        var profesorEditar = _context.Profesores.Find(ProfesorID);
                        if (profesorEditar != null)
                        {
                            profesorEditar.NombreCompletoProfesor = NombreCompletoProfesor;
                            profesorEditar.DNI = DNI;
                            profesorEditar.FechaNacimiento = FechaNacimiento;
                            profesorEditar.Dirrecion = Dirrecion;
                            profesorEditar.Correo = Correo;

                            _context.SaveChanges();
                            resultado = true;
                        }
                    }
                }
            }

            return Json(resultado);
        }







        public JsonResult DesahabilitarProfesor(int ProfesorID, int EliminarProfesor)
        {
            var profesor = _context.Profesores.Find(ProfesorID); // Buscar la carrera por su ID

            if (profesor != null) // Si se encontró la carrera
            {
                // Cambiar el valor de la propiedad "Eliminar" en función del valor de la variable "Eliminar"
                // Si Eliminar es igual a 1, se establece Eliminar en 1; de lo contrario, se establece en 0
                profesor.EliminarProfesor = EliminarProfesor == 1 ? 1 : 0;

                _context.SaveChanges(); // Guardar los cambios en la base de datos

                return Json(1); // Devolver el valor 1 para indicar éxito
            }

            // Si la carrera no se encontró, devolver el valor 0 para indicar que no se pudo realizar la acción
            return Json(0);
        }

        public JsonResult BuscarProfesoresAsignaturas(int ProfesorAsignaturaID = 0)
        {
            List<VistaProfesorAsignatura> profesoresAsignaturasMostrar = new List<VistaProfesorAsignatura>();

            var profesorAsignaturas = _context.ProfesorAsignaturas.Where(c => c.ProfesorID == ProfesorAsignaturaID).ToList();

            // Ordena la lista de profesores por DNI
            profesorAsignaturas = profesorAsignaturas.OrderBy(p => p.ProfesorID).ToList();

            foreach (var profesorAsignatura in profesorAsignaturas)
            {
                var nombreAsignatura = _context.Asignaturas.Where(a => a.AsignaturaID == profesorAsignatura.AsignaturaID).Select(a => a.NombreAsignatura).SingleOrDefault();
                var profesoreAsignaturasMostrar = new VistaProfesorAsignatura
                {
                    ProfesorAsignaturaID = profesorAsignatura.ProfesorAsignaturaID,

                    ProfesorID = profesorAsignatura.ProfesorID,

                    AsignaturaID = profesorAsignatura.AsignaturaID,

                    NombreAsignatura = nombreAsignatura,


                };
                profesoresAsignaturasMostrar.Add(profesoreAsignaturasMostrar);
            }

            return Json(profesoresAsignaturasMostrar);
        }



        public JsonResult GuardarProfesorAsignatura(int ProfesorAsignaturaID, int ProfesorID, int AsignaturaID)
        {
            bool resultado = false;

            if (ProfesorID != 0) // Cambiado de ProfesorID == null a ProfesorID != 0
            {
                // Verificar si el ProfesorAsignaturaID existe en la base de datos
                var profesorAsignaturaExistente = _context.ProfesorAsignaturas
                    .Count(p => p.ProfesorID == ProfesorID && p.ProfesorAsignaturaID == ProfesorAsignaturaID);

                if (ProfesorAsignaturaID == 0)
                {
                    // Si es un nuevo ProfesorAsignatura, asegurarse de que no exista
                    if (profesorAsignaturaExistente == 0)
                    {
                        var profesorAsignaturaGuardar = new ProfesorAsignatura
                        {
                            ProfesorID = ProfesorID,
                            AsignaturaID = AsignaturaID,
                        };

                        _context.Add(profesorAsignaturaGuardar);
                        _context.SaveChanges();
                        resultado = true;
                    }
                }
                else
                {
                    // Si es una edición, verificar si el ProfesorAsignaturaID existe en otro registro
                    var ProfesorAsignaturaOriginal = _context.ProfesorAsignaturas
                        .Count(c => c.ProfesorAsignaturaID != ProfesorAsignaturaID && c.ProfesorID == ProfesorID);

                    if (ProfesorAsignaturaOriginal == 0)
                    {
                        var profesorAsignaturaEditar = _context.ProfesorAsignaturas.Find(ProfesorAsignaturaID);
                        if (profesorAsignaturaEditar != null)
                        {
                            profesorAsignaturaEditar.AsignaturaID = AsignaturaID;
                            _context.SaveChanges();
                            resultado = true;
                        }
                    }
                }
            }

            return Json(resultado);
        }

        public JsonResult EliminarAsignaturaprofesor(int ProfesorAsignaturaID)
        {
            int resultado = 0;

            // Buscar la tarea en el contexto
            var profesorAsignatura = _context.ProfesorAsignaturas.Find(ProfesorAsignaturaID);

            if (profesorAsignatura != null)
            {
                // Marcar la tarea como eliminada (o realizar la acción de eliminación necesaria)
                _context.ProfesorAsignaturas.Remove(profesorAsignatura);
                _context.SaveChanges();
                resultado = 1; // Indicar éxito en la eliminación
            }

            return Json(resultado);
        }


    }
}



