using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FCAA.Data
{
    public class Attribute
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public Attribute(string name)
        {
            Name = name;
            //Id = Guid.NewGuid().ToString(); // Slows down attribute load
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
