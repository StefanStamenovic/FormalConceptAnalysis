using Newtonsoft.Json;
using NextClosureAlgorithm.Util;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NextClosureAlgorithm
{
    /// <summary>
    /// klasa sa funkcijama za operacije nad skupovima, 
    /// i parsiranje formalnog koncepta iz tekstualnog fajla
    /// </summary>
    public static class Appendix
    {
        /// <summary>
        /// provera jednakosti dva skupa atributa
        /// </summary>
        /// <param name="setA">prvi skup</param>
        /// <param name="setB">drugi skup</param>
        /// <returns>true ukoliko su skupovi jednaki, false u suprotnom</returns>
        public static bool SetEquals(this IEnumerable<Attribute> setA, IEnumerable<Attribute> setB)
        {
            var AnotB = setA.Except(setB);
            var BnotA = setB.Except(setA);

            return ((AnotB == null || !AnotB.Any()) && (BnotA == null || !BnotA.Any()));
        }
        /// <summary>
        /// provera jednakosti dva skupa objekata
        /// </summary>
        /// <param name="setA">prvi skup</param>
        /// <param name="setB">drugi skup</param>
        /// <returns>true ukoliko su skupovi jednaki, false u suprotnom</returns>
        public static bool SetEquals(this IEnumerable<Item> setA, IEnumerable<Item> setB)
        {
            var AnotB = setA.Except(setB);
            var BnotA = setB.Except(setA);

            return ((AnotB == null || !AnotB.Any()) && (BnotA == null || !BnotA.Any()));
        }
        /// <summary>
        /// provara da li jedan skup atributa sadrzi drugi
        /// </summary>
        /// <param name="setA">nadskup</param>
        /// <param name="setB">podskup</param>
        /// <returns>true ukoliko je setB podskup skupa setA</returns>
        public static bool Contains(this IEnumerable<Attribute> setA, IEnumerable<Attribute> setB)
        {
            return !setB.Except(setA).Any();
        }
        /// <summary>
        /// provara da li jedan skup objekata sadrzi drugi
        /// </summary>
        /// <param name="setA">nadskup</param>
        /// <param name="setB">podskup</param>
        /// <returns>true ukoliko je setB podskup skupa setA</returns>
        public static bool Contains(this IEnumerable<Item> setA, IEnumerable<Item> setB)
        {
            return !setB.Except(setA).Any();
        }
        /// <summary>
        /// funkcija za parsiranje formalnog konteksta iz tekstualnog fajla
        /// </summary>
        /// <param name="filePath">putanja do tekstualnog fajla sa dokumentima (objekti) i njihovim tagovima(attributes)</param>
        /// <returns>formalni kontekst</returns>
        public static FormalContext ParseFormalContext(string filePath)
        {
            IFCAFileReader reader = new LegacyFCAFileReader();
            //Drugi reader je dodat da bi mogao da parsira iz JSON fajlova o tek
            //reader = new FCAFileReader();
            //reader = new LegacyFCAFileReaderWithPreprocessing();
            //var attributes = reader.ReadAttributes(filePath);
            var context = reader.ReadContext(filePath);
            return context;
        }
    }
}
