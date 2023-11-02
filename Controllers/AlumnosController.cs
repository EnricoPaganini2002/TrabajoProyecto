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
    public class AlumnosController : Controller
    {


        private readonly ILogger<AlumnosController> _logger;
        private readonly ApplicationDbContext _context;
        public AlumnosController(ILogger<AlumnosController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }



        public IActionResult Index()
        {
            var carrera = _context.Carreras.Where(c => c.Eliminar == 0).OrderBy(c => c.NombreCarrera).ToList();
            ViewBag.CarreraID = new SelectList(carrera, "CarreraID", "NombreCarrera");
            return View();
        }

        // public JsonResult BuscarAlumno(int AlumnoID = 0)
        // {


        //     var alumnos = _context.Alumnos.Include(s => s.Carrera).ToList();

        //     if (AlumnoID > 0)
        //     {
        //         alumnos = alumnos.Where(c => c.AlumnoID == AlumnoID).OrderBy(s => s.NombreCompleto).ToList();
        //     }

        //     return Json(alumnos);
        // }

        // public JsonResult BuscarAlumnos(int AlumnoID = 0)
        // {
        //     List<VistaAlumno> alumnosMostrar = new List<VistaAlumno>();


        //     var alumnos = _context.Alumnos.Include(s => s.Carrera).ToList();

        //     if (AlumnoID > 0)
        //     {
        //         alumnos = alumnos.Where(s => s.AlumnoID == AlumnoID).OrderBy(s => s.NombreCompleto).ToList();
        //     }
        //     foreach (var alumno in alumnos)
        //     {
        //         var alumnoMostrar = new VistaAlumno
        //         {
        //             NombreCompleto = alumno.NombreCompleto,
        //             Fecha = alumno.Fecha,
        //             AlumnoID = alumno.AlumnoID,
        //             CarreraID = alumno.CarreraID,
        //             nombreCarrera = alumno.Carrera.NombreCarrera,
        //             Eliminar = alumno.Eliminar

        //         };
        //         alumnosMostrar.Add(alumnoMostrar);
        //     }

        //     return Json(alumnosMostrar);
        // }
        public JsonResult BuscarAlumnos(int AlumnoID = 0)
        {
            List<VistaAlumno> alumnosMostrar = new List<VistaAlumno>();

            var alumnos = _context.Alumnos.Include(s => s.Carrera).ToList();

            if (AlumnoID > 0)
            {
                alumnos = alumnos.Where(a => a.AlumnoID == AlumnoID).OrderBy(a => a.NombreCompleto).ThenBy(a => a.Carrera?.NombreCarrera).ToList();
            }

            foreach (var alumno in alumnos)
            {
                var alumnoMostrar = new VistaAlumno
                {
                    NombreCompleto = alumno.NombreCompleto,
                    Fecha = alumno.Fecha,
                    DirrecionEstudiante = alumno.DirrecionEstudiante,
                    CorreoEstudiante = alumno.CorreoEstudiante,
                    DNIEstudiante = alumno.DNIEstudiante,
                    AlumnoID = alumno.AlumnoID,
                    CarreraID = alumno.CarreraID,
                    NombreCarrera = alumno.Carrera.NombreCarrera,
                    Eliminar = alumno.Eliminar
                };
                alumnosMostrar.Add(alumnoMostrar);

            }


            return Json(alumnosMostrar);
        }




        // public JsonResult GuardarAlumnos(int AlumnoID, string NombreCompleto, DateTime Fecha, int CarreraID)
        // {
        //     bool resultado = false;
        //     if (!string.IsNullOrEmpty(NombreCompleto))
        //     {
        //         if (AlumnoID == 0)
        //         {
        //             var alumnoOriginal = _context.Alumnos.Where(c => c.NombreCompleto == NombreCompleto && c.Fecha == Fecha && c.CarreraID == CarreraID && c.AlumnoID != AlumnoID).FirstOrDefault();


        //             if (alumnoOriginal == null)
        //             {
        //                 var alumnoGuardar = new Alumno
        //                 {
        //                     NombreCompleto = NombreCompleto,
        //                     Fecha = Fecha,
        //                     CarreraID = CarreraID
        //                 };
        //                 _context.Add(alumnoGuardar);
        //                 _context.SaveChanges();
        //                 resultado = true;
        //             }
        //         }

        //     }
        //     else
        //     {
        //         var alumnoOriginal = _context.Alumnos.Where(c => c.NombreCompleto == NombreCompleto && c.Fecha == Fecha && c.CarreraID == CarreraID && c.AlumnoID == AlumnoID).FirstOrDefault();
        //         if (alumnoOriginal == null)
        //         {
        //             var alumnoEditar = _context.Alumnos.Find(AlumnoID);
        //             if (alumnoEditar != null)
        //             {
        //                 alumnoEditar.NombreCompleto = NombreCompleto;
        //                 alumnoEditar.Fecha = Fecha;
        //                 alumnoEditar.CarreraID = CarreraID;
        //                 _context.SaveChanges();
        //                 resultado = true;
        //             }
        //         }
        //     }

        //     return Json(resultado);
        // }

        // public JsonResult GuardarAlumno(int AlumnoID, string NombreCompleto, DateTime Fecha, int CarreraID)
        // {
        //     bool resultado = false;

        //     if (!string.IsNullOrEmpty(NombreCompleto))
        //     {
        //         if (AlumnoID == 0)
        //         {
        //             var alumnoOriginal = _context.Alumnos
        //                 .Where(c => c.AlumnoID == AlumnoID && c.NombreCompleto == NombreCompleto && c.Fecha == Fecha && c.CarreraID == CarreraID)
        //                 .FirstOrDefault();

        //             if (alumnoOriginal == null)
        //             {
        //                 var alumnoGuardar = new Alumno
        //                 {
        //                     NombreCompleto = NombreCompleto,
        //                     Fecha = Fecha,
        //                     CarreraID = CarreraID,
        //                 };
        //                 _context.Add(alumnoGuardar);
        //                 _context.SaveChanges();
        //                 resultado = true;
        //             }
        //         }
        //         else
        //         {
        //             var alumnoOriginal = _context.Alumnos
        //                 .Where(c => c.NombreCompleto == NombreCompleto && c.Fecha == Fecha && c.CarreraID == CarreraID && c.AlumnoID != AlumnoID)
        //                 .FirstOrDefault();

        //             if (alumnoOriginal == null)
        //             {
        //                 var alumnoEditar = _context.Alumnos.Find(AlumnoID);
        //                 if (alumnoEditar != null)
        //                 {
        //                     alumnoEditar.NombreCompleto = NombreCompleto;
        //                     alumnoEditar.Fecha = Fecha;
        //                     alumnoEditar.CarreraID = CarreraID;
        //                     _context.SaveChanges();
        //                     resultado = true;
        //                 }
        //             }
        //         }
        //     }

        //     return Json(resultado);
        // }

        public JsonResult GuardarAlumno(int AlumnoID, string NombreCompleto, DateTime Fecha, int CarreraID, string DirrecionEstudiante, string DNIEstudiante, string CorreoEstudiante)
        {
            bool resultado = false;

            if (!string.IsNullOrEmpty(NombreCompleto))
            {
                if (AlumnoID == 0)
                {
                    // Verificar si ya existe un alumno con el mismo nombre, misma D.N.I
                    var alumnoOriginal = _context.Alumnos
                        .Count(c => c.NombreCompleto == NombreCompleto && c.DNIEstudiante == DNIEstudiante);

                    if (alumnoOriginal == 0)
                    {
                        var alumnoGuardar = new Alumno
                        {
                            NombreCompleto = NombreCompleto,
                            Fecha = Fecha,
                            CarreraID = CarreraID,
                            DirrecionEstudiante = DirrecionEstudiante,
                            DNIEstudiante = DNIEstudiante,
                            CorreoEstudiante = CorreoEstudiante
                        };
                        _context.Add(alumnoGuardar);
                        _context.SaveChanges();
                        resultado = true;
                    }
                }
                else
                {
                    // Verificar si ya existe otro alumno con el mismo nombre, misma D.N.I
                    var alumnoOriginal = _context.Alumnos
                        .Count(c => c.NombreCompleto == NombreCompleto && c.DNIEstudiante == DNIEstudiante && c.AlumnoID != AlumnoID);

                    if (alumnoOriginal == 0)
                    {
                        var alumnoEditar = _context.Alumnos.Find(AlumnoID);
                        if (alumnoEditar != null)
                        {
                            alumnoEditar.NombreCompleto = NombreCompleto;
                            alumnoEditar.Fecha = Fecha;
                            alumnoEditar.CarreraID = CarreraID;
                            alumnoEditar.DirrecionEstudiante = DirrecionEstudiante;
                            alumnoEditar.DNIEstudiante = DNIEstudiante;
                            alumnoEditar.CorreoEstudiante = CorreoEstudiante; 
                            _context.SaveChanges();
                            resultado = true;
                        }
                    }
                }
            }

            return Json(resultado);
        }



        // public JsonResult DesahabilitarAlumno(int AlumnoID, bool Eliminar)
        // {
        //     int resultado = 0;

        //     var carrera = _context.Alumnos.Find(AlumnoID);

        //     if (carrera != null)
        //     {
        //         if (Eliminar == false)
        //         {
        //             carrera.Eliminar = 0;
        //             _context.SaveChanges();
        //         }
        //         else
        //         {

        //             if (Eliminar == true)
        //             {

        //                 carrera.Eliminar = 1;
        //                 _context.SaveChanges();



        //             }

        //         }



        //     }

        //     resultado = 1;

        //     return Json(resultado);




        // }

        // public JsonResult DesahabilitarAlumno(int AlumnoID, int Eliminar)
        // {
        //     var alumno = _context.Alumnos.Find(AlumnoID); // Buscar la carrera por su ID

        //     if (alumno != null) // Si se encontró la carrera
        //     {
        //         // Cambiar el valor de la propiedad "Eliminar" en función del valor de la variable "Eliminar"
        //         // Si Eliminar es igual a 1, se establece Eliminar en 1; de lo contrario, se establece en 0
        //         var asignaturasNoEliminadas = (from a in _context.Carreras where a.CarreraID == _context.Alumnos.Where(al => al.CarreraID == a.CarreraID) && a.Eliminar == 0 select a).Count();

        //         if (asignaturasNoEliminadas == 0)
        //         {
        //             alumno.Eliminar = Eliminar == 1 ? 1 : 0;

        //         _context.SaveChanges(); // Guardar los cambios en la base de datos

        //         return Json(1); // Devolver el valor 1 para indicar éxito
        //       }
        //     }

        //     // Si la carrera no se encontró, devolver el valor 0 para indicar que no se pudo realizar la acción
        //     return Json(0);
        // }
        public JsonResult DesahabilitarAlumno(int AlumnoID, int Eliminar)
        {
            var alumno = _context.Alumnos.Find(AlumnoID); // Buscar al alumno por su ID

            if (alumno != null) // Si se encontró al alumno
            {
                // Cambiar el valor de la propiedad "Eliminar" en función del valor de la variable "Eliminar"
                // Si Eliminar es igual a 1, se establece Eliminar en 1; de lo contrario, se establece en 0
                var alumnosCarrera = _context.Carreras
                    .Where(a => a.CarreraID == alumno.CarreraID && a.Eliminar == 0).Count();

                if (alumnosCarrera == 0)
                {
                    alumno.Eliminar = Eliminar == 1 ? 1 : 0;

                    _context.SaveChanges(); // Guardar los cambios en la base de datos

                    return Json(1); // Devolver el valor 1 para indicar éxito
                }
                else 
                {
                    // Devolver un mensaje de error si hay asignaturas activas asociadas a esta carrera
                    return Json("No se puede deshabilitar la alkumno, hay carreras asociadas activas.");
                }
            }

            // Si el alumno no se encontró o hay asignaturas asociadas activas, devolver el valor 0 para indicar que no se pudo realizar la acción
            return Json(0);
        }

    }
}



