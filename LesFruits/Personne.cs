using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LesFruits
{
    public class Personne
    {
        public string Nom { get; set; }
        public int Age { get; set; }
        public char Genre { get; set; }

        public IList<Fruit> FruitsAimes { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Personne personne &&
                   Nom == personne.Nom &&
                   Age == personne.Age &&
                   Genre == personne.Genre &&
                   EqualityComparer<IList<Fruit>>.Default.Equals(FruitsAimes, personne.FruitsAimes);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Nom, Age, Genre, FruitsAimes);
        }

        public override string ToString()
        {
            return $"{Nom} aime {string.Join(", ", FruitsAimes)}";
        }

       

    }
}
