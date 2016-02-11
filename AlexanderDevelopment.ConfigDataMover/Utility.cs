using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlexanderDevelopment.ConfigDataMover
{
    public static class Utility
    {
        public static Dictionary<string, string> ParseConnectionString(string connectionstring)
        {
            var dict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            if (!string.IsNullOrWhiteSpace(connectionstring))
            {
                var elements = connectionstring.Split(';');
                foreach (var element in elements)
                {
                    string trimelement = element.Trim();
                    if (!string.IsNullOrWhiteSpace(trimelement))
                    {
                        if (trimelement.Contains("="))
                        {
                            var kvp = trimelement.Split("=".ToCharArray());
                            dict.Add(kvp[0], kvp[1]);
                        }
                    }
                }
            }
            else
            {
                throw new Exception("Connection string cannot be null or empty");
            }
            return dict;
        }
    }
}
