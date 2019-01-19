using System.Collections.Generic;

namespace NextClosureAlgorithm.Domain
{
    public interface IFormalConceptAlgorithm
    {
        IEnumerable<FormalConcept> FormalConcepts();
    }
}
