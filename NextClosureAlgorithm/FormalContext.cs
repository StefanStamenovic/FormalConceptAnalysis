using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace NextClosureAlgorithm
{
    public class FormalContext
    {
        /// <summary>
        /// lista svih atributa fomalnog konteksta
        /// </summary>
        public List<Attribute> Attributes { get; set; }
        private Attribute[] _attributes { get; set; }
        /// <summary>
        /// lista svih objekata formalnog konteksta
        /// </summary>
        public List<Item> Items { get; set; }
        private Item[] _items { get; set; }
        public Dictionary<string, HashSet<string>> Relations { get; set; }

        public FormalContext(List<Attribute> attributes, List<Item> items, Dictionary<string, HashSet<string>> relations)
        {
            Attributes = attributes;
            Items = items;
            Relations = relations;
        }

        /// <summary>
        /// key:naziv objekta
        /// value:skup naziva atributa koje objekat sadrzi
        /// </summary>
        public Dictionary<string, HashSet<string>> itemHasAttrs { get; set; }
        /// <summary>
        /// key:naziv atributa
        /// value:skup naziva svih objekata koji sadrze key atribut
        /// </summary>
        public Dictionary<string, HashSet<string>> attrHasItems { get; set; }

        public FormalContext(List<Attribute> attributes, List<Item> items, Dictionary<string, HashSet<string>> itemHasAttrs, Dictionary<string, HashSet<string>> attrHasItems)
        {
            if (attributes == null ||
                !attributes.Any() ||
                items == null ||
                !items.Any())
                throw new ArgumentNullException("Invalid arguments.");

            this.Attributes = attributes;
            this.Items = items;
            this.itemHasAttrs = itemHasAttrs;
            this.attrHasItems = attrHasItems;

            for (int i = 0; i < attributes.Count; i++)
                this.Attributes[i].lecticPosition = i;

            for (int i = 0; i < items.Count; i++)
                this.Items[i].matrixOrder = i;
        }
        /// <summary>
        /// Extent za skup atributa je skup objekata koji sadrze sve atribute iz tog skupa
        /// </summary>
        /// <param name="attrs">skup atrubuta za koji se izracunava extent</param>
        /// <returns>extent skupa atributa</returns>
        public List<Item> Extent(IEnumerable<Attribute> attrs)
        {
            List<Item> resultItems = new List<Item>();
            if (attrs == null || !attrs.Any())
                return this.Items;


            foreach (int order in this.Items.Select(o => o.matrixOrder))
            {
                bool containsAllAttrs = true;
                foreach (string attr in attrs.Select(a => a.name))
                {
                    if (!(itemHasAttrs[Items[order].name].Contains(attr)))
                    {
                        containsAllAttrs = false;
                        break;
                    }
                }

                if (containsAllAttrs)
                    resultItems.Add(Items.FirstOrDefault(i => i.matrixOrder == order));
            }
            return resultItems;
        }
        /// <summary>
        /// Intent za skup objekata je skup atributa koje sadrze svi objekti iz tog skupa
        /// </summary>
        /// <param name="itemSet">skup objekata za koji se izracunava intent</param>
        /// <returns>intent skupa objekata</returns>
        public List<Attribute> Intent(IEnumerable<Item> itemSet)
        {
            List<Attribute> resultAttrs = new List<Attribute>();
            if (itemSet == null || !itemSet.Any())
                return this.Attributes;

            foreach (int position in Attributes.Select(p => p.lecticPosition))
            {
                bool containsAllItems = true;
                foreach (Item item in itemSet)
                {
                    if (!(this.attrHasItems[Attributes[position].name].Contains(item.name)))
                    {
                        containsAllItems = false;
                        break;
                    }
                }

                if (containsAllItems)
                    resultAttrs.Add(Attributes.FirstOrDefault(a => a.lecticPosition == position));
            }
            return resultAttrs;


        }
        
    }
}
