using System;
using System.IO;
using System.Linq;

namespace _2020
{
    public class Day02
    {
        public static int Part1(string filePath) =>
            DataSet(filePath).Count(it => it.RespectsCountingPolicy());

        public static int Part2(string filePath) =>
            DataSet(filePath).Count(it => it.RespectsPositionPolicy());

        private static Entry[] DataSet(string filePath)
        {
            return File.ReadAllLines(filePath)
                .Select(l=>l.Split(' '))
                .Select(ToEntry)
                .ToArray();
            
            Entry ToEntry(string[] line)
            {
                var min_max = line[0].Split('-').Select(int.Parse).ToArray();
                var c = line[1].First();
                var pw = line.Last();
                return new Entry(min_max[0], min_max[1],c,pw);
            }
        }
        
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

            public bool RespectsCountingPolicy()
            {
                var charCounters = Pwd.GroupBy(c => c).ToDictionary(g => g.Key, g => g.Count());
                return charCounters.ContainsKey(PolicyChar)
                       && charCounters[PolicyChar] >= N1
                       && charCounters[PolicyChar] <= N2;
            }
            
            public bool RespectsPositionPolicy() => 
                (Pwd[N1-1] == PolicyChar) ^ (Pwd[N2-1] == PolicyChar);
        }
    }
}