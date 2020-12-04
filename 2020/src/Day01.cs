using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;

namespace _2020
{
    public static class Day01
    {
        public static int Part1(string filePath)
        {
            var m = DataSet(filePath)
                .Combinations(2)
                .First(p => p.Sum() == 2020);
            return m[0] * m[1];
        }

        public static int Part2(string filePath)
        {
            var m = DataSet(filePath)
                    .Combinations(3)
                    .First(p => p.Sum() == 2020);
            return m[0] * m[1] * m[2];
        }
        
        private static int[] DataSet(string filePath) =>
            File.ReadAllLines(filePath).Select(int.Parse).ToArray();


        public static IEnumerable<T[]> Combinations<T>(this T[] src, int k)
        {
            var n = src.Length;
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
                        yield return result.Select(i=>src[i]).ToArray();
                        break;
                    }
                }
            }
        }
    }
}