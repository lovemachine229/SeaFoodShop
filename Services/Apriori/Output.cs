using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Apriori
{
    public class Output
    {
        #region Public Properties

        public IList<Rule> StrongRules { get; set; }

        public IList<string> MaximalItemSets { get; set; }

        public Dictionary<string, Dictionary<string, double>> ClosedItemSets { get; set; }

        public ItemsDictionary FrequentItems { get; set; }

        public Dictionary<string, object> AllProcess { get; set; }
        #endregion
    }
}
