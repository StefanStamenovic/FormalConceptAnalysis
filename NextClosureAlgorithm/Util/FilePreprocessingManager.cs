using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextClosureAlgorithm.Util
{
    class FilePreprocessingManager
    {
        public double Treshold { get; set; }
        private static readonly Random RandomGenerator = new Random();
        public List<Document> PreprocessFile(string filePath)
        {
            List<Document> documents = new List<Document>();
            HashSet<string> allAttributes = new HashSet<string>();
            HashSet<string> excessAttributes = new HashSet<string>();//ovde se smestaju atributi koji se proglase istim tako da se oni ne obradjuju kad se opet naidje na njih
            (documents, allAttributes) = GetDocumentsAndAttributes(filePath);
            foreach (var selectedAttr in allAttributes)
            {
                if (excessAttributes.Contains(selectedAttr))
                    continue;
                foreach (var attr in allAttributes)
                {
                    //TODO: Ovo mora da se uradi jer je svaki atribut isti sa samim sobom, razmisliti o potencijalnoj optimizaciji ovog dela jer ipak mogu da se javi istri atributi u tagovima
                    if (excessAttributes.Contains(attr) || selectedAttr.Equals(attr))
                        continue;
                    if(CheckIfAttributesAreEqual(attr,selectedAttr))
                    {
                        //allAttributes.Remove(attr);
                        excessAttributes.Add(attr);
                        RefactorDocumentTags(documents, selectedAttr, attr);
                    }
                }
            }
            return documents;
        }

        public void RefactorDocumentTags(List<Document> documents, string mainAttr, string replacingAttr)
        {
            for (int i = 0; i < documents.Count; i++)
            {
                if(documents[i].tags.Contains(replacingAttr))
                {
                    documents[i].tags.Remove(replacingAttr);
                    documents[i].tags.Add(mainAttr);
                }
               
            }
        }

        public bool CheckIfAttributesAreEqual(string attr1, string attr2)
        {
            //TODO: Ovde je potrebno pozvati pravi servis koji leksicki uporedjuje dve recenice
            //TODO: Dodatno, od tagova koji se sastoje vise reci (odvojene _ ili - ) potrebno je napraviti recenice pa tako leksicki uporedjivati, odnosno cilj je da ova
            //metoda uvek uporedjuje dve recenice cak i kad se one sastoje samo do po jedne reci
            var res = RandomGenerator.NextDouble();
            return res > Treshold;
        }
        public (List<Document>, HashSet<string>) GetDocumentsAndAttributes(string filePath)
        {
            StreamReader sr = new StreamReader(filePath);


            string jsonString;
            List<Document> documents = new List<Document>();

            HashSet<string> allAttributes = new HashSet<string>();


            while ((jsonString = sr.ReadLine()) != null)
            {
                Document document = JsonConvert.DeserializeObject<Document>(jsonString);
                documents.Add(document);
                if (document.tags.Count == 0) continue;

                HashSet<string> tagList = document.tags;
                allAttributes.UnionWith(tagList);
            }
            sr.Close();
            return (documents, allAttributes);
        }
    }

    class Document
    {

        public string oid { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public HashSet<string> tags { get; set; }

        public Document()
        {
            this.tags = new HashSet<string>();
        }

        public Document(string oid, string id, string name, HashSet<string> tags)
        {
            this.oid = oid;
            this.id = id;
            this.name = name;
            this.tags = tags;
        }
    }
}
