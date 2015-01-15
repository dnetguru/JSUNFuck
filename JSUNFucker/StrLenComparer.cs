using System.Collections.Generic;

namespace JSUNFucker
{
    public class StrLenComparer : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            if (x.Length == y.Length)
                return 1;
            return y.Length - x.Length;
        }
    }
}