using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NextClosureAlgorithm.Util
{
    public class LegacyFCAFileReaderWithPreprocessing
    {
        public ICollection<Attribute> ReadAttributes(string filePath)
        {
            throw new NotImplementedException();
        }
        public ICollection<Item> ReadObjects(string filePath)
        {
            throw new NotImplementedException();
        }
        public async Task<Domain.FormalContext> ReadContextAsync(string filePath)
        {
            FilePreprocessingManager preprocessingManager = new FilePreprocessingManager() { Treshold = 0.2};

            HashSet<string> allAttributes = new HashSet<string>();
            List<Domain.Object> objects = new List<Domain.Object>();
            List<Domain.Attribute> attributes = new List<Domain.Attribute>();
            Dictionary<Domain.Object, HashSet<Domain.Attribute>> objectAttributes = new Dictionary<Domain.Object, HashSet<Domain.Attribute>>();
            Dictionary<Domain.Attribute, HashSet<Domain.Object>> attributeObjects = new Dictionary<Domain.Attribute, HashSet<Domain.Object>>();

            List<Document> documents = await preprocessingManager.PreprocessFileAsync(filePath);

            foreach (var document in documents)
            {
                if (document.tags.Count == 0) continue;
                Domain.Object obj = new Domain.Object(document.name);
                objects.Add(obj);

                objectAttributes.Add(obj, new HashSet<Domain.Attribute>());
                allAttributes.UnionWith(document.tags);
                foreach (var tag in document.tags)
                {
                    Domain.Attribute attr = new Domain.Attribute(tag);

                    if (!(attributes.Any(a => a.Name == tag)))
                    {
                        attributes.Add(attr);
                        attributeObjects.Add(attr, new HashSet<Domain.Object>());
                    }

                    attributeObjects[attr].Add(obj);
                    objectAttributes[obj].Add(attr);
                }
            }
            return new Domain.FormalContext(objects, attributes, objectAttributes, attributeObjects);
        }
    }
}
