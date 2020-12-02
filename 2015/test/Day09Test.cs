using System;
using System.Linq;
using _2015.tools;
using NUnit.Framework;
using static _2015.tools.Graph.Kind;

namespace _2015.test
{
    [TestFixture]
    public class Day9Test
    {
        [Test]
        public void shortest_path_test()
        {
            var edges = new[]
            {
                ("A", "B", 1),
                ("B", "C", 2),
                ("B", "D", 2),
                ("D", "E", 5),
                ("C", "E", 3),
            };
            var g = new Graph(edges);
            var (pred, dist) = g.Bellman_Ford("A");
            Assert.IsTrue(pred.SequenceEqual(new int?[] {null, 0, 1, 1, 2}));
            Assert.IsTrue(dist.SequenceEqual(new int[] {0, 1, 3, 3, 6}));
        }


        [Test]
        public void answer()
        {
            
            var g = new Graph(DataSet,Undirected); 
            var result1 = g.HamiltonianCycle_BruteForce(src =>
            {
                return src.OrderBy(path => path.weights!.Sum(w => w.GetValueOrDefault(int.MaxValue))).First();
            });

            var result2 = g.HamiltonianCycle_BruteForce(src =>
            {
                return src.OrderByDescending(path => path.weights!.Sum(w => w.GetValueOrDefault(int.MinValue))).First();
            });

          
            Console.WriteLine(result1.weights.Sum());
            Console.WriteLine(result2.weights.Sum());
        }
        
        private (string, string, int)[] DataSet =>
            data.Split(Environment.NewLine)
                .Select(l => l.Split(' '))
                .Select(ps => (ps[0], ps[2], int.Parse(ps[4])))
                .ToArray();
        
        private string data = @"Faerun to Norrath = 129
Faerun to Tristram = 58
Faerun to AlphaCentauri = 13
Faerun to Arbre = 24
Faerun to Snowdin = 60
Faerun to Tambi = 71
Faerun to Straylight = 67
Norrath to Tristram = 142
Norrath to AlphaCentauri = 15
Norrath to Arbre = 135
Norrath to Snowdin = 75
Norrath to Tambi = 82
Norrath to Straylight = 54
Tristram to AlphaCentauri = 118
Tristram to Arbre = 122
Tristram to Snowdin = 103
Tristram to Tambi = 49
Tristram to Straylight = 97
AlphaCentauri to Arbre = 116
AlphaCentauri to Snowdin = 12
AlphaCentauri to Tambi = 18
AlphaCentauri to Straylight = 91
Arbre to Snowdin = 129
Arbre to Tambi = 53
Arbre to Straylight = 40
Snowdin to Tambi = 15
Snowdin to Straylight = 99
Tambi to Straylight = 70";
    }
}