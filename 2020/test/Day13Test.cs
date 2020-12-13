using NFluent;
using NUnit.Framework;
using static _2020.test.TestExtensions;
using static _2020.Day13;

namespace _2020.test
{
    [TestFixture]
    public class Day13Test
    {
        private const string INPUT_FILE_PATH_EX = "test/day13ex.txt";
        private const string INPUT_FILE_PATH = "test/day13.txt";

        [Test]
        public void answer_part1()
        {
            var n = Answer(Part1,Dataset(INPUT_FILE_PATH));
            Check.That(n).IsEqualTo(4938);
        }

        [Test]
        public void examples_part1()
        {
            var n = Answer(Part1,Dataset(INPUT_FILE_PATH_EX));
            Check.That(n).IsEqualTo(295);
        }
        
        [Test]
        public void answer_part2()
        {
            var n = Answer(Part2,Dataset(INPUT_FILE_PATH));
            Check.That(n).IsEqualTo(230903629977901);
        }

        [Test]
        public void examples_part2()
        {
            var n = Answer(Part2,Dataset(INPUT_FILE_PATH_EX));
            Check.That(n).IsEqualTo(1068781);
        }


        [Test]
        public void chinese_remainder_theorem()
        {
            //https://fr.wikipedia.org/wiki/Th%C3%A9or%C3%A8me_des_restes_chinois

            var r = ChineseRemainderTheorem(new (int mod, int rem)[] {(3, 2), (5, 3), (7, 2)});
            Check.That(r).IsEqualTo(23);
        }
    }
}