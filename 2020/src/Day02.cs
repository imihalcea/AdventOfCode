using System;
using System.Linq;

namespace _2020
{
    public class Day02
    {
        public static int Part1(Entry[] data) =>
            data.Count(e => e.IsValid());

        public static int Part2(Entry[] data) =>
            data.Count(e => e.IsValid2());

        
        public class Entry
        {
            private int Min { get; }
            private int Max { get; }
            private string Pwd { get; }
            private char PolicyChar { get; }

            public Entry(int min, int max, char c, string pwd)
            {
                Min = min;
                Max = max;
                PolicyChar = c;
                Pwd = pwd;
            }

            public bool IsValid()
            {
                var charCounters = Pwd.GroupBy(c => c).ToDictionary(g => g.Key, g => g.Count());
                return charCounters.ContainsKey(PolicyChar)
                       && charCounters[PolicyChar] >= Min
                       && charCounters[PolicyChar] <= Max;
            }
            
            public bool IsValid2()
            {
                
                return (Pwd[Min-1] == PolicyChar) ^ (Pwd[Max-1] == PolicyChar);

                bool ValidIndex(int idx)
                {
                    return idx < Pwd.Length;
                }

            }
        }
    }
}