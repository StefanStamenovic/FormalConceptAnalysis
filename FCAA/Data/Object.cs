using System;
using System.Collections.Generic;

namespace FCAA.Data
{
    public class Object
    {
        /// <summary>
        /// Object id. Initially random.
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Object name.
        /// </summary>
        public string Name { get; set; }

        public Object(string name)
        {
            Name = name;
            //Id = Guid.NewGuid().ToString();
        }

        public override bool Equals(object obj)
        {
            var @object = obj as Object;
            return @object != null &&
                   Name == @object.Name;
        }

        public override int GetHashCode()
        {
            return 539060726 + EqualityComparer<string>.Default.GetHashCode(Name);
        }
    }
}
