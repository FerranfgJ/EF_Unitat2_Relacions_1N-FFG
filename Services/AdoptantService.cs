using ConsoleApp1.Data;
using ConsoleApp1.Models;
using Microsoft.EntityFrameworkCore;

namespace MyNamespace.Services;

public class AdoptantService
{
    private readonly RefugiCOntext _context;

    public AdoptantService(RefugiCOntext context)
    {
        _context = context;
    }

    public List<Adoptant> GetAdoptants()
    {
        return _context.Adoptants
            .AsNoTracking()
            .Include(adoptant => adoptant.Animals)
            .ToList();
    }

    public bool Adoptar(string dni, string codiXip)
    {
        var adoptant = _context.Adoptants.SingleOrDefault(adoptant => adoptant.DNI == dni);
        if (adoptant is null)
        {
            Console.WriteLine($"No existeix cap adoptant amb el DNI {dni}.");
            return false;
        }

        var animal = _context.Animals.SingleOrDefault(animal => animal.Xip.Codi == codiXip);
        if (animal is null)
        {
            Console.WriteLine($"No existeix cap animal amb el xip {codiXip}.");
            return false;
        }

        if (animal.EstaAdoptat)
        {
            Console.WriteLine($"{animal.Nom} ja està adoptat.");
            return false;
        }

        if (animal.Especie == Especie.GOS && !adoptant.TeJardi)
        {
            Console.WriteLine($"{adoptant.Nom} no té jardí i no pot adoptar un gos.");
            return false;
        }

        animal.Adoptant = adoptant;
        animal.EstaAdoptat = true;
        animal.DataAdopcio = DateTime.Today;
        _context.SaveChanges();
        return true;
    }
}
