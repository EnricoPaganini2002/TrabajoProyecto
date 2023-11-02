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
    public class AsignaturasController : Controller
    {

        private readonly ILogger<AsignaturasController> _logger;
        private readonly ApplicationDbContext _context;
        public AsignaturasController(ILogger<AsignaturasController> logger, ApplicationDbContext context)
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


        // public JsonResult BuscarAsignaturas(int AsignaturaID = 0)
        // {
        //     List<VistaAsignatura> asignaturasMostrar = new List<VistaAsignatura>();

        //     var asignaturas = _context.Asignaturas.Include(s => s.Carrera).ToList();

        //     if (AsignaturaID > 0)
        //     {
        //         asignaturas = asignaturas.Where(a => a.AsignaturaID == AsignaturaID).OrderBy(a => a.NombreAsignatura).ThenBy(a => a.Carrera?.NombreCarrera).ToList();
        //     }

        //     foreach (var asignatura in asignaturas)
        //     {
        //         var asignaturaMostrar = new VistaAsignatura
        //         {
        //             NombreAsignatura = asignatura.NombreAsignatura,
        //             CarreraID = asignatura.Carrera.CarreraID,
        //             NombreCarrera = asignatura.Carrera.NombreCarrera
        //         };
        //         asignaturasMostrar.Add(asignaturaMostrar);

        //     }


        //     return Json(asignaturasMostrar);
        // }

        public JsonResult BuscarAsignaturas(int AsignaturaID = 0)
        {
            List<VistaAsignatura> asignaturasMostrar = new List<VistaAsignatura>();

            var asignaturas = _context.Asignaturas.Include(s => s.Carrera).ToList();

            if (AsignaturaID > 0)
            {
                asignaturas = asignaturas.Where(a => a.AsignaturaID == AsignaturaID).OrderBy(a => a.NombreAsignatura).ThenBy(a => a.Carrera?.NombreCarrera).ToList();
            }

            foreach (var asignatura in asignaturas)
            {
                var asignaturaMostrar = new VistaAsignatura
                {
                    AsignaturaID = asignatura.AsignaturaID,
                    NombreAsignatura = asignatura.NombreAsignatura,
                    CarreraID = asignatura.Carrera.CarreraID,
                    NombreCarrera = asignatura.Carrera.NombreCarrera,
                    Eliminar = asignatura.Eliminar
                };
                asignaturasMostrar.Add(asignaturaMostrar);
            }

            return Json(asignaturasMostrar);
        }



        // public JsonResult GuardarAsignatura(int AsignaturaID, string NombreAsignatura, int CarreraID)
        // {
        //     bool resultado = false;

        //     if (!string.IsNullOrEmpty(NombreAsignatura))
        //     {
        //         // Buscar si ya existe una carrera con el mismo nombre (excluyendo la actual si es una edición)


        //         if (AsignaturaID == 0)
        //         {
        //             var asignaturaExistente = _context.Asignaturas.Count(c => c.NombreAsignatura == NombreAsignatura);
        //             // Crear una nueva carrera si no existe una con el mismo nombre
        //             if (asignaturaExistente == 0)
        //             {
        //                 var asignaturaGuardar = new Asignatura
        //                 {
        //                     NombreAsignatura = NombreAsignatura,
        //                     CarreraID = CarreraID
        //                 };
        //                 _context.Add(asignaturaGuardar);
        //                 _context.SaveChanges();
        //                 resultado = true;
        //             }
        //         }
        //         else
        //         {
        //             var AsignaturaOriginal = _context.Asignaturas
        //                .Count(c => c.NombreAsignatura == NombreAsignatura && c.AsignaturaID != AsignaturaID);
        //             // Editar la carrera si no existe una con el mismo nombre
        //             if (AsignaturaOriginal == 0)
        //             {
        //                 var asignaturaEditar = _context.Asignaturas.Find(AsignaturaID);
        //                 if (asignaturaEditar != null)
        //                 {
        //                     asignaturaEditar.NombreAsignatura = NombreAsignatura;
        //                     asignaturaEditar.CarreraID = CarreraID;
        //                     _context.SaveChanges();
        //                     resultado = true;
        //                 }
        //             }
        //         }
        //     }

        //     return Json(resultado);
        // }

        public JsonResult GuardarAsignatura(int AsignaturaID, string NombreAsignatura, int CarreraID)
        {
            bool resultado = false;

            if (!string.IsNullOrEmpty(NombreAsignatura))
            {
                if (AsignaturaID == 0)
                {
                    // Crear una nueva asignatura si no existe una con el mismo nombre
                    var asignaturaExistente = _context.Asignaturas.Count(c => c.NombreAsignatura == NombreAsignatura);
                    if (asignaturaExistente == 0)
                    {
                        var asignaturaGuardar = new Asignatura
                        {
                            NombreAsignatura = NombreAsignatura,
                            CarreraID = CarreraID
                        };
                        _context.Add(asignaturaGuardar);
                        _context.SaveChanges();
                        resultado = true;
                    }
                }
                else
                {
                    // Editar la asignatura si no existe una con el mismo nombre
                    var AsignaturaOriginal = _context.Asignaturas.Count(c => c.NombreAsignatura == NombreAsignatura && c.AsignaturaID != AsignaturaID);
                    if (AsignaturaOriginal == 0)
                    {
                        var asignaturaEditar = _context.Asignaturas.Find(AsignaturaID);
                        if (asignaturaEditar != null)
                        {
                            asignaturaEditar.NombreAsignatura = NombreAsignatura;
                            asignaturaEditar.CarreraID = CarreraID;
                            _context.SaveChanges();
                            resultado = true;
                        }
                    }
                }
            }

            return Json(resultado);
        }


        // 
        public JsonResult DesahabilitarAsignatura(int AsignaturaID, int Eliminar)
        {
            var asignatura = _context.Asignaturas.Find(AsignaturaID); // Buscar la carrera por su ID
            // var tarea = _context.Asignaturas.Find(AsignaturaID); // Buscar la carrera por su ID

            if (asignatura != null) // Si se encontró la carrera
            {
                // Cambiar el valor de la propiedad "Eliminar" en función del valor de la variable "Eliminar"
                // Si Eliminar es igual a 1, se establece Eliminar en 1; de lo contrario, se establece en 0
                var asignaturaTarea = (from a in _context.Tareas where a.AsignaturaID == AsignaturaID && a.Realizada == 0 select a).Count();
                if (asignaturaTarea == 0)
                {
                    asignatura.Eliminar = Eliminar == 1 ? 1 : 0;

                    _context.SaveChanges(); // Guardar los cambios en la base de datos

                    return Json(1); // Devolver el valor 1 para indicar éxito
                }
                else
                {
                    // Devolver un mensaje de error si hay asignaturas activas asociadas a esta carrera
                    return Json("No se puede deshabilitar la asignatura, hay tareas asociadas activas.");
                }
                
            }

            // Si la carrera no se encontró, devolver el valor 0 para indicar que no se pudo realizar la acción
            return Json(0);
        }










    }
}


