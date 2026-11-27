using ConsoleApp1.Data;
using ConsoleApp1.Models;

namespace MyNamespace.Services;

public class AnimalService
{
    private readonly RefugiCOntext _context;

    public AnimalService(RefugiCOntext context)
    {
        _context = context;
    }

    public List<Animal> GetDisponibles(Especie especie)
    {
        return _context.Animals
            .Where(animal => animal.Especie == especie && !animal.EstaAdoptat)
            .OrderBy(animal => animal.DataEntrada)
            .ToList();
    }
}
