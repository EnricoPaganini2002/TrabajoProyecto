using System.Data.SqlTypes;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TrabajoProyecto.Data;
using TrabajoProyecto.Models;

namespace TrabajoProyecto.Controllers;
[Authorize]

public class TareasController : Controller

{
    private readonly ILogger<TareasController> _logger;
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager; // Reemplaza ApplicationUser con el nombre de tu clase de usuario


    public TareasController(ILogger<TareasController> logger, ApplicationDbContext context, UserManager<IdentityUser> userManager)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
    }


    public IActionResult Index()
    {

        var asignatura = _context.Asignaturas.Where(c => c.Eliminar == 0).OrderBy(c => c.NombreAsignatura).ToList();
        ViewBag.AsignaturaID = new SelectList(asignatura, "AsignaturaID", "NombreAsignatura");

        return View();
    }



    public JsonResult BuscarTareas(int TareaID = 0)
    {
        var usarioActual = _userManager.GetUserId(HttpContext.User);
        List<VistaTarea> tareasMostrar = new List<VistaTarea>();

        var tareas = _context.Tareas.Include(s => s.Asignatura).ToList();

        if (TareaID > 0)
        {
            tareas = tareas.Where(a => a.TareaID == TareaID).OrderBy(a => a.Descripcion).ThenBy(a => a.Asignatura?.NombreAsignatura).ToList();
        }

        foreach (var tarea in tareas)
        {
            var tareaMostrar = new VistaTarea
            {
                TareaID = tarea.TareaID,
                Titulo = tarea.Titulo,
                Descripcion = tarea.Descripcion,
                FechaCarga = tarea.FechaCarga,
                FechaVencimiento = tarea.FechaVencimiento,
                UsuarioID = usarioActual,
                AsignaturaID = tarea.Asignatura.AsignaturaID,
                NombreAsignatura = tarea.Asignatura.NombreAsignatura,
                Realizada = tarea.Realizada
                // ProfesorID = tarea.Profesor.ProfesorID,
                // NombreCompletoProfesor = tarea.Profesor.NombreCompletoProfesor
            };
            tareasMostrar.Add(tareaMostrar);

        }


        return Json(tareasMostrar);
    }





    public JsonResult GuardarTarea(int TareaID, string Titulo, string Descripcion, DateTime FechaCarga, DateTime FechaVencimiento, int AsignaturaID, int Realizada)
    {

        var usarioActual = _userManager.GetUserId(HttpContext.User);
        bool resultado = false;
        if (!string.IsNullOrEmpty(Descripcion))
        {
            // Obtener el usuario autenticado
            // var user = await _userManager.GetUserAsync(User);

            if (usarioActual != null)
            {
                if (TareaID == 0)
                {
                    var tareaOriginal = _context.Tareas.Where(t => t.Descripcion == Descripcion && t.UsuarioID == usarioActual && t.TareaID == TareaID).Count();
                    if (tareaOriginal == 0)
                    {
                        var tareaGuardar = new Tarea
                        {
                            Titulo = Titulo,
                            Descripcion = Descripcion,
                            FechaCarga = FechaCarga,
                            FechaVencimiento = FechaVencimiento,
                            UsuarioID = usarioActual,
                            AsignaturaID = AsignaturaID,
                            Realizada = Realizada
                        };
                        _context.Add(tareaGuardar);
                        _context.SaveChanges();
                        resultado = true;
                    }
                }
                else
                {
                    var tareaOriginal = _context.Tareas.Where(t => t.Descripcion == Descripcion && t.UsuarioID == usarioActual && t.TareaID != TareaID).Count();
                    if (tareaOriginal == 0)
                    {
                        var tareaEditar = _context.Tareas.Find(TareaID);
                        if (tareaEditar != null)
                        {
                            tareaEditar.Titulo = Titulo;
                            tareaEditar.Descripcion = Descripcion;
                            tareaEditar.FechaCarga = FechaCarga;
                            tareaEditar.FechaVencimiento = FechaVencimiento;
                            tareaEditar.AsignaturaID = AsignaturaID;
                            tareaEditar.Realizada = Realizada;
                            _context.SaveChanges();
                            resultado = true;
                        }
                    }
                }
            }
        }

        return Json(resultado);
    }

    // public JsonResult DesahabilitarAlumno(int TareaID, int Eliminar)
    // {
    //     var tarea = _context.Tareas.Find(TareaID); // Buscar la carrera por su ID

    //     if (tarea != null) // Si se encontró la carrera
    //     {
    //         // Cambiar el valor de la propiedad "Eliminar" en función del valor de la variable "Eliminar"
    //         // Si Eliminar es igual a 1, se establece Eliminar en 1; de lo contrario, se establece en 0
    //         tarea.Eliminar = Eliminar == true ? true : false;

    //         _context.SaveChanges(); // Guardar los cambios en la base de datos

    //         return Json(1); // Devolver el valor 1 para indicar éxito
    //     }

    //     // Si la carrera no se encontró, devolver el valor 0 para indicar que no se pudo realizar la acción
    //     return Json(0);
    // }

    public JsonResult DeshabilitarTarea(int TareaID, int Realizada)
    {
        var tarea = _context.Tareas.Find(TareaID); // Buscar la tarea por su ID

        if (tarea != null) // Si se encontró la tarea
        {
            // Cambiar el valor de la propiedad "Eliminar" en función del valor de la variable "Eliminar"
            // Si Eliminar es igual a 1, se establece Eliminar en true; de lo contrario, se establece en false
            tarea.Realizada = Realizada == 1 ? 1 : 0;

            _context.SaveChanges(); // Guardar los cambios en la base de datos

            return Json(1); // Devolver el valor 1 para indicar éxito
        }

        // Si la tarea no se encontró, devolver el valor 0 para indicar que no se pudo realizar la acción
        return Json(0);
    }


    public JsonResult EliminarTarea(int TareaID)
    {
        int resultado = 0;

        // Buscar la tarea en el contexto
        var tarea = _context.Tareas.Find(TareaID);

        if (tarea != null)
        {
            // Marcar la tarea como eliminada (o realizar la acción de eliminación necesaria)
            // var tareaAsignatura = _context.Asignaturas
            //        .Where(a => a.AsignaturaID == tarea.AsignaturaID && a.Eliminar == 0).Count();

            // if (tareaAsignatura == 0)
            // {
                _context.Tareas.Remove(tarea);
                _context.SaveChanges();
                resultado = 1; // Indicar éxito en la eliminación
            // }
            // else 
            // {
            //     // Devolver un mensaje de error si hay asignaturas activas asociadas a esta carrera
            //     return Json("No se puede deshabilitar la tarea, hay asignaturas asociadas activas.");
            // }
        }
        return Json(resultado);
    }


}






