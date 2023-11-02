using System.ComponentModel.DataAnnotations;

namespace TrabajoProyecto.Models;

public class Carrera
{
    [Key]

    public int CarreraID { get; set; }

    [Display(Name = "Nombre Carrera")]
    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(20, ErrorMessage = "El nombre debe tener como máximo 20 caracteres")]
    public string NombreCarrera { get; set; }

    [Display(Name = "Duracion")]
    [Required(ErrorMessage = "La duracion es obligatoria")]
    [StringLength(2, ErrorMessage = "La duracion debe tener como máximo 2 caracteres")]
    public int Duracion { get; set; }

    public virtual ICollection<Alumno>? Alumnos { get; set; }

    public virtual ICollection<Asignatura>? Asignaturas { get; set; }

    public int Eliminar { get; set; }


}