using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics;

namespace _2020
{
    public class Day15
    {
        public static int Part1((int[], int) input)
        {
            var (dataset, n) = input;
            return Generator(dataset, n);
        }

        private static int Generator(int[] input, int limit)
        {
            var mem = input.Select((value,idx)=>(value,idx)).ToDictionary(it=>it.value, it=>it.idx+1);
            var last = input[^1];
            var turn = mem.Count;
            while (turn<limit)
            {
                var next = mem.ContainsKey(last) ? turn - mem[last] : 0;
                mem[last] = turn;
                last = next;
                turn++;
            }
            return last;
        }
        
        public static int[] Dataset(string input) => input.Split(",").Select(int.Parse).ToArray();
    }
}