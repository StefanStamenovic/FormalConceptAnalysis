using NextClosureAlgorithm;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NextClosureAlgorithm.Util
{
    public interface IFCAFileReader
    {
        FormalContext ReadContext(string filePath);
        ICollection<Attribute> ReadAttributes(string filePath);
        ICollection<Item> ReadObjects(string filePath);
        Task<FormalContext> ReadContextAsync(string filePath);
    }
}
