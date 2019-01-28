using Newtonsoft.Json;
using NextClosureAlgorithm.Util;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NextClosureAlgorithm
{
    /// <summary>
    /// klasa sa funkcijama za operacije nad skupovima, 
    /// i parsiranje formalnog koncepta iz tekstualnog fajla
    /// </summary>
    public static class Appendix
    {
        /// <summary>
        /// provera jednakosti dva skupa atributa
        /// </summary>
        /// <param name="setA">prvi skup</param>
        /// <param name="setB">drugi skup</param>
        /// <returns>true ukoliko su skupovi jednaki, false u suprotnom</returns>
        public static bool SetEquals(this IEnumerable<Attribute> setA, IEnumerable<Attribute> setB)
        {
            var AnotB = setA.Except(setB);
            var BnotA = setB.Except(setA);

            return ((AnotB == null || !AnotB.Any()) && (BnotA == null || !BnotA.Any()));
        }
        /// <summary>
        /// provera jednakosti dva skupa objekata
        /// </summary>
        /// <param name="setA">prvi skup</param>
        /// <param name="setB">drugi skup</param>
        /// <returns>true ukoliko su skupovi jednaki, false u suprotnom</returns>
        public static bool SetEquals(this IEnumerable<Item> setA, IEnumerable<Item> setB)
        {
            var AnotB = setA.Except(setB);
            var BnotA = setB.Except(setA);

            return ((AnotB == null || !AnotB.Any()) && (BnotA == null || !BnotA.Any()));
        }
        /// <summary>
        /// provara da li jedan skup atributa sadrzi drugi
        /// </summary>
        /// <param name="setA">nadskup</param>
        /// <param name="setB">podskup</param>
        /// <returns>true ukoliko je setB podskup skupa setA</returns>
        public static bool Contains(this IEnumerable<Attribute> setA, IEnumerable<Attribute> setB)
        {
            return !setB.Except(setA).Any();
        }
        /// <summary>
        /// provara da li jedan skup objekata sadrzi drugi
        /// </summary>
        /// <param name="setA">nadskup</param>
        /// <param name="setB">podskup</param>
        /// <returns>true ukoliko je setB podskup skupa setA</returns>
        public static bool Contains(this IEnumerable<Item> setA, IEnumerable<Item> setB)
        {
            return !setB.Except(setA).Any();
        }
        /// <summary>
        /// funkcija za parsiranje formalnog konteksta iz tekstualnog fajla
        /// </summary>
        /// <param name="filePath">putanja do tekstualnog fajla sa dokumentima (objekti) i njihovim tagovima(attributes)</param>
        /// <returns>formalni kontekst</returns>
        public static async System.Threading.Tasks.Task<Domain.FormalContext> ParseFormalContextAsync(string filePath)
        {
            //IFCAFileReader reader = new LegacyFCAFileReader();
            //Drugi reader je dodat da bi mogao da parsira iz JSON fajlova o tek
            //reader = new FCAFileReader();

            var reader = new LegacyFCAFileReaderWithPreprocessing();
            //var attributes = reader.ReadAttributes(filePath);
            var context = reader.ReadContext(filePath);
            var objectDictionary = new Dictionary<string, Domain.Object>();
            var newObjects = new HashSet<Domain.Object>();
            foreach (var obj in context.Items)
            {
                var object_ = new Domain.Object(obj.name);
                newObjects.Add(object_);
                objectDictionary[object_.Name] = object_;
            }
            var attributeDictionary = new Dictionary<string, Domain.Attribute>();
            var newAttributes = new HashSet<Domain.Attribute>();
            foreach (var attribute in context.Attributes)
            {
                var attribute_ = new Domain.Attribute(attribute.name);
                newAttributes.Add(attribute_);
                attributeDictionary[attribute_.Name] = attribute_;
            }
            var newObjectsHasAttributs = new Dictionary<Domain.Object, HashSet<Domain.Attribute>>();
            foreach (var oha in context.itemHasAttrs)
            {
                var oattributes = new HashSet<Domain.Attribute>();
                foreach (var attributeName in oha.Value)
                {
                    oattributes.Add(attributeDictionary[attributeName]);
                }
                newObjectsHasAttributs[objectDictionary[oha.Key]] = oattributes;
            }
            var newAttributsHasObjects = new Dictionary<Domain.Attribute, HashSet<Domain.Object>>();
            foreach (var aho in context.attrHasItems)
            {
                var aobjects = new HashSet<Domain.Object>();
                foreach (var objectName in aho.Value)
                {
                    aobjects.Add(objectDictionary[objectName]);
                }
                newAttributsHasObjects[attributeDictionary[aho.Key]] = aobjects;
            }

            var newFCAContext = new Domain.FormalContext(newObjects, newAttributes, newObjectsHasAttributs, newAttributsHasObjects);
            //var context = reader.ReadContextAsync(filePath);
            return newFCAContext;
            var context = reader.ReadContextAsync(filePath);
            return await context;
        }
    }
}
