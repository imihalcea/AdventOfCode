using System;
using System.Collections.Generic;
using System.Linq;

namespace _2015.tools
{
    public static class ExtensionsIEnumerable
    {
        public static IEnumerable<U> Pairwise<T,U>(this IEnumerable<T> src, Func<T,T,U> f)
        {
            using var it = src.GetEnumerator();
            
            if(!it.MoveNext())
                yield break;
            
            var prev = it.Current;

            while (it.MoveNext())
            {
                yield return f(prev, it.Current);
                prev = it.Current;
            }
        }

        public static IEnumerable<U> TwoByTwo<T,U>(this IEnumerable<T> src, Func<T,T,U> f)
        {
            
            using var it = src.GetEnumerator();
            
            while (it.MoveNext())
            {
                var i1 = it.Current;
                var i2 = it.MoveNext() ? it.Current : default(T); 
                yield return f(i1,i2);
            }
        }
        
        public static IEnumerable<(K key, IEnumerable<T> values)> GroupConsecutiveRepeats<K,T>(this IEnumerable<T> src, Func<T,K> keySelector)
        {
            using var it = src.GetEnumerator();
            var comparer = EqualityComparer<K>.Default;
            if(!it.MoveNext())
                yield break;

            var prev = keySelector(it.Current);
            var grpStartIndex = 0;
            var takeCounter = 1;
            while (it.MoveNext())
            {
                var current = keySelector(it.Current);
                if (comparer.Equals(prev,current))
                {
                    takeCounter += 1;
                }
                else
                {
                    yield return (prev, src.Skip(grpStartIndex).Take(takeCounter));
                    grpStartIndex = grpStartIndex + takeCounter;
                    prev = current;
                    takeCounter = 1;
                }
            }
            yield return (prev, src.Skip(grpStartIndex).Take(takeCounter));
        }  

    }
}