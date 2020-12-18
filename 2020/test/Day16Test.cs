using NFluent;
using NUnit.Framework;
using static _2020.Day16;
using static _2020.test.TestExtensions;

namespace _2020.test
{
    public class Day16Test
    {
        private const string INPUT_FILE_PATH_EX = "test/day16ex.txt";
        private const string INPUT_FILE_PATH = "test/day16.txt";

        [Test]
        public void answer_part1()
        {
            var n = Answer(Part1,Dataset(INPUT_FILE_PATH));
            Check.That(n).IsEqualTo(24021);
        }
        
        [Test]
        public void answer_part2()
        {
            var n = Answer(Part2,Dataset(INPUT_FILE_PATH));
            Check.That(n).IsEqualTo(1289178686687);
        }

        [Test]
        public void examples_part1()
        {
            var n = Answer(Part1,Dataset(INPUT_FILE_PATH_EX));
            Check.That(n).IsEqualTo(71);
        }
    }
}