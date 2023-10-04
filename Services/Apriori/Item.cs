using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Services.Apriori
{
    public class Item : IComparable<Item>
    {
        #region Public Properties
        public string Name { get; set; }
        public double Support { get; set; }
        #endregion

        #region IComparable
        public int CompareTo(Item other)
        {
            return SafeNativeMethods.StrCmpLogicalW(Name, other.Name);
        }
        [DllImport("shlwapi.dll", CharSet = CharSet.Unicode)]
        private static extern int StrCmpLogicalW(string psz1, string psz2);
        #endregion
    }

    [SuppressUnmanagedCodeSecurity]
    internal static class SafeNativeMethods
    {
        [DllImport("shlwapi.dll", CharSet = CharSet.Unicode)]
        public static extern int StrCmpLogicalW(string psz1, string psz2);
    }
}
