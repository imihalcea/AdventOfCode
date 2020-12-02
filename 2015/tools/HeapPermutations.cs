using System;
using System.Collections.Generic;
using System.Linq;

namespace _2015
{
    public static class HeapPermutations
    {
        /**
         * https://en.wikipedia.org/wiki/Heap%27s_algorithm
         */
        public static IEnumerable<T[]> Permutations<T>(this T[] src)
        {
            var n = src.Length;
            var c = new int[n];

            yield return src;
            var i = 0;
            while (i<n)
            {
                if (c[i] < i)
                {
                    if (i % 2 == 0)
                    {
                        Swap(ref src[0], ref src[i]);
                    }
                    else
                    {
                        Swap(ref src[c[i]], ref src[i]);
                    }

                    yield return src;
                    c[i] += 1;
                    i = 0;
                }
                else
                {
                    c[i] = 0;
                    i += 1;
                }
            }
        } 
        
        static void Swap<T>(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }
    }
}