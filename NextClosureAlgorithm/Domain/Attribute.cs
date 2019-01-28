using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NextClosureAlgorithm.Domain
{
    public class Attribute
    {
        public string Name { get; set; }

        public Attribute(string name)
        {
            Name = name;
        }

        public override bool Equals(object obj)
        {
            var attribute = obj as Attribute;
            return attribute != null &&
                   Name == attribute.Name;
        }

        public override int GetHashCode()
        {
            return 539060726 + EqualityComparer<string>.Default.GetHashCode(Name);
        }
    }
}
