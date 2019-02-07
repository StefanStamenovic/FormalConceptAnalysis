using FCAA.Data;
using FCAA.Data.Lattice;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Attribute = FCAA.Data.Attribute;
using Object = FCAA.Data.Object;
using FCAA.FormalConceptAlgorithms;

namespace FCA
{
    public static class Test
    {
        public static FormalContext biosphereFormalContext()
        {
            List<Attribute> attributes = new List<Attribute>() {
                new Attribute("a"), //needs water to live
                new Attribute("b"), //lives in water
                new Attribute("c"), //lives on land
                new Attribute("d"), //needs chlorophyll to produce food
                new Attribute("e"), //two seed leaves
                new Attribute("f"), //one seed leaf
                new Attribute("g"), //can move around
                new Attribute("h"), //has limbs
                new Attribute("i"), //suckles its offspring
            };
            List<Object> Objects = new List<Object>() {
                new Object("1"), //leech
                new Object("2"), //bream
                new Object("3"), //frog
                new Object("4"), //dog
                new Object("5"), //spike-weed
                new Object("6"), //reed
                new Object("7"), //bean
                new Object("8"), //maize
            };
            Dictionary<string, HashSet<string>> ObjectHasAttrs = new Dictionary<string, HashSet<string>>();
            Dictionary<string, HashSet<string>> attrHasObjects = new Dictionary<string, HashSet<string>>();

            for (int i = 0; i < Objects.Count; i++)
                ObjectHasAttrs.Add(Objects[i].Name, new HashSet<string>());
            for (int j = 0; j < attributes.Count; j++)
                attrHasObjects.Add(attributes[j].Name, new HashSet<string>());

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

            for (int i = 0; i < Objects.Count; i++)
                for (int j = 0; j < attributes.Count; j++)
                {
                    if (matrix[i, j])
                    {
                        ObjectHasAttrs[Objects[i].Name].Add(attributes[j].Name);
                        attrHasObjects[attributes[j].Name].Add(Objects[i].Name);
                    }
                }

            FormalContext context = null;//new FormalContext(attributes, Objects, ObjectHasAttrs, attrHasObjects);

            return context;
        }
        public static FormalContext basicFormalContext()
        {
            List<Attribute> attributes = new List<Attribute>() {
                new Attribute("1"), //needs water to live
                new Attribute("2"), //lives in water
                new Attribute("3"), //lives on land
            };
            List<Object> Objects = new List<Object>() {
                new Object("x1"), //leech
                new Object("x2"), //bream
                new Object("x3"), //frog
                new Object("x4"), //dog
            };
            Dictionary<string, HashSet<string>> ObjectHasAttrs = new Dictionary<string, HashSet<string>>();
            Dictionary<string, HashSet<string>> attrHasObjects = new Dictionary<string, HashSet<string>>();

            for (int i = 0; i < Objects.Count; i++)
                ObjectHasAttrs.Add(Objects[i].Name, new HashSet<string>());
            for (int j = 0; j < attributes.Count; j++)
                attrHasObjects.Add(attributes[j].Name, new HashSet<string>());

            bool[,] matrix = new bool[,] {
                { true,true,true},
                { true,false,true},
                { false,true,true},
                { true,false,false},
            };

            for (int i = 0; i < Objects.Count; i++)
                for (int j = 0; j < attributes.Count; j++)
                {
                    if (matrix[i, j])
                    {
                        ObjectHasAttrs[Objects[i].Name].Add(attributes[j].Name);
                        attrHasObjects[attributes[j].Name].Add(Objects[i].Name);
                    }
                }

            FormalContext context = null;//new FormalContext(attributes, Objects, ObjectHasAttrs, attrHasObjects);

            return context;
        }

        public static ConceptLattice performAlgorithm(FormalContext context, string fileName, bool writeContext, BackgroundWorker worker, DoWorkEventArgs e)
        {
            var algorithm = new NextClosureAlgorithm(context);
            var formalConcepts = algorithm.FormalConcepts();
            var lattice = new ConceptLattice(formalConcepts, context);
            return lattice;
        }
    }
}
