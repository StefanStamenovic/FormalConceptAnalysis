using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NextClosureAlgorithm.Util
{
    public class LegacyFCAFileReaderWithPreprocessing : IFCAFileReader
    {
        public ICollection<Attribute> ReadAttributes(string filePath)
        {
            throw new NotImplementedException();
        }
        public ICollection<Item> ReadObjects(string filePath)
        {
            throw new NotImplementedException();
        }
        public async Task<FormalContext> ReadContextAsync(string filePath)
        {
            FilePreprocessingManager preprocessingManager = new FilePreprocessingManager() { Treshold = 0.2};
            List<Document> docs = await preprocessingManager.PreprocessFileAsync(filePath);
            StreamReader sr = new StreamReader(filePath);

   
            string jsonString;
            List<dynamic> documents = new List<dynamic>();

            HashSet<string> allAttributes = new HashSet<string>();
           /* while ((jsonString = sr.ReadLine()) != null)
            {
                dynamic document = JsonConvert.DeserializeObject(jsonString);
                documents.Add(document);
            }*/

            List<Item> items = new List<Item>();
            List<Attribute> attributes = new List<Attribute>();
            Dictionary<string, HashSet<string>> itemHasAttrs = new Dictionary<string, HashSet<string>>();
            Dictionary<string, HashSet<string>> attrHasItems = new Dictionary<string, HashSet<string>>();

            while ((jsonString = sr.ReadLine()) != null)
            {
                dynamic document = JsonConvert.DeserializeObject(jsonString);
                if (document.tags.Count == 0) continue;
                Item item = new Item() { id = document.id, name = document.name };
                items.Add(item);

                itemHasAttrs.Add(item.name, new HashSet<string>());
                List<string> tagList = document.tags.ToObject<List<string>>();
                allAttributes.UnionWith(tagList);
                for (int i = 0; i < document.tags.Count; i++)
                {
                    string tag = document.tags[i].ToString();
                    Attribute attr = new Attribute() { name = tag };

                    if (!(attributes.Any(a => a.name == tag)))
                    {
                        attributes.Add(attr);
                        attrHasItems.Add(attr.name, new HashSet<string>());
                    }

                    attrHasItems[attr.name].Add(item.name);
                    itemHasAttrs[item.name].Add(attr.name);
                }
            }
            sr.Close();
            return new FormalContext(attributes, items, itemHasAttrs, attrHasItems);
        }

        public FormalContext ReadContext(string filePath)
        {
            throw new NotImplementedException();
        }
    }
}
