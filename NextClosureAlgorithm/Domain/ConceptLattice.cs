using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextClosureAlgorithm.Domain
{
    public class ConceptLattice
    {
        public IEnumerable<FormalConcept> FormalConcepts { get; private set; }

        public ConceptLattice(IEnumerable<FormalConcept> formalConcepts)
        {
            FormalConcepts = formalConcepts ?? throw new ArgumentNullException(nameof(formalConcepts));
        }

        private void ComputeFormalConceptLevels()
        {
            
        }
    }
}
