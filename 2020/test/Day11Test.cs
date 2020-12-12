using NFluent;
using NUnit.Framework;
using static _2020.test.TestExtensions;
using static _2020.Day11;

namespace _2020.test
{
    [TestFixture]
    public class Day11Test
    {
        private const string INPUT_FILE_PATH_EX = "test/day11ex.txt";
        private const string INPUT_FILE_PATH = "test/day11.txt";
        
        [Test]
        public void answer_part1()
        {
            var n = Answer(Part1,Dataset(INPUT_FILE_PATH));
            Check.That(n).IsEqualTo(2321);
        }
        
        [Test]
        public void answer_part2()
        {
            var n = Answer(Part2,Dataset(INPUT_FILE_PATH));
            //Check.That(n).IsEqualTo(2321);
        }
        
        
        [Test]
        public void examples_part1()
        {
            var n = Answer(Part1,Dataset(INPUT_FILE_PATH_EX));
            Check.That(n).IsEqualTo(37);
        }
        
        [Test]
        public void examples_part2()
        {
            var n = Answer(Part2,Dataset(INPUT_FILE_PATH_EX));
            Check.That(n).IsEqualTo(26);
        }
    }
}