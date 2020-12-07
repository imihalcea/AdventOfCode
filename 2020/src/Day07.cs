using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using _2020.tools;
using static System.Environment;

namespace _2020
{
    public static class Day07
    {
        public static long Part2()
        {
            var edges = DataSet().ToArray();
            var g = new Graph<Bag>(edges);
            var myBag = new Bag("shiny", "gold");
            var myIdx = g.IndexOf(myBag);
            return ComputeCost(myIdx) - 1;

            int ComputeCost(int v)
            {
                var s = 1;
                s += (from n in g.NeighborsOf(v)
                    let w = g.Weight(v, n).GetValueOrDefault(0)
                    select w * ComputeCost(n)).Sum();   
                return s;
            }
        }
       
        public static int Part1()
        {
            var g = new Graph<Bag>(DataSet().ToArray());
            var shiny_gold = new Bag("shiny", "gold");
            var myBag = g.IndexOf(shiny_gold);
            var cnt = 0;
            var sw = Stopwatch.StartNew();
            for (int i = 0; i < g.Vertices.Length; i++)
            {
                g.Dfsi(i,
                    stopCondition:(n) => n == myBag,
                    visitAction:(_, visitedNode, __) =>
                    {
                        if (visitedNode == myBag) cnt += 1;
                    });
            }
            sw.Stop();
            Console.WriteLine($"Dsf: {sw.ElapsedMilliseconds} ms");
            return cnt-1;
        }

        private static IEnumerable<(Bag, Bag, int)> DataSet()
        {
            return File.ReadAllText(INPUT_FILE_PATH)
                .Split(NewLine).SelectMany(ProcessLine);

            IEnumerable<(Bag, Bag, int)> ProcessLine(string line)
            {
                string[] parts = line.Split(' ');
                var b1 = new Bag(parts[0], parts[1]);
                if (int.TryParse(parts[4], out var w))
                {
                    var b2 = new Bag(parts[5], parts[6]);
                    yield return (b1, b2, w);
                }
                var otherBags = line.Split(",");
                for (int i = 1; i < otherBags.Length; i++)
                {
                    var enumerationParts = otherBags[i].Split(' ');
                    var b2 = new Bag(enumerationParts[2], enumerationParts[3]);
                    w = int.Parse(enumerationParts[1]);
                    yield return (b1, b2, w);
                }
            }
        }

        private const string INPUT_FILE_PATH = "test/day07.txt";

        public struct Bag 
        {
            public string Color { get; }
            public string Style { get; }

            public Bag(string style, string color)
            {
                Color = color;
                Style = style;
            }
            public override string ToString()
            {
                return $"{Style} {Color}";
            }
        }
    }
}