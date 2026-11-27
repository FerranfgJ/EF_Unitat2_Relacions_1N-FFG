using ConsoleApp1.Data;
using ConsoleApp1.Models;
using Microsoft.EntityFrameworkCore;
using MyNamespace.Services;


namespace MyNamespace
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var context = new RefugiCOntext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Animal[] animals =
                [
                    new Animal
                    {
                        Nom = "Churumbeh", Especie = Especie.GOS, EsFemella = true, Pes = 18.5,
                        DataNaixement = new DateTime(2021, 3, 14),
                        DataEntrada = new DateTime(2026, 2, 10),
                        Xip = new Xip { Codi = "724098100000001", DataImplantacio = new DateTime(2026, 2, 10) }
                    },
                    new Animal
                    {
                        Nom = "Falete", Especie = Especie.CONILL, EsFemella = false, Pes = 100,
                        DataNaixement = new DateTime(2020, 7, 8),
                        DataEntrada = new DateTime(2026, 1, 19),
                        Xip = new Xip { Codi = "724098100000002", DataImplantacio = new DateTime(2026, 1, 19) }
                    },
                    new Animal
                    {
                        Nom = "PacoPorras", Especie = Especie.AU, EsFemella = true, Pes = 12.8,
                        DataNaixement = new DateTime(2023, 9, 22),
                        DataEntrada = new DateTime(2026, 3, 4),
                        Xip = new Xip { Codi = "724098100000003", DataImplantacio = new DateTime(2026, 3, 4) }
                    }
                ];

                foreach (Animal a in animals)
                {
                    context.Animals.Add(a);
                }

                context.SaveChanges();

                List<Animal> llista = context.Animals.ToList();
                foreach (var animal in llista)
                {
                    Console.WriteLine(animal);
                }

                var animalService = new AnimalService(context);

                Console.WriteLine("\nExercici 1 · Gossos disponibles");
                foreach (var animal in animalService.GetDisponibles(Especie.GOS))
                {
                    Console.WriteLine(animal);
                }

                Console.WriteLine($"Gats disponibles: {animalService.GetDisponibles(Especie.GAT).Count}");

                context.Adoptants.AddRange(
                    new Adoptant
                    {
                        DNI = "12345678Z",
                        Nom = "Marta",
                        Cognoms = "Puig Soler",
                        TeJardi = true,
                        DataRegistre = DateTime.Today
                    },
                    new Adoptant
                    {
                        DNI = "87654321X",
                        Nom = "Pau",
                        Cognoms = "Ferrer Vidal",
                        TeJardi = false,
                        DataRegistre = DateTime.Today
                    });
                context.SaveChanges();
            }

            using (var context2 = new RefugiCOntext())
            {
                
            }
        }
    }
}






