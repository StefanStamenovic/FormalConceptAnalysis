using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.IO;

namespace NextClosureAlgorithm
{
    public class ConceptLattice
    {
        private FormalContext context { get; set; }
        /// <summary>
        /// formalni koncepti rasporedjeni po nivoima latice
        /// </summary>
        public Dictionary<int, HashSet<FormalConcept>> levels { get; set; }
        public ConceptLattice(FormalContext context)
        {
            this.context = context;
            levels = new Dictionary<int, HashSet<FormalConcept>>();
        }
        /// <summary>
        /// Funkcija za izracunavanje latice na osnovu formalnog konteksta
        /// 1. Izracunavaju se f. koncepti
        /// 2. Koncepti se rasporedjuju po nivoima
        /// </summary>
        public void computeConceptLattice(BackgroundWorker worker, DoWorkEventArgs e)
        {
            List<FormalConcept> concepts = performAlgorithm(worker, e);

            int maxLevel = concepts.LastOrDefault().level; //koncepti su uredjeni u rastucem redosledu po level-u na kome se nalaze u latici

            for (int i = 0; i <= maxLevel; i++)
                levels.Add(i, new HashSet<FormalConcept>());

            foreach (FormalConcept concept in concepts)
                levels[concept.level].Add(concept);
        }

        /// <summary>
        /// Funkcija za izracunavanje koncepata formalnog konteksta
        /// </summary>
        /// <returns>Lista f. koncepata</returns>
        public List<FormalConcept> performAlgorithm(BackgroundWorker worker, DoWorkEventArgs e)
        {
            return computeFormalConcepts(NextClosure(worker, e));
        }
        /// <summary>
        /// Slajd 35 na:
        /// http://www.math.tu-dresden.de/~ganter/psfiles/FingerExercises.pdf
        /// 
        /// Algoritam za generisanje intenata (liste atributa) po leksickom redosledu
        /// tako da nije potrebna provera vec generisanih intenata pri generisanju sledeceg intent-a
        /// 
        /// Leksicko uredjenje skupova: 
        /// Za dva skupa atributa A i B, 
        ///  A <i B ako najmanji element po kom se razlikuju (i) pripada skupu B 
        /// (elementi su uredjeni po redosledu dodavanja u listu atributa)
        /// 
        ///Najpre se krece od skupa atributa koji je intent svih objekata f.konteksta
        ///Naredni zatvoreni skup po leksickom uredjenju u odnosu na A je A+i (Closure atributa i i skupa A)
        ///ali tako da vazi A <i A+i
        /// </summary>
        /// <returns></returns>
        private List<List<Attribute>> NextClosure(BackgroundWorker worker, DoWorkEventArgs e)
        {
            List<Item> items = context.Items;
            List<Attribute> attributes = context.Attributes;

            //pocetni skup je intent svih objekata
            List<Attribute> setA = context.Intent(items).ToList();
            List<List<Attribute>> resultSets = new List<List<Attribute>>();
            bool found = true;

            resultSets.Add(setA);
            if (worker.CancellationPending)
            {
                e.Cancel = true;
                return resultSets;
            }

            while (found)
            {
                // ukoliko skup sadrzi sve atribute f. konteksta, to je poslednji intent i kraj algoritma
                if (!attributes.Any(a => !setA.Contains(a)))
                    return resultSets;

                foreach (var ai in attributes.Where(a => !setA.Contains(a)).OrderByDescending(a => a.lecticPosition))
                {
                    if (worker.CancellationPending)
                    {
                        e.Cancel = true;
                        return resultSets;
                    }
                    List<Attribute> closure = ai.Closure(setA, context);

                    //proverava se da li je dobijeni skup najmanji skup veci od trenutnog po leksickom redosledu
                    Attribute newMinimal = closure.Where(c => !setA.Contains(c)).OrderBy(c => c.lecticPosition).FirstOrDefault();

                    if (newMinimal == ai)
                    {
                        setA = closure;
                        resultSets.Add(closure);
                        found = true;

                        if (setA.SetEquals(attributes))
                        {
                            return resultSets;
                        }

                        break;
                    }
                    else found = false;
                }

                if (!found)
                    throw new Exception("Not found.");
            }

            return null;
        }
        /// <summary>
        /// Najpre se za svaki od intenata dobijenih NextClosure algoritmom odredjuju podskupovi
        /// Zatim se na osnovu intenata izracunavaju se extenti
        /// Na osnovu toga se kreira lista formalnih koncepata
        /// Na osnovu relacija naskup-podskup izmedju intenata se odredjuju nivoi formalnim koncepatima
        /// </summary>
        /// <param name="intents">skupovi atributa dobijeni NextClosure algoritmom</param>
        /// <returns>lista formalnih koncepata</returns>
        private List<FormalConcept> computeFormalConcepts(List<List<Attribute>> intents)
        {
            List<FormalConcept> resultConcepts = new List<FormalConcept>();

            Dictionary<int, HashSet<int>> relations = computeRelations(intents);
            HashSet<int> subsets = new HashSet<int>();

            for (int i = 0; i < intents.Count; i++)
            {
                subsets = relations[i];
                FormalConcept concept = new FormalConcept(i, intents[i], context.Extent(intents[i]).ToList(), subsets);
                resultConcepts.Add(concept);
            }

            resultConcepts = computeLevels(resultConcepts);

            return resultConcepts;
        }
        /// <summary>
        /// Funkcija za izracunavanje relacija (nadskup-podskupovi) izmedju intenata (skupova atributa)
        /// </summary>
        /// <param name="intents"></param>
        /// <returns>
        /// key:id formalnog koncepta kom intent (nadskup atributa) pripada
        /// value: skup id-eva fomalnih koncepata ciji intenti (skupovi atributa) su podskupovi intenta konkretnog f. koncepta
        /// </returns>
        private Dictionary<int, HashSet<int>> computeRelations(List<List<Attribute>> intents)
        {
            Dictionary<int, HashSet<int>> result = new Dictionary<int, HashSet<int>>();

            for (int i = 0; i < intents.Count; i++)
                result.Add(i, new HashSet<int>());

            Parallel.For(0, intents.Count, i =>
            {
                for (int j = 0; j <= i; j++)
                {
                    if (i != j && intents[i].Contains(intents[j]))
                    {
                        result[i].Add(j);
                    }
                }
            });

            for (int i = 1; i < result.Count; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    if (result[i].Contains(j))
                        result[i].RemoveWhere(r => result[j].Contains(r));
                }
            }

            return result;
        }
        /// <summary>
        /// Na osnovu relacija izmedju formalnih koncepata,
        /// odredjuju se nivo kome svaki koncept pripada
        /// </summary>
        /// <param name="concepts"></param>
        /// <returns></returns>
        private List<FormalConcept> computeLevels(List<FormalConcept> concepts)
        {
            List<FormalConcept> leftConcepts = new List<FormalConcept>();

            foreach (FormalConcept concept in concepts)
                leftConcepts.Add(concept);

            List<int> parentConcepts = new List<int>();
            List<int> tmpLevelConcepts = new List<int>();
            //Prvi koncept u latici je onaj koji sadrzi sve objekte i prazan skup atributa
            int currentLevel = 0;
            concepts[0].level = currentLevel;
            tmpLevelConcepts.Add(concepts[0].alias_id);
            leftConcepts.RemoveAt(0);

            while (leftConcepts.Any())
            {
                currentLevel++;
                foreach (int c in tmpLevelConcepts)
                    parentConcepts.Add(c);

                tmpLevelConcepts = new List<int>();

                for (int i = leftConcepts.Count - 1; i >= 0; i--)
                {
                    //Svaki od preostalih koncepata cijim podkupovima je vec odredjen nivo
                    //se nalazi na takucem nivou
                    if (!(leftConcepts[i].subsets.Except(parentConcepts).ToList().Any()))
                    {
                        concepts[leftConcepts[i].alias_id].level = currentLevel;
                        tmpLevelConcepts.Add(leftConcepts[i].alias_id);
                        leftConcepts.RemoveAt(i);
                    }
                }
            }

            //Koncepti se sortiraju prema nivou kome pripadaju
            //u rastuci redosled
            concepts.Sort((ci, cj) => ci.level.CompareTo(cj.level));

            return concepts;
        }

        /// <summary>
        /// Upis formalnog koncepta i rezultujuce latice u tekstualni fajl
        /// </summary>
        /// <param name="outputFile">Putanja do fajla u koji se upisuje rezultat</param>
        /// <param name="context">Formalni kontekst za koji je generisana latica</param>
        /// <param name="writeContext">true: ukoliko se upisuje i formalni kontekst u fajl (skup objekata i atributa)</param>
        public void WriteOutput(string outputFile, FormalContext context, bool writeContext)
        {
            StreamWriter writer = File.CreateText(outputFile);
            if (writeContext)
            {
                writer.WriteLine("Attributes: (lecticPosition, name)");
                foreach (var attr in context.Attributes)
                {
                    writer.WriteLine("(" + attr.lecticPosition + ", " + attr.name + ")");
                }

                writer.WriteLine("Items: (matrixOrded, id, name)");
                foreach (var item in context.Items)
                {
                    writer.WriteLine("(" + item.matrixOrder + ", " + item.id + ", " + item.name + ")");
                }
            }
            writer.WriteLine("Formal concepts by levels: (list of items, list of attributes)\n");
            for (int i = 0; i < levels.Count; i++)
            {
                writer.WriteLine("LEVEL {0}:\n", i);

                foreach (FormalConcept concept in levels[i])
                {
                    writer.WriteLine("{(" + string.Join(", ", concept.itemSet.Select(itm => itm.name)) + ") ,(" +
                                            string.Join(", ", concept.attrSet.Select(attr => attr.name)) + ")}\n");
                }
            }

            writer.Close();
        }

    }
}
