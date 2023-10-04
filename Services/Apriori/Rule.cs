﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Apriori
{
    public class Rule : IComparable<Rule>
    {
        #region Member Variables

        string[] combination, remaining;
        double confidence;

        #endregion

        #region Constructor

        public Rule(string[] combination, string[] remaining, double confidence)
        {
            this.combination = combination;
            this.remaining = remaining;
            this.confidence = confidence;
        }

        #endregion

        #region Public Properties

        public string[] X { get { return combination; } }

        public string[] Y { get { return remaining; } }

        public double Confidence { get { return confidence; } }

        #endregion

        #region IComparable<clssRules> Members

        public int CompareTo(Rule other)
        {
            return Apriori.TransformToString(X).CompareTo(Apriori.TransformToString(other.X));
        }

        #endregion

        public override int GetHashCode()
        {
            ISorter sorter = new Sorter();
            var XY = new List<string>();
            XY.AddRange(X);
            XY.AddRange(Y);

            string sortedXY = sorter.Sort(Apriori.TransformToString(XY.ToArray()));
            return sortedXY.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var other = obj as Rule;
            if (other == null)
            {
                return false;
            }

            return other.X == this.X && other.Y == this.Y ||
                other.X == this.Y && other.Y == this.X;
        }
    }
}
