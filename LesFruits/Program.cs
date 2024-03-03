using System;
using System.Collections.Generic;
using System.Linq;

namespace LesFruits
{
    public class Program
    {
        static void Main(string[] _)
        {
            var fruits = new List<Fruit>()
            {
                new Fruit() { Nom = "Abricot"},   new Fruit() { Nom = "Banane"},    new Fruit() { Nom = "Cerise"}, new Fruit() { Nom = "Datte"},
                new Fruit() { Nom = "Framboise"}, new Fruit() { Nom = "Grenade"},   new Fruit() { Nom = "Kiwi"},   new Fruit() { Nom = "Lime"},
                new Fruit() { Nom = "Mangue"},    new Fruit() { Nom = "Nectarine"}, new Fruit() { Nom = "Olive"},  new Fruit() { Nom = "Pomme"}
            };

            var personnes = new List<Personne>()
            {
                new Personne() { Nom = "Alice",   Genre = 'F', Age = 22, FruitsAimes = new List<Fruit>() { fruits[0], fruits[1], fruits[10] } },
                new Personne() { Nom = "Bob",     Genre = 'M', Age = 12, FruitsAimes = new List<Fruit>() { fruits[4], fruits[5], fruits[6], fruits[7], fruits[8] } },
                new Personne() { Nom = "Charlie", Genre = 'M', Age = 31, FruitsAimes = new List<Fruit>() { fruits[0], fruits[1], fruits[4], fruits[11] } },
                new Personne() { Nom = "Diane",   Genre = 'F', Age = 45, FruitsAimes = new List<Fruit>() { fruits[2], fruits[4] } },
                new Personne() { Nom = "Eve",     Genre = 'F', Age = 4,  FruitsAimes = new List<Fruit>() { } },
            };

            Console.WriteLine("Les fruits qui contiennent la lettre A sont : ");
            IEnumerable<Fruit> reponse = fruits.Where(AvecA);

            Console.WriteLine ($"{string.Join(separator: ", ", values: reponse)}");
            double ageMoyenM = personnes.Where(p => p.Genre == 'M').Average(p => p.Age);
            Console.WriteLine($"Age moyen des hommes {ageMoyenM}");

            var query = personnes.Where(p => p.Genre == 'M').
                            OrderBy(p => p.Age).
                            Select(p => new { p.Age, p.Genre});
            Console.WriteLine($"Age et genre des hommes : {string.Join(separator: ", ", values: query)}");


            Console.WriteLine("personne la plus agée ");

            Console.Read();


        }
        static bool AvecA(Fruit fruit)
        {
            return fruit.Nom.ToUpper().Contains('A');
        }


        public static IEnumerable<Personne> Enfants(IEnumerable<Personne> personnes)
        {
            return personnes.Where(pers => pers.Age < 19).Select(p => p);
        }

        public static IEnumerable<Personne> EnfantsSR(IEnumerable<Personne> personnes)
        {
            return from person in personnes
                   where person.Age < 19
                   select person;
        }

        public static Personne LaPlusVieille(IEnumerable<Personne> personnes)
        {
            return personnes.Where(pers => pers.Age == personnes.Max(pers => pers.Age)).SingleOrDefault();
        }

        public static Personne LaPlusVieilleSR(IEnumerable<Personne> personnes)
        {
            return (from person in personnes
                    orderby person.Age descending
                    select person).SingleOrDefault();
        }


        public static IEnumerable<Fruit> PlusPopulaire(IEnumerable<Personne> personnes)
        {
            return personnes.SelectMany(person => person.FruitsAimes).GroupBy(fruit => fruit)
                            .OrderByDescending(fruit => fruit.Count())
                            .Select(fruit => fruit.Key);
        }

        public static IEnumerable<Fruit> PlusPopulaireSR(IEnumerable<Personne> personnes)
        {
            return from personne in personnes
                   from fruit in personne.FruitsAimes
                   group fruit by fruit into groupeFruits
                   orderby groupeFruits.Count() descending
                   select groupeFruits.Key;
        }

        public static void ParGenre(IEnumerable<Personne> personnes)
        {
            var genre = personnes.GroupBy(p => p.Genre).Select(g => new { key = g.Key,
                                                                          count = g.Count(),
                                                                          MaxAge = g.Max(p => p.Age),
                                                                          minAge = g.Min(p => p.Age) });

            var genreSR = from p in personnes
                          group p by p.Genre into g
                          select new
                          {
                              key = g.Key,
                              count = g.Count(),
                              MaxAge = g.Max(p => p.Age),
                              minAge = g.Min(p => p.Age)
                          };

            foreach (var g in genre)
            {
               Console.WriteLine($"Genre: {g.key}"); 
               Console.WriteLine($"Nombre de personnes de ce genre : {g.count}"); 
               Console.WriteLine($"L'âge de la personne la plus vieille de ce genre : {g.MaxAge}");
               Console.WriteLine($"L'âge de la personne la plus jeune de ce genre : {g.minAge}");
            }
        }

    }
}
