using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrabajoProyecto.Models
{

    public class Tarea
    {

        [Key]
        public int TareaID { get; set; }

        private const int MaxDescripcionLength = 100;

        [Display(Name = "Titulo")]
        [Required(ErrorMessage = "{0} es requerido")]
        [StringLength(MaxDescripcionLength, ErrorMessage = "La longitud máxima de {0} es {1}")]
        public string? Titulo { get; set; }


        [Display(Name = "Descripcion")]
        [Required(ErrorMessage = "{0} es requerido")]
        [StringLength(MaxDescripcionLength, ErrorMessage = "La longitud máxima de {0} es {1}")]
        public string? Descripcion { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "{0} es requerido")]
        public DateTime FechaCarga { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "{0} es requerido")]
        public DateTime FechaVencimiento { get; set; }

        [Required(ErrorMessage = "{0} es requerido")]
        public string? UsuarioID { get; set; }

        [Required(ErrorMessage = "{0} es requerido")]
        public int? AsignaturaID { get; set; }

        // [Required(ErrorMessage = "{0} es requerido")]
        // public int? ProfesorID { get; set; }


        [Required(ErrorMessage = "{0} es requerido")]
        public int Realizada { get; set; }

        public virtual Asignatura? Asignatura { get; set; }

        // public virtual Profesor? Profesor { get; set; }

    }

    public class VistaTarea
    {
        public int TareaID { get; set; }

        public string? Titulo { get; set; }

        public string? Descripcion { get; set; }

        public DateTime FechaCarga { get; set; }

        public DateTime FechaVencimiento { get; set; }

        public string? UsuarioID { get; set; }

        public int AsignaturaID { get; set; }

        public string? NombreAsignatura { get; set; }

        public int Realizada { get; set; }


        // public int ProfesorID { get; set; }

        // public string? NombreCompletoProfesor { get; set; }

    }
}