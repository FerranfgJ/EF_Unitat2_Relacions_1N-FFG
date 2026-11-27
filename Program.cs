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
            }
            //Exercici 1 - Obtenir animals disponibles per espècie
            using (var ctx3 = new RefugiCOntext())
            {
                Console.WriteLine("\n=== EXERCICI 1: LLISTAT D'ANIMALS DISPONIBLES PER ESPÈCIE ===");
                AnimalService animalService = new AnimalService(ctx3);
 
                //Obtenir animals disponibles de la espècie GOS
                List<Animal> animals = animalService.GetDisponibles(Especie.GOS);
                if (animals.Count == 0){
                    Console.WriteLine($"No hi ha animals de la especie GOS");
                }else{
                    Console.WriteLine($"Hi ha {animals.Count} animals de la especie GOS");
                    foreach (var a in animals){
                        Console.WriteLine($"{a}");
                    }
                }
                
                //Obtenir animals disponibles de l'espècie ALTRES
                List<Animal> altres = animalService.GetDisponibles(Especie.ALTRES);
                if (altres.Count == 0){
                    Console.WriteLine("No hi ha animals disponibles de l'espècie ALTRES");
                }
            }
            
            //Exercici 2 - Inserim dos adoptants
            using (var ctx4 = new RefugiCOntext()){
                ctx4.Adoptants.Add(new Adoptant() { DNI = "12345678Z", Nom = "Marta", Cognoms = "Puig Soler", TeJardi = true });
                ctx4.Adoptants.Add(new Adoptant() { DNI = "87654321X", Nom = "Pau", Cognoms = "Ferrer Vidal", TeJardi = false });
                ctx4.SaveChanges();
 
                //abans de llegir: buida el que recorda i et força a llegir de la BD
                ctx4.ChangeTracker.Clear();
 
                AdoptantService adoptantService = new AdoptantService(ctx4);
                Console.WriteLine("\n=== EXERCICI 2: LLISTAT D'ADOPTANTS ===");
                foreach (Adoptant adoptant in adoptantService.GetAdoptants()){
                    Console.WriteLine($" > {adoptant} · animals: {adoptant.Animals.Count}");
                    foreach (Animal animal in adoptant.Animals){
                        Console.WriteLine($"  - {animal.Nom} ({animal.Especie})");
                    }
                }
            }
            
            //Exercici 3 - Adopció d'animals
            using (var ctx5 = new RefugiCOntext())
            {
                AdoptantService adoptantService = new AdoptantService(ctx5);
                Console.WriteLine("\n=== EXERCICI 3: ADOPTEM ANIMALS  ===");
                adoptantService.Adoptar("00000000A", "724098100000001");   // l'adoptant no existeix
                adoptantService.Adoptar("12345678Z", "000000000000000");   // l'animal no existeix
                adoptantService.Adoptar("87654321X", "724098100000001");   // l'adoptant no te jardí
                adoptantService.Adoptar("12345678Z", "724098100000001");   // tot correcte, s'adopta
                adoptantService.Adoptar("87654321X", "724098100000001");   // l'animal ja esta adoptat
 
                //abans de llegir: buida el que recorda i et força a llegir de la BD
                ctx5.ChangeTracker.Clear();
 
                //Mostrem llistat d'adoptants
                Console.WriteLine("\nMostrem llistat d'adoptants actualitzada:");
                foreach (Adoptant adoptant in adoptantService.GetAdoptants()){
                    Console.WriteLine($" > {adoptant} · animals: {adoptant.Animals.Count}");
                    foreach (Animal animal in adoptant.Animals){
                        Console.WriteLine($"  - {animal.Nom} ({animal.Especie})");
                    }
                }
            }
        }
    }
}






