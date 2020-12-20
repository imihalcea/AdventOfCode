using System.Collections.Generic;
using System.Linq;
using NFluent;
using NUnit.Framework;
using static _2020.test.TestExtensions;
using static _2020.Day17;

namespace _2020.test
{
    [TestFixture]
    public class Day17Test
    {
        private const string INPUT_FILE_PATH_EX = "test/day17ex.txt";
        private const string INPUT_FILE_PATH = "test/day17.txt";

        [Test]
        public void answer_part1()
        {
            var n = Answer(Part1, 6, Dataset(INPUT_FILE_PATH,3));
            Check.That(n).IsEqualTo(295);
        }
        
        [Test]
        public void examples_part1()
        {
            var n = Answer(Part1,6,Dataset(INPUT_FILE_PATH_EX));
            Check.That(n).IsEqualTo(112);
        }
        
        [Test]
        public void answer_part2()
        {
            var n = Answer(Part1, 6, Dataset(INPUT_FILE_PATH,4));
            Check.That(n).IsEqualTo(1972);
        }
        
        [Test]
        public void examples_part2()
        {
            var n = Answer(Part2,6,Dataset(INPUT_FILE_PATH_EX,4));
            Check.That(n).IsEqualTo(848);
        }

        [Test]
        public void neighbors_3d_test()
        {
            var g = new Game(new HashSet<Cell>());
            var c = new Cell(new int[]{1,1,1});
            var ns = g.NeighborsOf(c).ToArray();
            Check.That(ns.Distinct()).CountIs(26);
            Check.That(ns).Not.Contains(c);
        }
        
        [Test]
        public void neighbors_4d_test()
        {
            var g = new Game(new HashSet<Cell>());
            var c = new Cell(new int[]{1,1,1,1});
            var ns = g.NeighborsOf(c).ToArray();
            Check.That(ns).Not.Contains(c);
            Check.That(ns.Distinct()).CountIs(80);
        }

       
    }
}