using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextClosureAlgorithm
{
    public class Item
    {
        /// <summary>
        /// id objekta (za slucaj tekstualnog fajla sa dokumentima, id dokumenta)
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// naziv objekta (za slucaj tekstualnog fajla sa dokumentima, naziv dokumenta)
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// pozicija u matrici formalnog konteksta, odnosno redosled dodavanja u listu objekata formalnog konteksta, u konkretnoj implementaciji
        /// </summary>
        public int matrixOrder { get; set; }
        public string toString() {
            return String.Format("{0}:{1}:{2}",this.id, this.name,this.matrixOrder);
        }
    }
}
