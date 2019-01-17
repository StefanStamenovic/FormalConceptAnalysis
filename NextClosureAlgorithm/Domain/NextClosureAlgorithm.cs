using System.Collections.Generic;
using System.Linq;

namespace NextClosureAlgorithm.Domain
{
    public class NextClosureAlgorithm : IFormalConceptAlgorithm
    {
        public FormalContext FormalContext { get; private set; }

        public Dictionary<Attribute, int> AttributeLexicalPosition { get; private set; }

        public NextClosureAlgorithm(FormalContext formalContext)
        {
            FormalContext = formalContext;

            AttributeLexicalPosition = new Dictionary<Attribute, int>();
            var attributes = FormalContext.AttributesArray;
            for (int i = 0; i < attributes.Length; i++)
                AttributeLexicalPosition[attributes[i]] = i;
        }

        public HashSet<Object> Extent(IEnumerable<Attribute> attributes)
        {
            var attributeSubSet = attributes as HashSet<Attribute> ?? new HashSet<Attribute>(attributes);

            // Extent of empty attribute set is the whole object set
            if (!attributeSubSet.Any())
                return FormalContext.ObjectsSet;

            // Computing extents of single attributes
            if (attributeSubSet.Count == 1)
            {
                var attributeObjects = FormalContext.AttributeObjects[attributeSubSet.First()];
                return new HashSet<Object>(attributeObjects);
            }

            var extent = new HashSet<Object>();
            // Computing extents when passed attributes set have more than one attribute
            foreach (var obj in FormalContext.ObjectsSet)
            {
                var objectAttributes = FormalContext.ObjectAttributes[obj];
                if (attributeSubSet.IsSubsetOf(objectAttributes))
                    extent.Add(obj);
            }

            return extent;
        }
        /// <summary>
        /// Intents the specified objects.
        /// </summary>
        /// <param name="objects">The objects.</param>
        /// <returns></returns>
        public IEnumerable<Attribute> Intent(IEnumerable<Object> objects)
        {
            // Used for hash set operations
            var objectSubSet = objects as HashSet<Object> ?? new HashSet<Object>(objects);

            // Intent of empty object set is the whole attribute set
            if (!objectSubSet.Any())
                return new HashSet<Attribute>(FormalContext.AttributesSet);

            // Computing intents of single objects
            if(objectSubSet.Count == 1)
            {
                var objectAttributes = FormalContext.ObjectAttributes[objectSubSet.First()];
                return new HashSet<Attribute>(objectAttributes);
            }

            var intent = new HashSet<Attribute>();
            // Computing intents when passed objects set have more than one object
            foreach (var attribute in FormalContext.AttributesSet)
            {
                var attributeObjects = FormalContext.AttributeObjects[attribute];
                if (objectSubSet.IsSubsetOf(attributeObjects))
                    intent.Add(attribute);
            }
            return intent;
        }

        /// <summary>
        /// Closures of specified objects.
        /// </summary>
        /// <param name="objects">The objects.</param>
        /// <returns></returns>
        public IEnumerable<Object> Closure(IEnumerable<Object> objects)
        {
            var closure = Extent(Intent(objects));
            return closure;
        }
        /// <summary>
        /// Closures of specified attributes.
        /// </summary>
        /// <param name="attributes">The attributes.</param>
        /// <returns></returns>
        public IEnumerable<Attribute> Closure(IEnumerable<Attribute> attributes)
        {
            var closure = Intent(Extent(attributes));
            return closure;
        }

        public IEnumerable<IEnumerable<Object>> Extents()
        {
            return null;
        }
        public IEnumerable<IEnumerable<Attribute>> Intents()
        {
            var intents = NextIntent();
            return intents;
        }

        public IEnumerable<IEnumerable<Object>> NextExtent()
        {
            return null;
        }

        public IEnumerable<IEnumerable<Attribute>> NextIntent()
        {
            /// Set M is set of all attributes
            var setM = FormalContext.AttributesSet;

            // A = FirstClousure(") = ∅"
            var setA = FirstClousure();
            var intents = new List<IEnumerable<Attribute>>();
            while (setA != null)
            {
                // Output A
                intents.Add(setA);
                setA = NextClousure(setA, setM);
            }
            return intents;
        }
        /// <summary>
        /// Find first closed set that is closure of the empty set.
        /// </summary>
        /// <returns></returns>
        private IEnumerable<Attribute> FirstClousure()
        {
            return Closure(new HashSet<Attribute>());
        }
        /// <summary>
        /// Finds next smallest closed set larger than given set A ⊂ M 
        /// </summary>
        /// <param name="A">Set A.</param>
        /// <param name="M">Set M.</param>
        /// <returns>Null when algoritham is finished.</returns>
        private IEnumerable<Attribute> NextClousure(IEnumerable<Attribute> A, IEnumerable<Attribute> M)
        {
            // Coping set A for modification
            var _A = new HashSet<Attribute>(A);

            /// Forech loop provides lecticlly ordered where mi is largest element of M
            /// that is enabled by reverse ordering set M.
            foreach (var m in M.Reverse())
            {
                
                /// This part in foreach loop calculates A ⊕ mi = ((A ∩ {m1, . . . ,mi−1}) ∪ {mi})"
                /// and finds the next smallest closed set larger than given set A ⊂ M with respect
                /// to the lectic order where mi being the largest element of M

                /// Provides the (A ∩ {m1, . . . ,mi−1}) part in set A for the given mi
                /// by removing all elements grater than mi-1 from set A.

                /// if m ∈ A
                if (_A.Contains(m))
                {
                    // A = A\{m}
                    _A.Remove(m);
                }
                else
                {
                    /// Part where we add union with mi and form (A ∩ { m1, . . . ,mi−1}) ∪ { mi})
                    var AUm = new HashSet<Attribute>(_A);
                    AUm.Add(m);

                    /// B = A ⊕ mi = ((A ∩ {m1, . . . ,mi−1}) ∪ {mi})"
                    var setB = Closure(AUm);

                    // B\A min element
                    var BeA = setB.Except(_A);

                    // Part where we check is B next smallest closure A <i A ⊕ mi

                    // if B\A contains no elements < m, that is meet when min elment is not < m
                    // when B\A is empty set there is no min element and is < m
                    if (BeA.Any() && !(BeA.Min(a => AttributeLexicalPosition[a]) < AttributeLexicalPosition[m]))
                        return setB;
                }
            }
            /// Null indicates that alghoritham is finished
            return null;
        }

        public IEnumerable<FormalConcept> FormalConceptsFromIntents()
        {
            var intents = Intents();
            var formalConcepts = FormalConcepts(intents);
            return formalConcepts;
        }
        public IEnumerable<FormalConcept> FormalConcepts(IEnumerable<IEnumerable<Attribute>> intents)
        {
            var formalConcepts = new HashSet<FormalConcept>();
            foreach (var intent in intents)
            {
                var formalConcept = new FormalConcept(Extent(intent), intent);
                formalConcepts.Add(formalConcept);
            }
            return formalConcepts;
        }

        //public ICollection<FormalConcept> FormalConceptsFromExtents()
        //{
        //    var extents = Extents();
        //    var formalConcepts = FormalConcepts(extents);
        //    return formalConcepts;
        //}

        //public ICollection<FormalConcept> FormalConcepts(IEnumerable<IEnumerable<Object>> extents)
        //{
        //    var formalConcepts = new HashSet<FormalConcept>();
        //    foreach (var extent in extents)
        //    {
        //        var formalConcept = new FormalConcept();
        //        formalConcepts.Add(formalConcept);
        //    }
        //    return formalConcepts;
        //}
    }
}
