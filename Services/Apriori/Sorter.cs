using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Apriori
{
    public interface ISorter
    {
        string Sort(string token);
    }

    [Export(typeof(ISorter))]
    class Sorter : ISorter
    {
        string ISorter.Sort(string token)
        {
            char[] tokenArray = token.ToCharArray();
            Array.Sort(tokenArray);
            return new string(tokenArray);
        }

        public static int CompareItem(string s1, string s2)
        {
            var intS1 = int.Parse(s1);
            var intS2 = int.Parse(s2);
            return intS1.CompareTo(intS2);
        }
    }
}
