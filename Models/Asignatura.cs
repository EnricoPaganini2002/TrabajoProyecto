using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrabajoProyecto.Models;

public class Asignatura 
{
    [Key]

    public int AsignaturaID { get; set; }

    [Required(ErrorMessage = "El nombre de la asignatura es obligatorio.")]
    [StringLength(100, ErrorMessage = "La longitud máxima del nombre es 100 caracteres.")]
    public string? NombreAsignatura { get; set; }

    public int CarreraID { get; set; }


    public virtual Carrera? Carrera { get; set; }

    public virtual ICollection<ProfesorAsignatura>? ProfesorAsignaturas { get; set; }
    public virtual ICollection<Tarea>? Tareas { get; set; }

    public int Eliminar { get; set; }


}

public class VistaAsignatura 
{
    public int AsignaturaID { get; set; }

    public string? NombreAsignatura { get; set; }

    public int CarreraID { get; set; }

    public string? NombreCarrera { get; set; }

    public int Eliminar { get; set; }

}
