using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TrabajoProyecto.Models;

namespace TrabajoProyecto.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Carrera> Carreras { get; set; }

    public DbSet<Alumno> Alumnos { get; set; }

    public DbSet<Profesor> Profesores { get; set; }

    public DbSet<Asignatura> Asignaturas { get; set; }

    public DbSet<ProfesorAsignatura> ProfesorAsignaturas { get; set; }

    public DbSet<Tarea> Tareas { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Profesor>()
        .HasMany(p => p.ProfesorAsignaturas)
        .WithOne(pa => pa.Profesor)
        .HasForeignKey(pa => pa.ProfesorID)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ProfesorAsignatura>()
            .HasOne(pa => pa.Profesor) // Cambio a HasOne ya que ProfesorAsignatura tiene una relación con un solo Profesor
            .WithMany(p => p.ProfesorAsignaturas) // Aquí usamos WithMany para la relación Profesor -> ProfesorAsignatura
            .HasForeignKey(pa => pa.ProfesorID)
            .OnDelete(DeleteBehavior.Restrict);
        // Esto evita la eliminación en cascada

        // Esto evita la eliminación en cascada

        // Configuración de relaciones para el modelo Tarea
        modelBuilder.Entity<Tarea>()
            .HasOne(t => t.Asignatura)
            .WithMany(a => a.Tareas)
            .HasForeignKey(t => t.AsignaturaID);

        // modelBuilder.Entity<Tarea>()
        // .HasOne(t => t.Profesor)
        // .WithMany(a => a.Tareas)
        // .HasForeignKey(t => t.ProfesorID);

    }
}
