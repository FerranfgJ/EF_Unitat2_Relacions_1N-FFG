using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp1.Models;

public class Animal
{
    [Required]
    public int AnimalID { get; set; }

    [Required]
    [MaxLength(50)]
    public string Nom { get; set; } = null!;

    [Required]
    public Especie Especie { get; set; }

    [Required]
    public bool EsFemella { get; set; }

    [Required]
    public DateTime DataNaixement { get; set; }

    [Required]
    public DateTime DataEntrada { get; set; }

    [Range(0.1, 100)]
    public double Pes { get; set; }

    public bool EstaAdoptat { get; set; }
    public DateTime? DataAdopcio { get; set; }

    [Precision(8, 2)]
    public decimal QuotaAdopcio { get; set; }

    public virtual Xip Xip { get; set; } = null!;
    
    public Adoptant? Adoptant { get; set; }

    public Animal()
    {
        DataEntrada = DateTime.Today;
        EstaAdoptat = false;
        QuotaAdopcio = 0m;
    }

    public Animal(int animalId, string nom, Especie especie, bool esFemella, DateTime dataNaixement, DateTime dataEntrada, double pes, bool estaAdoptat, decimal quotaAdopcio, Xip xip)
    {
        AnimalID = animalId;
        Nom = nom;
        Especie = especie;
        EsFemella = esFemella;
        DataNaixement = dataNaixement;
        DataEntrada = dataEntrada;
        Pes = pes;
        EstaAdoptat = estaAdoptat;
        QuotaAdopcio = quotaAdopcio;
        Xip = xip;
    }

    public override string ToString()
    {
        return $"Animal {{AnimalID = {AnimalID}, Nom = {Nom}, Especie = {Especie}}}";
    }
}
