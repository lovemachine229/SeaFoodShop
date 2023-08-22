using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Apriori
{
    public interface IApriori
    {
        Output ProcessTransaction(double minSupport, double minConfidence, IEnumerable<string> items, string[] transactions);
        List<string> ProcessInputRule(List<string> productIds);
    }

    [Export(typeof(IApriori))]
    public class Apriori : IApriori
    {
        #region Member Variables

        readonly ISorter _sorter;

        #endregion

        #region Constructor

        public Apriori()
        {
            _sorter = ContainerProvider.Container.GetExportedValue<ISorter>();
        }

        #endregion

        #region IApriori

        public Output ProcessTransaction(double minSupport, double minConfidence, IEnumerable<string> items, string[] transactions)
        {
            try
            {
                Dictionary<string, object> allProcess = new Dictionary<string, object>();
                IList<Item> frequentItems = GetL1FrequentItems(minSupport, items, transactions);
                allProcess.Add("L1", frequentItems);

                ItemsDictionary allFrequentItems = new ItemsDictionary();
                allFrequentItems.ConcatItems(frequentItems);
                IDictionary<string, double> candidates;
                double transactionsCount = transactions.Count();

                int step = 2;
                do
                {
                    candidates = GenerateCandidates(frequentItems, transactions);
                    allProcess.Add($"C{step}", candidates);
                    frequentItems = GetFrequentItems(candidates, minSupport, transactionsCount);
                    allProcess.Add($"L{step}", frequentItems);
                    allFrequentItems.ConcatItems(frequentItems);
                    step += 1;
                }
                while (candidates.Count != 0);

                HashSet<Rule> rules = GenerateRules(allFrequentItems);
                IList<Rule> strongRules = GetStrongRules(minConfidence, rules, allFrequentItems);
                Dictionary<string, Dictionary<string, double>> closedItemSets = GetClosedItemSets(allFrequentItems);
                IList<string> maximalItemSets = GetMaximalItemSets(closedItemSets);

                return new Output
                {
                    StrongRules = strongRules,
                    MaximalItemSets = maximalItemSets,
                    ClosedItemSets = closedItemSets,
                    FrequentItems = allFrequentItems,
                    AllProcess = allProcess
                };
            }
            catch (Exception ex)
            {
                return null;
            }




        }

        public List<string> ProcessInputRule(List<string> productIds)
        {
            List<string> allInputRules = new List<string>();
            List<string> inputRules = productIds;
            do
            {
                allInputRules.AddRange(inputRules);
                inputRules = GenerateInputRule(inputRules);
            }
            while (inputRules.Count != 0);

            return allInputRules.Select(_ => String.Join(",", TransformToArray(_))).ToList();
        }

        #endregion

        #region Private Methods

        private List<Item> GetL1FrequentItems(double minSupport, IEnumerable<string> items, IEnumerable<string> transactions)
        {
            var frequentItemsL1 = new List<Item>();

            foreach (var item in items)
            {
                double support = GetSupport(item, transactions);

                if (support >= minSupport)
                {
                    frequentItemsL1.Add(new Item { Name = item, Support = support });
                }
            }
            frequentItemsL1.Sort();
            return frequentItemsL1;
        }

        private double GetSupport(string generatedCandidate, IEnumerable<string> transactionsList)
        {
            double support = 0;

            foreach (string transaction in transactionsList)
            {
                if (CheckIsSubset(generatedCandidate, transaction))
                {
                    support++;
                }
            }

            return support;
        }

        private bool CheckIsSubset(string child, string parent)
        {
            if (!parent.Contains(child))
            {
                return false;
            }

            return true;
        }

        public static string[] TransformToArray(string candidate)
        {
            return candidate.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
        }

        public static string TransformToString(string[] array)
        {
            return $",{String.Join(",", array)},";
        }

        private string SortCandidate(string candidate)
        {
            var array = TransformToArray(candidate);
            Array.Sort(array, CompareItem);
            return TransformToString(array);
        }

        private Dictionary<string, double> GenerateCandidates(IList<Item> frequentItems, IEnumerable<string> transactions)
        {
            Dictionary<string, double> candidates = new Dictionary<string, double>();

            for (int i = 0; i < frequentItems.Count - 1; i++)
            {
                string firstItem = SortCandidate(frequentItems[i].Name);

                for (int j = i + 1; j < frequentItems.Count; j++)
                {
                    string secondItem = SortCandidate(frequentItems[j].Name);
                    string generatedCandidate = GenerateCandidate(firstItem, secondItem);

                    if (generatedCandidate != string.Empty)
                    {
                        double support = GetSupport(generatedCandidate, transactions);
                        candidates.Add(generatedCandidate, support);
                    }
                }
            }

            return candidates;
        }

        private List<string> GenerateInputRule(List<string> productIds)
        {
            List<string> inputRules = new List<string>();

            for (int i = 0; i < productIds.Count - 1; i++)
            {
                string firstItem = SortCandidate(productIds[i]);

                for (int j = i + 1; j < productIds.Count; j++)
                {
                    string secondItem = SortCandidate(productIds[j]);
                    string generatedInputRule = GenerateCandidate(firstItem, secondItem);

                    if (generatedInputRule != string.Empty)
                    {
                        inputRules.Add(generatedInputRule);
                    }
                }
            }

            return inputRules;
        }

        private string GenerateCandidate(string firstItem, string secondItem)
        {
            string newCandidate;

            try
            {

                List<string> newArray = new List<string>();

                var firstArray = TransformToArray(firstItem);
                var secondArray = TransformToArray(secondItem);

                int length = firstItem.Length;

                if (firstArray.Length == 1)
                {
                    newArray.AddRange(firstArray);
                    newArray.AddRange(secondArray);
                    newCandidate = TransformToString(newArray.ToArray());
                }
                else
                {
                    string firstSubString = firstItem.Substring(0, length - 1);
                    string secondSubString = secondItem.Substring(0, Math.Min(length - 1, secondItem.Length));

                    if (firstSubString == secondSubString)
                    {
                        newCandidate = firstItem + secondItem[length - 1];
                        return newCandidate;
                    }

                    newCandidate = string.Empty;
                }
            }
            catch (Exception ex)
            {

                throw;
            }


            return newCandidate;
        }

        private List<Item> GetFrequentItems(IDictionary<string, double> candidates, double minSupport, double transactionsCount)
        {
            var frequentItems = new List<Item>();

            foreach (var item in candidates)
            {
                if (item.Value >= minSupport)
                {
                    frequentItems.Add(new Item { Name = item.Key, Support = item.Value });
                }
            }

            return frequentItems;
        }

        private Dictionary<string, Dictionary<string, double>> GetClosedItemSets(ItemsDictionary allFrequentItems)
        {
            var closedItemSets = new Dictionary<string, Dictionary<string, double>>();
            int i = 0;

            foreach (var item in allFrequentItems)
            {
                Dictionary<string, double> parents = GetItemParents(item.Name, ++i, allFrequentItems);

                if (CheckIsClosed(item.Name, parents, allFrequentItems))
                {
                    closedItemSets.Add(item.Name, parents);
                }
            }

            return closedItemSets;
        }

        private Dictionary<string, double> GetItemParents(string child, int index, ItemsDictionary allFrequentItems)
        {
            var parents = new Dictionary<string, double>();

            for (int j = index; j < allFrequentItems.Count; j++)
            {
                string parent = allFrequentItems[j].Name;

                if (parent.Length == child.Length + 1)
                {
                    if (CheckIsSubset(child, parent))
                    {
                        parents.Add(parent, allFrequentItems[parent].Support);
                    }
                }
            }

            return parents;
        }

        private bool CheckIsClosed(string child, Dictionary<string, double> parents, ItemsDictionary allFrequentItems)
        {
            foreach (string parent in parents.Keys)
            {
                if (allFrequentItems[child].Support == allFrequentItems[parent].Support)
                {
                    return false;
                }
            }

            return true;
        }

        private IList<string> GetMaximalItemSets(Dictionary<string, Dictionary<string, double>> closedItemSets)
        {
            var maximalItemSets = new List<string>();

            foreach (var item in closedItemSets)
            {
                Dictionary<string, double> parents = item.Value;

                if (parents.Count == 0)
                {
                    maximalItemSets.Add(item.Key);
                }
            }

            return maximalItemSets;
        }

        private HashSet<Rule> GenerateRules(ItemsDictionary allFrequentItems)
        {
            var rulesList = new HashSet<Rule>();

            foreach (var item in allFrequentItems)
            {
                var itemArray = TransformToArray(item.Name);
                if (itemArray.Length > 1)
                {
                    IEnumerable<string[]> subsetsList = GenerateSubsets(item.Name, itemArray);

                    foreach (var subset in subsetsList)
                    {
                        string[] remaining = GetRemaining(subset, itemArray);
                        Rule rule = new Rule(subset, remaining, 0);

                        if (!rulesList.Contains(rule))
                        {
                            rulesList.Add(rule);
                        }
                    }
                }
            }

            return rulesList;
        }

        private IEnumerable<string[]> GenerateSubsets(string item, string[] itemArray)
        {
            IEnumerable<string[]> allSubsets = new List<string[]> { };
            int level = itemArray.Length / 2;

            for (int i = 1; i <= level; i++)
            {
                IList<string[]> subsets = new List<string[]>();
                GenerateSubsetsRecursive(item, itemArray, i, new string[itemArray.Length], subsets);
                allSubsets = allSubsets.Concat(subsets);
            }

            return allSubsets;
        }

        private void GenerateSubsetsRecursive(string item, string[] itemArray, int level, string[] temp, IList<string[]> subsets, int q = 0, int r = 0)
        {
            if (q == level)
            {
                List<string> sb = new List<string>();

                for (int i = 0; i < level; i++)
                {
                    sb.Add(temp[i]);
                }

                subsets.Add(sb.ToArray());
            }

            else
            {
                for (int i = r; i < itemArray.Length; i++)
                {
                    temp[q] = itemArray[i];
                    GenerateSubsetsRecursive(item, itemArray, level, temp, subsets, q + 1, i + 1);
                }
            }
        }

        private string[] GetRemaining(string[] child, string[] parent)
        {
            string[] remaining = parent.Where(x => !child.Contains(x)).ToArray();
            return remaining;
        }

        private IList<Rule> GetStrongRules(double minConfidence, HashSet<Rule> rules, ItemsDictionary allFrequentItems)
        {
            var strongRules = new List<Rule>();

            foreach (Rule rule in rules)
            {
                AddStrongRule(rule, strongRules, minConfidence, allFrequentItems);
            }

            strongRules.Sort();
            return strongRules;
        }

        private void AddStrongRule(Rule rule, List<Rule> strongRules, double minConfidence, ItemsDictionary allFrequentItems)
        {
            try
            {
                List<string> xy = new List<string>();
                xy.AddRange(rule.X);
                xy.AddRange(rule.Y);
                var xyArray = xy.ToArray();

                Array.Sort(rule.X, CompareItem);
                Array.Sort(rule.Y, CompareItem);
                Array.Sort(xyArray, CompareItem);

                double confidence = GetConfidence(rule.X, xyArray, allFrequentItems);

                if (confidence >= minConfidence)
                {
                    Rule newRule = new Rule(rule.X, rule.Y, confidence);
                    strongRules.Add(newRule);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private double GetConfidence(string[] X, string[] XY, ItemsDictionary allFrequentItems)
        {
            double supportX = allFrequentItems[TransformToString(X)].Support;
            double supportXY = allFrequentItems[TransformToString(XY)].Support;
            return supportXY / supportX;
        }

        public static int CompareItem(string s1, string s2)
        {
            var intS1 = int.Parse(s1);
            var intS2 = int.Parse(s2);
            return intS1.CompareTo(intS2);
        }
        #endregion
    }
}
