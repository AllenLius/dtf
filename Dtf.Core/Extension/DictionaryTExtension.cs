using System.Collections.Generic;

namespace Dtf.Core
{
    public static class DictionaryTExtension
    {
        public static void AddRange<T>(this Dictionary<T, T> dictionaryT, IEnumerable<KeyValuePair<T, T>> items)
        {
            foreach (var item in items)
            {
                dictionaryT.Add(item.Key, item.Value);
            }
        }
    }
}
