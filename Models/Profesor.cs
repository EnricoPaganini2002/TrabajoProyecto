using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrabajoProyecto.Models
{
    public class Profesor
    {

        [Key]

        public int ProfesorID { get; set; }
        
        [Required]
        [StringLength(50, ErrorMessage = "El nombre debe tener como máximo 10 caracteres")]
        public string? NombreCompletoProfesor { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "El nombre debe tener como máximo 10 caracteres")]
        public string? DNI { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "{0} es requerido")]
        public DateTime FechaNacimiento { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "El nombre debe tener como máximo 10 caracteres")]
        public string? Dirrecion { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "El campo {0} no tiene un formato válido de dirección de correo electrónico.")]
        [Display(Name = "Dirección de Correo")]
        [StringLength(20, ErrorMessage = "El nombre debe tener como máximo 20 caracteres")]
        public string? Correo { get; set; }

        public int EliminarProfesor { get; set; }

        public virtual ICollection<ProfesorAsignatura>? ProfesorAsignaturas { get; set; }
        public virtual ICollection<Tarea>? Tareas { get; set; }
    }



    public class VistaProfesor
    {
        public int ProfesorID { get; set; }

        public string? NombreCompletoProfesor { get; set; }

        public string? DNI { get; set; }

        public DateTime FechaNacimiento { get; set; }

        public string? Dirrecion { get; set; }

        public string? Correo { get; set; }

        public int EliminarProfesor { get; set; }
    }
}

