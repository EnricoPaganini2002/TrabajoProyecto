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
    public class CarrerasController : Controller
    {

        private readonly ILogger<CarrerasController> _logger;
        private readonly ApplicationDbContext _context;
        public CarrerasController(ILogger<CarrerasController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }


        public IActionResult Index()
        {
            return View();
        }

        public JsonResult BuscarCarreras(int CarreraID = 0)
        {


            var carreras = _context.Carreras.OrderBy(c => c.NombreCarrera).ToList();

            if (CarreraID > 0)
            {
                carreras = carreras.Where(c => c.CarreraID == CarreraID).ToList();
            }

            return Json(carreras);
        }

        // public JsonResult GuardarCarrera(int CarreraID, string NombreCarrera, int Duracion)
        // {
        //     bool resultado = false;
        //     if (!string.IsNullOrEmpty(NombreCarrera))
        //     {
        //         if (CarreraID == 0)
        //         {
        //             var carreraOriginal = _context.Carreras.Where(c => c.NombreCarrera == NombreCarrera && c.Duracion == Duracion).FirstOrDefault();


        //             if (carreraOriginal == null)
        //             {
        //                 var carreraGuardar = new Carrera
        //                 {
        //                     NombreCarrera = NombreCarrera,
        //                     Duracion = Duracion
        //                 };
        //                 _context.Add(carreraGuardar);
        //                 _context.SaveChanges();
        //                 resultado = true;
        //             }
        //         }
        //         else
        //         {
        //             var carrerasOriginal = _context.Carreras.Where(c => c.NombreCarrera == NombreCarrera && c.Duracion == Duracion && c.CarreraID != CarreraID).FirstOrDefault();
        //             if (carrerasOriginal == null)
        //             {
        //                 var carreraEditar = _context.Carreras.Find(CarreraID);
        //                 if (carreraEditar != null)
        //                 {
        //                     carreraEditar.NombreCarrera = NombreCarrera;
        //                     carreraEditar.Duracion = Duracion;
        //                     _context.SaveChanges();
        //                     resultado = true;
        //                 }
        //             }
        //         }

        //     }


        //     return Json(resultado);
        // }

        public JsonResult GuardarCarrera(int CarreraID, string NombreCarrera, int Duracion)
        {
            bool resultado = false;

            if (!string.IsNullOrEmpty(NombreCarrera))
            {
                // Buscar si ya existe una carrera con el mismo nombre (excluyendo la actual si es una edición)
                var carreraExistente = _context.Carreras.FirstOrDefault(c => c.NombreCarrera == NombreCarrera && c.CarreraID != CarreraID);

                if (CarreraID == 0)
                {
                    // Crear una nueva carrera si no existe una con el mismo nombre
                    if (carreraExistente == null)
                    {
                        var carreraGuardar = new Carrera
                        {
                            NombreCarrera = NombreCarrera,
                            Duracion = Duracion
                        };
                        _context.Add(carreraGuardar);
                        _context.SaveChanges();
                        resultado = true;
                    }
                }
                else
                {
                    // Editar la carrera si no existe una con el mismo nombre
                    if (carreraExistente == null)
                    {
                        var carreraEditar = _context.Carreras.Find(CarreraID);
                        if (carreraEditar != null)
                        {
                            carreraEditar.NombreCarrera = NombreCarrera;
                            carreraEditar.Duracion = Duracion;
                            _context.SaveChanges();
                            resultado = true;
                        }
                    }
                }
            }

            return Json(resultado);
        }


        // public JsonResult DesahabilitarCarrera(int CarreraID, bool Eliminar)
        // {
        //     int resultado = 0;

        //     var carrera = _context.Carreras.Find(CarreraID);

        //     if (carrera != null)
        //     {
        //         if (Eliminar == false)
        //         {
        //             carrera.Eliminar = 1;
        //             _context.SaveChanges();
        //         }
        //         else
        //         {

        //             if (Eliminar == true)
        //             {

        //                 carrera.Eliminar = 0;
        //                 _context.SaveChanges();



        //             }

        //         }



        //     }

        //     resultado = 1;

        //     return Json(resultado);




        // }
        // public JsonResult DesahabilitarCarrera(int CarreraID, int Eliminar)
        // {
        //     var carrera = _context.Carreras.Find(CarreraID);

        //     if (carrera != null)
        //     {
        //         if (Eliminar == 1)
        //         {
        //             carrera.Eliminar = Eliminar == 0 ? 0 : 1; // Cambia el valor de "Eliminar" directamente en función del valor de la variable
        //             return Json(1); // Devuelve directamente el valor 1 sin usar una variable intermedia
        //         }
        //         else
        //         {
        //             carrera.Eliminar = Eliminar == 1 ? 1 : 0; // Cambia el valor de "Eliminar" directamente en función del valor de la variable
        //             return Json(1); // Devuelve directamente el valor 1 sin usar una variable intermedia
        //         }

        //     }
        //     _context.SaveChanges();

        //     return Json(0); // Devuelve directamente el valor 0 si la carrera no se encuentra
        // }

        // public JsonResult DesahabilitarCarrera(int CarreraID, int Eliminar)
        // {
        //     var carrera = _context.Carreras.Find(CarreraID); // Buscar la carrera por su ID

        //     if (carrera != null) // Si se encontró la carrera
        //     {
        //         // Cambiar el valor de la propiedad "Eliminar" en función del valor de la variable "Eliminar"
        //         // Si Eliminar es igual a 1, se establece Eliminar en 1; de lo contrario, se establece en 0
        //         carrera.Eliminar = Eliminar == 1 ? 1 : 0;

        //         _context.SaveChanges(); // Guardar los cambios en la base de datos

        //         return Json(1); // Devolver el valor 1 para indicar éxito
        //     }

        //     // Si la carrera no se encontró, devolver el valor 0 para indicar que no se pudo realizar la acción
        //     return Json(0);
        // }

        public JsonResult DesahabilitarCarrera(int CarreraID, int Eliminar)
        {
            var carrera = _context.Carreras.Find(CarreraID); // Buscar la carrera por su ID

            if (carrera != null) // Si se encontró la carrera
            {
                // Verificar si existen asignaturas asociadas a esta carrera que no estén eliminadas
               
                var asignaturasNoEliminadas = (from a in _context.Asignaturas where a.CarreraID == CarreraID && a.Eliminar == 0 select a).Count();
                // var alumnosNoEliminadas = (from al in _context.Alumnos where al.CarreraID == CarreraID && al.Eliminar == 0 select al).Count();

                if (asignaturasNoEliminadas == 0)
                {
                    // Cambiar el valor de la propiedad "Eliminar" en función del valor de la variable "Eliminar"
                    // Si Eliminar es igual a 1, se establece Eliminar en 1; de lo contrario, se establece en 0
                    carrera.Eliminar = Eliminar == 1 ? 1 : 0;

                    _context.SaveChanges(); // Guardar los cambios en la base de datos

                    return Json(1); // Devolver el valor 1 para indicar éxito
                }
                else
                {
                    // Devolver un mensaje de error si hay asignaturas activas asociadas a esta carrera
                    return Json("No se puede deshabilitar la carrera, hay asignaturas asociadas activas.");
                }
            }

            // Si la carrera no se encontró, devolver el valor 0 para indicar que no se pudo realizar la acción
            return Json(0);
        }











    }
}


