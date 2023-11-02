// using System.Diagnostics;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.AspNetCore.Mvc;
// using TrabajoProyecto.Data;
// using TrabajoProyecto.Models;

// namespace TrabajoProyecto.Controllers;

// public class HomeController : Controller
// {


//     private readonly ApplicationDbContext _context;
//     private readonly ILogger<HomeController> _logger;
//     private readonly UserManager<IdentityUser> _userManager;
//     private readonly RoleManager<IdentityRole> _rolManager;

//     public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> rolManager)
//     {
//         _logger = logger;
//         _context = context;
//         _userManager = userManager;
//         _rolManager = rolManager;
//     }

//     public IActionResult Index()
//     {
//         return View();
//     }

//     public async Task<JsonResult> InicializarPermisosUsuario()
//     {
//         //CREAR ROLES SI NO EXISTEN
//         var administrador = _context.Roles.Where(r => r.Name == "Administrador").SingleOrDefault();
//         if (administrador == null)
//         {
//             var roleResult = await _rolManager.CreateAsync(new IdentityRole("Administrador"));
//         }

//         // CREAR ROLES SI NO EXISTEN
//         var profesor = _context.Roles.Where(r => r.Name == "Profesor").SingleOrDefault();
//         if (administrador == null)
//         {
//             var roleResult = await _rolManager.CreateAsync(new IdentityRole("Profesor"));
//         }

//         // CREAR ROLES SI NO EXISTEN
//         var estudiante = _context.Roles.Where(r => r.Name == "Estudiante").SingleOrDefault();
//         if (administrador == null)
//         {
//             var roleResult = await _rolManager.CreateAsync(new IdentityRole("Estudiante"));
//         }

//         //CREAR USUARIO PRINCIPAL
//         bool creado = false;
//         //BUSCAR POR MEDIO DE CORREO ELECTRONICO SI EXISTE EL USUARIO
//         var usuario = _context.Users.Where(u => u.Email == "administrador@gmail.com").SingleOrDefault();
//         if (usuario == null)
//         {
//             var user = new IdentityUser { UserName = "administrador@gmail.com", Email = "administrador@gmail.com" };
//             var result = await _userManager.CreateAsync(user, "123456");

//             await _userManager.AddToRoleAsync(user, "Administrador");
//             creado = result.Succeeded;
//         }

//         //CODIGO PARA BUSCAR EL USUARIO EN CASO DE NECESITARLO
//         var superusuario = _context.Users.Where(r => r.Email == "administrador@gmail.com").SingleOrDefault();
//         if (superusuario != null)
//         {

//             //var personaSuperusuario = _contexto.Personas.Where(r => r.UsuarioID == superusuario.Id).Count();

//             var usuarioID = superusuario.Id;

//         }

//         return Json(creado);
//     }

//     public IActionResult Privacy()
//     {
//         return View();
//     }



//     [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
//     public IActionResult Error()
//     {
//         return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
//     }
// }
using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TrabajoProyecto.Data;
using TrabajoProyecto.Models;

namespace TrabajoProyecto.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _rolManager;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> rolManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _rolManager = rolManager;
        }

        public async Task<IActionResult> IndexAsync()
        {
            await InicializarPermisosUsuario();
            return View();
        }

        public async Task<JsonResult> InicializarPermisosUsuario()
        {
            string mensajeError;
            try
            {
                // CREAR ROLES SI NO EXISTEN
                await CrearRolSiNoExiste("Administrador");
                await CrearRolSiNoExiste("Profesor");
                await CrearRolSiNoExiste("Estudiante");

                // CREAR USUARIO PRINCIPAL
                var usuario = await _userManager.FindByEmailAsync("administrador@gmail.com");
                if (usuario == null)
                {
                    // Crear un usuario con nombre de usuario y correo electrónico
                    var user = new IdentityUser { UserName = "administrador@gmail.com", Email = "administrador@gmail.com" };

                    // Establecer una contraseña segura para el usuario
                    var result = await _userManager.CreateAsync(user, "123456");

                    if (result.Succeeded)
                    {
                        // Asignar el rol "Administrador" al usuario
                        await _userManager.AddToRoleAsync(user, "Administrador");
                    }
                }

                return Json(true);
            }
            catch (Exception ex)
            {
                mensajeError = "Ocurrió un error al inicializar los permisos: " + ex.Message;
            }
            return Json(new { success = false, error = mensajeError });
        }

        // Método para crear un rol si no existe
        private async Task CrearRolSiNoExiste(string nombreRol)
        {
            var rolExistente = await _rolManager.RoleExistsAsync(nombreRol);
            if (!rolExistente)
            {
                await _rolManager.CreateAsync(new IdentityRole(nombreRol));
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
