using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acrelec.SCO.Server
{
    public static class DictionaryExtension
    {
        public static Dictionary<string, List<string>> AddOrUpdate(this Dictionary<string,List<string>> dictionary, string key, string value)
        {
            List<string> val;
            if (dictionary.TryGetValue(key, out val))
            {
                val.Add(value);
                dictionary[key] = val;
            }
            else
            {
                dictionary.Add(key, new List<string> {value});
            }

            return dictionary;
        }
    }
}
