using System.ComponentModel.DataAnnotations;

namespace TrabajoProyecto.Models;

public class Alumno
{
    [Key]

    public int AlumnoID { get; set; }

    [Required(ErrorMessage = "{0} es requerido")]
    [StringLength(20, ErrorMessage = "El nombre completo debe tener como máximo 20 caracteres")]
    public string? NombreCompleto { get; set; }

    [Required]
    [StringLength(20, ErrorMessage = "El nombre debe tener como máximo 10 caracteres")]
    public string? DirrecionEstudiante { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
    [Required(ErrorMessage = "{0} es requerido")]
    public DateTime Fecha { get; set; }

    [Required]
    [StringLength(10, ErrorMessage = "El D.N.I debe tener como máximo 10 caracteres")]
    public string? DNIEstudiante { get; set; }

    [Required]
    [EmailAddress(ErrorMessage = "El campo {0} no tiene un formato válido de dirección de correo electrónico.")]
    [Display(Name = "Dirección de Correo")]
    [StringLength(20, ErrorMessage = "El nombre debe tener como máximo 20 caracteres")]
    public string? CorreoEstudiante { get; set; }

    public int Eliminar { get; set; }

    public int CarreraID { get; set; }

    public virtual Carrera? Carrera { get; set; }

}

public class VistaAlumno
{
    public int AlumnoID { get; set; }


    public string? NombreCompleto { get; set; }

    public string? DirrecionEstudiante { get; set; }

    public string? CorreoEstudiante { get; set; }
    public string? DNIEstudiante { get; set; }

    public DateTime Fecha { get; set; }

    public int Eliminar { get; set; }

    public int CarreraID { get; set; }

    public string? NombreCarrera { get; set; }
}