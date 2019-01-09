using Newtonsoft.Json;
using NextClosureAlgorithm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextClosureAlgorithm.Util
{
    public interface IFCAFileReader
    {
        FormalContext ReadContext(string filePath);
        ICollection<Attribute> ReadAttributes(string filePath);
        ICollection<Item> ReadObjects(string filePath);
    }

    /// <summary>
    /// Builtin reader
    /// </summary>
    /// <seealso cref="FCA.Util.IContextFromFileReader" />
    public class LegacyFCAFileReader : IFCAFileReader
    {
        public ICollection<Attribute> ReadAttributes(string filePath)
        {
            throw new NotImplementedException();
        }
        public ICollection<Item> ReadObjects(string filePath)
        {
            throw new NotImplementedException();
        }
        public FormalContext ReadContext(string filePath)
        {

            StreamReader sr = new StreamReader(filePath);

            string jsonString;
            List<dynamic> documents = new List<dynamic>();

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
    }

    /// <summary>
    /// FCA file reader that read formal concept data from json file
    /// Format: [{id: #string, tags : [#string, ...], category : [#string, ...]}, ...][...]
    /// NOTICE: Files contains arrays of arrays that are not separated and we replace "][" with "," to get single array
    /// </summary>
    /// <seealso cref="NextClosureAlgorithm.Util.IFCAFileReader" />
    public class FCAFileReader : IFCAFileReader
    {
        public bool IgnoreObjectsWithoutAttributes { get; set; } = true;
        public bool UseAttributeReduction { get; set; } = true;

        public ICollection<Attribute> ReadAttributes(string filePath)
        {
            var attribute_tags = new HashSet<string>();
            var json = ReadJsonData(filePath);
            dynamic documents = JsonConvert.DeserializeObject(json);
            foreach(var document in documents)
            {
                foreach (string tag in document.tags)
                {
                    if (!attribute_tags.Contains(tag))
                    {
                        attribute_tags.Add(tag);
                    }
                }
            }
            var attributes = new List<Attribute>();
            attribute_tags.ToList().ForEach(a => attributes.Add(new Attribute { name = a }));
            return attributes;
        }

        public FormalContext ReadContext(string filePath)
        {
            var json = ReadJsonData(filePath);
            dynamic documents = JsonConvert.DeserializeObject(json);

            var attribute_set = new HashSet<string>();
            var objects_d = new Dictionary<string, Item>();

            Dictionary<string, HashSet<string>> objectsHasAttributs = new Dictionary<string, HashSet<string>>();
            Dictionary<string, HashSet<string>> attributsHasObjects = new Dictionary<string, HashSet<string>>();
            foreach (var document in documents)
            {
                // Ignoring objects that don't have tags as attributes
                if (IgnoreObjectsWithoutAttributes && document.tags.Count <= 0)
                    continue;

                // Getting objects
                string id = document.id;
                string name = document.name;
                if(!objects_d.ContainsKey(id))
                    objects_d[id] = new Item() { id = id, name = id };

                objectsHasAttributs[id] = new HashSet<string>();

                // Getting attributes
                foreach (string tag in document.tags)
                {
                    if (!attribute_set.Contains(tag))
                    {
                        attribute_set.Add(tag);
                        attributsHasObjects[tag] = new HashSet<string>();
                    }

                    attributsHasObjects[tag].Add(id);
                    objectsHasAttributs[id].Add(tag);
                }
            }

            //if (document.tags.Count == 0) continue;
            //Item item = new Item() { id = document.id, name = document.name };
            var objects = new List<Item>();
            objects = objects_d.Values.ToList();

            // Attributes
            var attributes = new List<Attribute>();
            attribute_set.ToList().ForEach(a => attributes.Add(new Attribute { name = a }));

            var fcacontext = new FormalContext(attributes, objects, objectsHasAttributs, attributsHasObjects);
            return fcacontext;
        }

        public ICollection<Item> ReadObjects(string filePath)
        {
            throw new NotImplementedException();
        }

        private string ReadJsonData(string filePath)
        {
            var streamReader = new StreamReader(filePath);
            var json = streamReader.ReadToEnd();
            streamReader.Close();
            // Formating file broken json to regular json
            if (json.IndexOf("][") > 0)
            {
                json = json.Replace("][", ",");
            }
            return json;
        }
    }
}
