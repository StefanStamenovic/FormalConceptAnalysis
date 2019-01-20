using System.Collections.Generic;
using System.Linq;

namespace NextClosureAlgorithm.Domain
{
    public class FormalConcept
    {
        public FormalContext FormalContext { get; private set; }
        private Object[] _objects { get; set; }
        public HashSet<Object> ObjectsSet { get; private set; }
        public IEnumerable<Object> Objects => _objects;

        private Attribute[] _attributes { get; set; }
        public IEnumerable<Attribute> Attributes => _attributes;


        public FormalConcept(IEnumerable<Object> objects, IEnumerable<Attribute> attributes, FormalContext formalContext)
        {
            _objects = objects.ToArray();
            ObjectsSet = objects as HashSet<Object> ?? new HashSet<Object>(objects);
            _attributes = attributes.ToArray();
            FormalContext = formalContext;
        }
    }
}
