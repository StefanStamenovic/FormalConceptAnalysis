using System.Collections.Generic;
using System.Linq;

namespace NextClosureAlgorithm.Domain
{
    public class FormalConcept
    {
        private Object[] _objects { get; set; }
        private Attribute[] _attributes { get; set; }

        public FormalConcept(IEnumerable<Object> objects, IEnumerable<Attribute> attributes)
        {
            _objects = objects.ToArray();
            _attributes = attributes.ToArray();
        }
    }
}
