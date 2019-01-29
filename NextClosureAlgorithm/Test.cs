using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.ComponentModel;

namespace NextClosureAlgorithm
{
    public static class Test
    {
        public static FormalContext biosphereFormalContext()
        {
            List<Attribute> attributes = new List<Attribute>() {
                new Attribute(){ name="a"}, //needs water to live
                new Attribute(){ name="b"}, //lives in water
                new Attribute(){ name="c"}, //lives on land
                new Attribute(){ name="d"}, //needs chlorophyll to produce food
                new Attribute(){ name="e"}, //two seed leaves
                new Attribute(){ name="f"}, //one seed leaf
                new Attribute(){ name="g"}, //can move around
                new Attribute(){ name="h"}, //has limbs
                new Attribute(){ name="i"}, //suckles its offspring
            };
            List<Item> items = new List<Item>() {
                new Item(){ name ="1", id="1"}, //leech
                new Item(){ name ="2", id="2"}, //bream
                new Item(){ name ="3", id="3"}, //frog
                new Item(){ name ="4", id="4"}, //dog
                new Item(){ name ="5", id="5"}, //spike-weed
                new Item(){ name ="6", id="6"}, //reed
                new Item(){ name ="7", id="7"}, //bean
                new Item(){ name ="8", id="8"}, //maize
            };
            Dictionary<string, HashSet<string>> itemHasAttrs = new Dictionary<string, HashSet<string>>();
            Dictionary<string, HashSet<string>> attrHasItems = new Dictionary<string, HashSet<string>>();

            for (int i = 0; i < items.Count; i++)
                itemHasAttrs.Add(items[i].name, new HashSet<string>());
            for (int j = 0; j < attributes.Count; j++)
                attrHasItems.Add(attributes[j].name, new HashSet<string>());

            bool[,] matrix = new bool[,] {
                { true,true,false,false,false,false,true,false,false},
                { true,true,false,false,false,false,true,true,false},
                { true,true,true,false,false,false,true,true,false},
                { true,false,true,false,false,false,true,true,true},
                { true,true,false,true,false,true,false,false,false},
                { true,true,true,true,false,true,false,false,false},
                { true,false,true,true,true,false,false,false,false},
                { true,false,true,true,false,true,false,false,false}
            };

            for (int i = 0; i < items.Count; i++)
                for (int j = 0; j < attributes.Count; j++)
                {
                    if (matrix[i, j])
                    {
                        itemHasAttrs[items[i].name].Add(attributes[j].name);
                        attrHasItems[attributes[j].name].Add(items[i].name);
                    }
                }

            FormalContext context = new FormalContext(attributes, items, itemHasAttrs,attrHasItems);

            return context;
        }
        public static FormalContext basicFormalContext()
        {
            List<Attribute> attributes = new List<Attribute>() {
                new Attribute(){ name="1"}, //needs water to live
                new Attribute(){ name="2"}, //lives in water
                new Attribute(){ name="3"}, //lives on land
            };
            List<Item> items = new List<Item>() {
                new Item(){ name ="x1", id="1" }, //leech
                new Item(){ name ="x2", id="2" }, //bream
                new Item(){ name ="x3", id="3" }, //frog
                new Item(){ name ="x4", id="4"}, //dog
            };
            Dictionary<string, HashSet<string>> itemHasAttrs = new Dictionary<string, HashSet<string>>();
            Dictionary<string, HashSet<string>> attrHasItems = new Dictionary<string, HashSet<string>>();

            for (int i = 0; i < items.Count; i++)
                itemHasAttrs.Add(items[i].name, new HashSet<string>());
            for (int j = 0; j < attributes.Count; j++)
                attrHasItems.Add(attributes[j].name, new HashSet<string>());

            bool[,] matrix = new bool[,] {
                { true,true,true},
                { true,false,true},
                { false,true,true},
                { true,false,false},
            };

            for (int i = 0; i < items.Count; i++)
                for (int j = 0; j < attributes.Count; j++)
                {
                    if (matrix[i, j])
                    {
                        itemHasAttrs[items[i].name].Add(attributes[j].name);
                        attrHasItems[attributes[j].name].Add(items[i].name);
                    }
                }

            FormalContext context = new FormalContext(attributes, items,itemHasAttrs,attrHasItems);

            return context;
        }
        public static ConceptLattice performAlgorithm(FormalContext context,string fileName, bool writeContext,BackgroundWorker worker, DoWorkEventArgs e)
        {
            ConceptLattice lattice = new ConceptLattice(context);
            lattice.computeConceptLattice(worker, e);
            lattice.WriteOutput(fileName, context, writeContext);

            return lattice;
        }

        public static Domain.ConceptLattice newperformAlgorithm(Domain.FormalContext context, BackgroundWorker worker, DoWorkEventArgs e)
        {
            var algorithm = new Domain.NextClosureAlgorithm(context);
            var formalConcepts = algorithm.FormalConcepts();
            Domain.ConceptLattice lattice = new Domain.ConceptLattice(formalConcepts, context);
            return lattice; 
        }
    }
}
