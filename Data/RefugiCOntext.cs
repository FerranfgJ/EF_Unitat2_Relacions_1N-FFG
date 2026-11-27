using ConsoleApp1.Models;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp1.Data;

public class RefugiCOntext : DbContext
{
    public RefugiCOntext()
    {
    }

    public DbSet<Animal> Animals { get; set; }
    public DbSet<Xip> Xip { get; set; }
    public DbSet<Adoptant> Adoptants { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured) optionsBuilder.UseSqlite("Data Source=RefugiAnimals.db");
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Animal>()
            .HasOne(a => a.Xip)
            .WithOne(x => x.Animal)
            .HasForeignKey<Animal>("XipID")
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Animal>()
            .Property(e => e.Especie)
            .HasConversion<string>();
        
        modelBuilder.Entity<Animal>()
            .HasOne(a => a.Adoptant)
            .WithMany(x => x.Animals)
            .HasForeignKey("AdoptantID")
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
