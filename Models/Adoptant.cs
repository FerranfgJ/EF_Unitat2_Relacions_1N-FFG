using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp1.Models;

[Index(nameof(DNI), IsUnique = true)]

public class Adoptant
{
    [Required]
    public int AdoptantID { get; set; }
    
    [Required]
    [MaxLength(9)]
    public string DNI { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string Nom { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Cognoms { get; set; }

    [Required]
    public bool TeJardi { get; set; }
    
    [Required]
    public DateTime DataRegistre { get; set; }
    
    [Required]
    public ICollection<Animal> Animals { get; set; }

    public Adoptant()
    {
        Animals = new HashSet<Animal>();
    }

    public Adoptant(int adoptantId, string dni, string nom, string cognoms, bool teJardi, DateTime dataRegistre, ICollection<Animal> animals)
    {
        AdoptantID = adoptantId;
        DNI = dni;
        Nom = nom;
        Cognoms = cognoms;
        TeJardi = teJardi;
        DataRegistre = dataRegistre;
        Animals = animals;
    }

    public override string ToString()
    {
        return $"Adoptant {{AdoptantID = {AdoptantID}, Nom = {Nom}, Cognoms = {Cognoms}, DNI = {DNI}}}";
    }
}
