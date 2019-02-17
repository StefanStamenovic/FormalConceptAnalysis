using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Attribute = FCAA.Data.Attribute;
using FCAA.Data;

namespace FCAA.DataImport.ContextFileImporters
{
    public class FilePreprocessingManager
    {
        public double Treshold { get; set; }
        private static readonly Random RandomGenerator = new Random();
        public async Task<List<Document>> PreprocessFileAsync(string filePath)
        {
            List<Document> documents = new List<Document>();
            HashSet<string> allAttributes = new HashSet<string>();
            HashSet<string> excessAttributes = new HashSet<string>();//ovde se smestaju atributi koji se proglase istim tako da se oni ne obradjuju kad se opet naidje na njih
            (documents, allAttributes) = GetDocumentsAndAttributes(filePath);
            //foreach (var selectedAttr in allAttributes)
            //{
            //    if (excessAttributes.Contains(selectedAttr))
            //        continue;
            //    //TODO: Ovde ubaciti izmenu kada se servis za slicnost izmeni da prima niz atributa
            //    foreach (var attr in allAttributes)
            //    {
            //        //TODO: Ovo mora da se uradi jer je svaki atribut isti sa samim sobom, razmisliti o potencijalnoj optimizaciji ovog dela jer ipak mogu da se javi istri atributi u tagovima
            //        if (excessAttributes.Contains(attr) || selectedAttr.Equals(attr))
            //            continue;
            //        bool result = await CheckIfAttributesAreEqualAsync(attr, selectedAttr);
            //        if (result)
            //        {
            //            excessAttributes.Add(attr);
            //            RefactorDocumentTags(documents, selectedAttr, attr);
            //        }
            //    }
            //}
            return documents;
        }

        void RefactorDocumentTags(List<Document> documents, string mainAttr, string replacingAttr)
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

         async Task<bool> CheckIfAttributesAreEqualAsync(string attr1, string attr2)
        {
            //TODO: Ovde je potrebno pozvati pravi servis koji leksicki uporedjuje dve recenice
            //TODO: Dodatno, od tagova koji se sastoje vise reci (odvojene _ ili - ) potrebno je napraviti recenice pa tako leksicki uporedjivati, odnosno cilj je da ova
            //metoda uvek uporedjuje dve recenice cak i kad se one sastoje samo do po jedne reci
            //var firstSentence = attr1.Replace("-", " ");
            //var secondSentence = attr2.Replace("-", " ");
            //string myJson = "{ 'IRI1': '1', 'DESC1' : '"+firstSentence+"','IRI2': '2','DESC2' : '"+secondSentence+"' }";
            //using (var client = new HttpClient())
            //{
            //    var response = await client.PostAsync(
            //        "http://160.99.9.216/Description/api/description",
            //         new StringContent(myJson, Encoding.UTF8, "application/json"));
            //}
            var res = RandomGenerator.NextDouble();
            return res > Treshold;
        }
        (List<Document>, HashSet<string>) GetDocumentsAndAttributes(string filePath)
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

        public List<Document> ReadDocumentsFromFile(string filePath)
        {
            StreamReader sr = new StreamReader(filePath);


            string jsonString;
            List<Document> documents = new List<Document>();
            while ((jsonString = sr.ReadLine()) != null)
            {
                Document document = JsonConvert.DeserializeObject<Document>(jsonString);
                //Ovde se vrsi preprocesiranje da se od tagova povezanih - naprave recenice zbog NLP-a
                document.tags = new HashSet<string>(document.tags.Select(s => s.Replace("-", " ")));
                documents.Add(document);
            }
            sr.Close();
            return documents;
        }

        public async Task<List<ProcessedAttribute>> CalculateAndOrderPreprocessedAttributesAsync(List<Attribute> attributes)
        {
            List<ProcessedAttribute> ProcessedAttributes = new List<ProcessedAttribute>();
            var jsonStringArray = JsonConvert.SerializeObject(attributes.Select(s => s.Name).ToList());
            foreach (var att in attributes)
            {
                string Json = "{ 'IRI1': '1', 'DESC1' : '" + att.Name + "','IRI2': '2','DESC2' : " + jsonStringArray + " }";
                using (var client = new HttpClient())
                {
                    var response = await client.PostAsync(
                        "http://160.99.9.216/Description/api/description/comparepair",
                         new StringContent(Json, Encoding.UTF8, "application/json"));

                    var content = await response.Content.ReadAsStringAsync();
                    var NLPObject = JsonConvert.DeserializeObject<NLPServiceHttpResponse>(content);
                    ProcessedAttributes.Add(new ProcessedAttribute(att, NLPObject.Value.Select(x => new MeasuredAttribute(x.Key, x.Value)).ToList(), Treshold));
                }
            }
            return ProcessedAttributes.OrderByDescending(x=>x.NumOfSimilarAttributes).ToList();
        }

        public async Task ReduceContext(FormalContext context)
        {
            var ProcessedAttributes = await CalculateAndOrderPreprocessedAttributesAsync(context.AttributesArray.ToList());
            foreach (var att in ProcessedAttributes)
            {
                ReduceAttributesSetAndAttributeObjects(att, context);
                ReduceObjectAttributes(att, context);
            }
        }

        /// <summary>
        /// Iz liste AttributesSet i AttributeObject se samo brisu atributi koji ce biti zamenjeni jednim atributom
        /// </summary>
        /// <param name="att"></param>
        /// <param name="context"></param>
        public void ReduceAttributesSetAndAttributeObjects(ProcessedAttribute att, FormalContext context)
        {
            foreach (var item in att.SimilarAttributes)
            {
                context.AttributesSet.Remove(item);
                context.AttributeObjects.Remove(item);
            }
        }

        /// <summary>
        /// Iz dictionary-ja za atribute koje objekti imaju prolazi se kroz atribute, brisu se oni koji ce biti zamenjeni i ubacuje glavni ako vec nije u listi atributa koje objekat poseduje
        /// </summary>
        /// <param name="att"></param>
        /// <param name="context"></param>
        public void ReduceObjectAttributes(ProcessedAttribute att, FormalContext context)
        {
            foreach (var item in att.SimilarAttributes)
            {
                foreach (var obj in context.ObjectAttributes)
                {
                    obj.Value.Remove(item);
                    if (!obj.Value.Contains(att.Attribute))
                        obj.Value.Add(att.Attribute);
                }
            }
        }
    }

    public class Document
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

    public class MeasuredAttribute : Attribute
    {
        public float SimilarityValue { get; set; }

        public MeasuredAttribute(string name, float similarityValue) : base(name)
        {
            SimilarityValue = similarityValue;
        }

        public override bool Equals(object obj)
        {
            var attribute = obj as MeasuredAttribute;
            return attribute != null &&
                   SimilarityValue == attribute.SimilarityValue;
        }

        public override int GetHashCode()
        {
            return -1731922609 + SimilarityValue.GetHashCode();
        }
    }

    public class ProcessedAttribute
    {
        public double Treshold { get; set; }
        public Attribute Attribute { get; set; }
        public List<MeasuredAttribute> MeasuredAttributes { get; set; }
        public int NumOfSimilarAttributes { get; set; } = 0;
        public float MedianValue { get; set; } = 0;
        public List<Attribute> SimilarAttributes { get; set; }


        public ProcessedAttribute(Attribute attribute, List<MeasuredAttribute> measuredAttributes, double treshold)
        {
            Treshold = treshold;
            Attribute = attribute;
            MeasuredAttributes = measuredAttributes;
            if (MeasuredAttributes.Count != 0)
            {
                var similarAttributes = MeasuredAttributes.Where(o => o.SimilarityValue > Treshold);
                NumOfSimilarAttributes = similarAttributes.Count();
                MedianValue = similarAttributes.Sum(x => x.SimilarityValue) / MeasuredAttributes.Count;
                SimilarAttributes = similarAttributes.Select(p => new Attribute(p.Name)).ToList();
            }
                
        }
    }

    public class JsonItem
    {
        public string Key { get; set; }
        public float Value { get; set; }
    }

    public class NLPServiceHttpResponse
    {
        public int IRI1 { get; set; }
        public int IRI2 { get; set; }
        public List<JsonItem> Value { get; set; }
    }

}
