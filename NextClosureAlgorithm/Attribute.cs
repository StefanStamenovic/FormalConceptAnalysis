using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextClosureAlgorithm
{

    public class Attribute
    {
        /// <summary>
        /// naziv atributa
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// NextClosure algoritam: atributi moraju biti uredjeni po nekom kriterijumu (lecticPosition); 
        ///u ovom slucaju je to redosled dodavanja u listu atributa formalnog konteksta
        /// </summary>
        public int lecticPosition { get; set; }

        /**/
        /// <summary>
        /// Closure: je jednak intentu extenta leksickog skupa (lecticSet, koji se izracunava
        ///FormLecticSet funkcijom)
        /// </summary>
        /// <param name="setA">skup atributa za koji se formira leksicki skup</param>
        /// <param name="formalContext">formalni kontekst kom atribut i skup atributa pripadaju</param>
        /// <returns>intentu extenta leksickog skupa</returns>
        public List<Attribute> Closure(List<Attribute> setA, FormalContext formalContext)
        {
            List<Attribute> lecticSet = FormLecticSet(setA);
            return formalContext.Intent(formalContext.Extent(lecticSet));
        }

        /// <summary>
        /// LecticSet je unija atributa za koji se poziva funkcija
        ///i svih atributa iz skupa setA koji imaju manji lecticPosition od atributa
        ///za koji je funkcija pozvana
        /// </summary>
        /// <param name="setA"></param>
        /// <returns></returns>
        public List<Attribute> FormLecticSet(List<Attribute> setA)
        {
            List<Attribute> result = new List<Attribute>();
            result = setA.Where(a => a.lecticPosition < this.lecticPosition).ToList(); 
            result.Add(this);

            return result;
        }
        public string toString()
        {
            return String.Format("{0}:{1}", this.name, this.lecticPosition);
        }
    }
}
