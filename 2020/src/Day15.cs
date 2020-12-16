using System.Collections.Generic;
using System.Linq;

namespace _2020
{
    public class Day15
    {
        public static int Part1(int[] dataset)
        {
            var spoken = new int[2021];
            spoken[0] = 0;
            var mem = new SortedDictionary<int,List<int>>();
            dataset.CopyTo(spoken,0);
            
            for (var turn = dataset.Length; turn <= 2020; turn++)
            {
                var lastSpoken = spoken[turn -1];
                var previousTurnsOfLastSpoken = mem.GetValueOrDefault(lastSpoken, new List<int>());
                mem[lastSpoken] = previousTurnsOfLastSpoken;
                if (previousTurnsOfLastSpoken.Count==0)
                {
                    spoken[turn] = 0;
                }
                else
                {
                    spoken[turn] = previousTurnsOfLastSpoken.Last() - previousTurnsOfLastSpoken.First();
                }

                var cs = mem.GetValueOrDefault(spoken[turn], new List<int>());
                cs.Add(turn);
                mem[spoken[turn]] = cs;
            }

            return spoken[2020];
        }
        
        
        public static int[] Dataset(string input) => input.Split(",").Select(int.Parse).ToArray();
    }
}