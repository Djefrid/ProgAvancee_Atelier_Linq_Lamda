using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LesFruits
{
    public class Fruit
    {
        public string Nom { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Fruit fruit &&
                   Nom == fruit.Nom;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Nom);
        }

        public override string ToString()
        {
            return Nom;
        }

        public static IEnumerable<Fruit> PlusPopulaire(IEnumerable<Personne> personnes)
        {
            return personnes.SelectMany(person => person.FruitsAimes).GroupBy(fruit => fruit)
                            .Select(group => new { Nom = group.Key, Popularite = group.Count() })
                            .OrderByDescending(fruit => fruit.Popularite)
                            .Select(fruit => new Fruit { Nom = fruit.Nom.ToString() });
        }

        public static IEnumerable<Fruit> PlusPopulaireSR(IEnumerable<Personne> personnes)
        {
            return from personne in personnes
                   from fruit in personne.FruitsAimes
                   group fruit by fruit into groupeFruits
                   orderby groupeFruits.Count() descending
                   select groupeFruits.Key;
        }


    }
}
