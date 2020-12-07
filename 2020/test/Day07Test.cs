using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using _2020.tools;
using NUnit.Framework;
using static System.Environment;
using static _2020.test.TestExtensions;


namespace _2020.test
{
    public class Day07Test
    {

        [Test]
        public void answer_part1()
        {
            Answer(Part1);
        }
        
        [Test]
        public void answer_part2()
        {
            Answer(Part2);
        }

        public static int Part2()
        {
            var edges = DataSet().ToArray();
            var g = new Graph<Bag>(edges);
            var myBag = new Bag("shiny","gold");
            var myIdx = Array.IndexOf(g.V, myBag);
            
            var (ps, ws) = g.Dfs(myBag);
            var wss = ws.Where(w=>w<int.MaxValue);
            var predecessors = ps.Where(p=>p!=null).Select(p=>p!.Value).ToArray();
            
            var s = 0;
            foreach (var (p,w) in predecessors.Zip(wss))
            {
                var bag = g.V[p];
                Console.WriteLine($"{bag.Style} {bag.Color}");
                s+= w * edges.Where(e => e.Item1 == bag).Select(e => e.Item3).Sum();
            }

            return s;


        }
        
        public static int Part1()
        {
            var g = new Graph<Bag>(DataSet().ToArray());
            var myBag = new Bag("shiny","gold");
            var myIdx = Array.IndexOf(g.V, myBag);
            var paths = new List<int?[]>();
            foreach (var vertex in g.V.Where(v=>!v.Equals(myBag)))
            {
                
                var (ps, ws) = g.Dfs(vertex);
                paths.Add(ps);
            }

            var a =paths.Count(path => path.Contains(myIdx));
            return a;
        }
        private static IEnumerable<(Bag,Bag,int)> DataSet()
        {
            return File.ReadAllText(INPUT_FILE_PATH)
                .Split(NewLine).SelectMany(ProcessLine);

            IEnumerable<(Bag, Bag, int)> ProcessLine(string line)
            {
                string[] parts = line.Split(' ');
                var b1 = new Bag(parts[0], parts[1]);
                int w = -1;
                try
                {
                    w = parts[4]!="no"? int.Parse(parts[4]):0;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                
                var b2 = w>0? new Bag(parts[5], parts[6]):new Bag("no","no");

                yield return (b1, b2, w);
                var otherBags = line.Split(",");
                for (int i = 1; i < otherBags.Length; i++)
                {
                    var enumerationParts = otherBags[i].Split(' ');
                    b2 = new Bag(enumerationParts[2], enumerationParts[3]);
                    try
                    {
                        w = int.Parse(enumerationParts[1]);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                    yield return (b1, b2, w);
                }
            }
        }
        
        private const string INPUT_FILE_PATH = "test/day07ex.txt";
        
        public class Bag : IEquatable<Bag>
        {
            public string Color { get; }
            public string Style { get; }

            public Bag(string style, string color)
            {
                Color = color;
                Style = style;
            }

            public bool Equals(Bag other)
            {
                return (Color == other.Color && Style == other.Style);// || (Color==other.Style && Style==other.Color);
            }

            public override bool Equals(object? obj)
            {
                return obj is Bag other && Equals(other);
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(Color, Style);
            }

            public static bool operator ==(Bag left, Bag right)
            {
                return left.Equals(right);
            }

            public static bool operator !=(Bag left, Bag right)
            {
                return !left.Equals(right);
            }

            public override string ToString()
            {
                return $"{Style} {Color}";
            }
        }
    }
    
    
}