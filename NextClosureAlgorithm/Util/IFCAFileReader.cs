﻿using Newtonsoft.Json;
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
        Task<FormalContext> ReadContextAsync(string filePath);
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

        public Task<FormalContext> ReadContextAsync(string filePath)
        {
            throw new NotImplementedException();
        }
    }

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
                if (!objects_d.ContainsKey(id))
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

            //Test
            //var G = new HashSet<Item>(objects);
            //var M = new HashSet<Attribute>(attributes);
            //var ieA = G.Skip(0).Take(1);
            //var A = new HashSet<Item>(ieA);
            //bool isSuperSet = G.IsSupersetOf(ieA);
            //bool isSubSet = A.IsSubsetOf(G);

            //Dictionary<Attribute, HashSet<Item>> attributeObjects = new Dictionary<Attribute, HashSet<Item>>();
            //foreach (var pair in attributsHasObjects)
            //    attributeObjects[attributes.Find(a => a.name == pair.Key)] = new HashSet<Item>(pair.Value.Select(o => objects_d[o]));

            //var intent = new HashSet<Attribute>();
            //foreach (var attribute in M)
            //{
            //    var attributeObjectsSet = attributeObjects[attribute];
            //    if (A.IsSubsetOf(attributeObjectsSet))
            //        intent.Add(attribute);
            //}
            var objectDictionary = new Dictionary<string, Domain.Object>();
            var newObjects = new HashSet<Domain.Object>();
            foreach (var obj in objects)
            {
                var object_ = new Domain.Object(obj.name);
                newObjects.Add(object_);
                objectDictionary[object_.Name] = object_;
            }
            var attributeDictionary = new Dictionary<string, Domain.Attribute>();
            var newAttributes = new HashSet<Domain.Attribute>();
            foreach (var attribute in attributes)
            {
                var attribute_ = new Domain.Attribute(attribute.name);
                newAttributes.Add(attribute_);
                attributeDictionary[attribute_.Name] = attribute_;
            }
            var newObjectsHasAttributs = new Dictionary<Domain.Object, HashSet<Domain.Attribute>>();
            foreach (var oha in objectsHasAttributs)
            {
                var oattributes = new HashSet<Domain.Attribute>();
                foreach(var attributeName in oha.Value)
                {
                    oattributes.Add(attributeDictionary[attributeName]);
                }
                newObjectsHasAttributs[objectDictionary[oha.Key]] = oattributes;
            }
            var newAttributsHasObjects = new Dictionary<Domain.Attribute, HashSet<Domain.Object>>();
            foreach (var aho in attributsHasObjects)
            {
                var aobjects = new HashSet<Domain.Object>();
                foreach (var objectName in aho.Value)
                {
                    aobjects.Add(objectDictionary[objectName]);
                }
                newAttributsHasObjects[attributeDictionary[aho.Key]] = aobjects;
            }

            var newFCAContext = new Domain.FormalContext(newObjects, newAttributes, newObjectsHasAttributs, newAttributsHasObjects);
            var algoritham = new Domain.NextClosureAlgorithm(newFCAContext);
            var formalConcepts = algoritham.FormalConceptsFromIntents();

            var fcacontext = new FormalContext(attributes, objects, objectsHasAttributs, attributsHasObjects);
            return fcacontext;
        }

        public Task<FormalContext> ReadContextAsync(string filePath)
        {
            throw new NotImplementedException();
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
