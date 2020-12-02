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
            private int N1 { get; }
            private int N2 { get; }
            private string Pwd { get; }
            private char PolicyChar { get; }

            public Entry(int n1, int n2, char c, string pwd)
            {
                N1 = n1;
                N2 = n2;
                PolicyChar = c;
                Pwd = pwd;
            }

            public bool IsValid()
            {
                var charCounters = Pwd.GroupBy(c => c).ToDictionary(g => g.Key, g => g.Count());
                return charCounters.ContainsKey(PolicyChar)
                       && charCounters[PolicyChar] >= N1
                       && charCounters[PolicyChar] <= N2;
            }
            
            public bool IsValid2() => 
                (Pwd[N1-1] == PolicyChar) ^ (Pwd[N2-1] == PolicyChar);
        }
    }
}