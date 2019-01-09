using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextClosureAlgorithm
{
    public class FormalConcept
    {
        /// <summary>
        /// jedinstveni identifikator formalnog koncepta
        /// odredjen je redosledom generisanja intenata 
        /// pri izvrsenju NextClosure algoritma
        /// </summary>
        public int alias_id { get; set; }
        /// <summary>
        /// nivo na kome se koncept nalazi u latici
        /// </summary>
        public int level { get; set; }
        /// <summary>
        /// skup objekata koji cine koncept
        /// </summary>
        public List<Item> itemSet { get; set; }
        /// <summary>
        /// skup atributa koji cine koncept
        /// </summary>
        public List<Attribute> attrSet { get; set; }
        /// <summary>
        /// skup alias_id-eva koncepata koji su podskupovi ovog koncepta
        /// </summary>
        public HashSet<int> subsets { get; set; }
        public FormalConcept(int id, List<Attribute> attrs, List<Item> items, HashSet<int> subsets) {
            this.alias_id = id;
            this.level = -1;
            this.itemSet = items;
            this.attrSet = attrs;
            this.subsets = subsets;
        }

    }
}
