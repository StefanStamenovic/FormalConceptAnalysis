using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NextClosureAlgorithm.Util
{
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
            foreach (var document in documents)
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
            //var objectDictionary = new Dictionary<string, Domain.Object>();
            //var newObjects = new HashSet<Domain.Object>();
            //foreach (var obj in objects)
            //{
            //    var object_ = new Domain.Object(obj.name);
            //    newObjects.Add(object_);
            //    objectDictionary[object_.Name] = object_;
            //}
            //var attributeDictionary = new Dictionary<string, Domain.Attribute>();
            //var newAttributes = new HashSet<Domain.Attribute>();
            //foreach (var attribute in attributes)
            //{
            //    var attribute_ = new Domain.Attribute(attribute.name);
            //    newAttributes.Add(attribute_);
            //    attributeDictionary[attribute_.Name] = attribute_;
            //}
            //var newObjectsHasAttributs = new Dictionary<Domain.Object, HashSet<Domain.Attribute>>();
            //foreach (var oha in objectsHasAttributs)
            //{
            //    var oattributes = new HashSet<Domain.Attribute>();
            //    foreach (var attributeName in oha.Value)
            //    {
            //        oattributes.Add(attributeDictionary[attributeName]);
            //    }
            //    newObjectsHasAttributs[objectDictionary[oha.Key]] = oattributes;
            //}
            //var newAttributsHasObjects = new Dictionary<Domain.Attribute, HashSet<Domain.Object>>();
            //foreach (var aho in attributsHasObjects)
            //{
            //    var aobjects = new HashSet<Domain.Object>();
            //    foreach (var objectName in aho.Value)
            //    {
            //        aobjects.Add(objectDictionary[objectName]);
            //    }
            //    newAttributsHasObjects[attributeDictionary[aho.Key]] = aobjects;
            //}

            //var newFCAContext = new Domain.FormalContext(newObjects, newAttributes, newObjectsHasAttributs, newAttributsHasObjects);
            //var algoritham = new Domain.NextClosureAlgorithm(newFCAContext);
            //var formalConcepts = algoritham.FormalConcepts();
            //var lattice = new Domain.ConceptLattice(formalConcepts, newFCAContext);

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
