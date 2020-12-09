using System.Collections.Generic;
using System.Linq;

namespace _2020
{
    public static class EnumerableExt
    {
        public static IEnumerable<T[]> Combinations<T>(this IEnumerable<T> @this, int k)
        {
            var input = @this.ToArray();
            var n = input.Length;
            var result = new int[k];
            var stack = new Stack<int>();
            stack.Push(0);
 
            while (stack.Count > 0)
            {
                var index = stack.Count - 1;
                var value = stack.Pop();
 
                while (value < n) 
                {
                    result[index++] = value++;
                    stack.Push(value);
 
                    if (index == k) 
                    {
                        yield return result.Select(i=>input[i]).ToArray();
                        break;
                    }
                }
            }
        }

        public static IEnumerable<T> Windowed<T>(this IEnumerable<T> @this, int windowSize)
        {
            var enumerable = @this as T[] ?? @this.ToArray();
            return enumerable.Skip(enumerable.Length - windowSize).Take(windowSize);
        }
            
            
        
    }
}