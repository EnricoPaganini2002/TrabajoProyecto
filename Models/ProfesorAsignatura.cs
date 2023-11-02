using System.ComponentModel.DataAnnotations;

namespace TrabajoProyecto.Models;

public class ProfesorAsignatura
{
    [Key]

    public int ProfesorAsignaturaID { get; set; }


    public int ProfesorID { get; set; }

    public int AsignaturaID { get; set; }



    public virtual Asignatura? Asignatura { get; set; }

    public virtual Profesor? Profesor { get; set; }
}

public class VistaProfesorAsignatura
{
    public int ProfesorAsignaturaID { get; set; }



    public int ProfesorID { get; set; }



    public int AsignaturaID { get; set; }

    public string? NombreAsignatura { get; set; }






}
