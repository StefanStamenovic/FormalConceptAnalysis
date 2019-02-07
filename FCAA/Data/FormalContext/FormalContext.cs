using System.Collections.Generic;
using System.Linq;

namespace FCAA.Data
{
    public class FormalContext
    {
        private Object[] _objects { get; set; }
        public Object[] ObjectsArray => _objects;
        public HashSet<Object> ObjectsSet => new HashSet<Object>(_objects);

        private Attribute[] _attributes { get; set; }
        public Attribute[] AttributesArray => _attributes;
        public HashSet<Attribute> AttributesSet => new HashSet<Attribute>(_attributes);

        public Dictionary<Object, HashSet<Attribute>> ObjectAttributes { get; set; }
        public Dictionary<Attribute, HashSet<Object>> AttributeObjects { get; set; }

        public FormalContext(
            IEnumerable<Object> objects,
            IEnumerable<Attribute> attributes,
            Dictionary<Object, HashSet<Attribute>> objectAttributes,
            Dictionary<Attribute, HashSet<Object>> attributeObjects)
        {
            _objects = objects?.ToArray() ?? throw new System.ArgumentNullException(nameof(objects));
            _attributes = attributes?.ToArray() ?? throw new System.ArgumentNullException(nameof(attributes));
            ObjectAttributes = objectAttributes ?? throw new System.ArgumentNullException(nameof(objectAttributes));
            AttributeObjects = attributeObjects ?? throw new System.ArgumentNullException(nameof(attributeObjects));
        }
    }
}
