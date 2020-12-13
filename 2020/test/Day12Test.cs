using System;
using System.IO;
using NFluent;
using NUnit.Framework;
using static _2020.test.TestExtensions;
using static _2020.Day12;
using static _2020.Day12.ShipAction;

namespace _2020.test
{
    [TestFixture]
    public class Day12Test
    {
        private const string INPUT_FILE_PATH_EX = "test/day12ex.txt";
        private const string INPUT_FILE_PATH = "test/day12.txt";

        [Test]
        public void answer_part1()
        {
            var n = Answer(Part1,Dataset(INPUT_FILE_PATH));
            Check.That(n).IsEqualTo(845);
        }
        
        [Test]
        public void answer_part2()
        {
            var n = Answer(Part2,Dataset(INPUT_FILE_PATH));
            Check.That(n).IsEqualTo(27016);
        }
        
        [Test]
        public void examples_part1()
        {
            var n = Answer(Part1,Dataset(INPUT_FILE_PATH_EX));
            Check.That(n).IsEqualTo(25);
        }

        [Test]
        public void examples_part2()
        {
            var n = Answer(Part2,Dataset(INPUT_FILE_PATH_EX));
            Check.That(n).IsEqualTo(286);
        }
        
    }
}