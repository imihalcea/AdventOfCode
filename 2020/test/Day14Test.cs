using NFluent;
using NUnit.Framework;
using static _2020.Day14;
using static _2020.test.TestExtensions;

namespace _2020.test
{
    public class Day14Test
    {
        private const string INPUT_FILE_PATH_EX2 = "test/day14ex2.txt";
        private const string INPUT_FILE_PATH_EX = "test/day14ex.txt";
        private const string INPUT_FILE_PATH = "test/day14.txt";

        [Test]
        public void answer_part1()
        {
            var n = Answer(Part1,Dataset(INPUT_FILE_PATH));
            Check.That(n).IsEqualTo(17028179706934);
        }

        [Test]
        public void examples_part1()
        {
            var n = Answer(Part1,Dataset(INPUT_FILE_PATH_EX));
            Check.That(n).IsEqualTo(165);
        }
        
        [Test]
        public void answer_part2()
        {
            var n = Answer(Part2,Dataset(INPUT_FILE_PATH));
            Check.That(n).IsEqualTo(3683236147222);
        }

        [Test]
        public void examples_part2()
        {
            var n = Answer(Part2,Dataset(INPUT_FILE_PATH_EX2));
            Check.That(n).IsEqualTo(208);
        }
        
    }
}