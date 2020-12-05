using NFluent;
using NUnit.Framework;
using static _2020.test.TestExtensions;
using static _2020.Day05;

namespace _2020.test
{
    public class Day05Test
    {

        private const string INPUT_FILE_PATH = "test/day05.txt";

        [Test]
        public void answer_part1()
        {
            Answer(Part1,INPUT_FILE_PATH);
        }
        
        [TestCase("FBFBBFFRLR",357)]
        [TestCase("BFFFBBFRRR",567)]
        [TestCase("FFFBBBFRRR",119)]
        [TestCase("BBFFBBFRLL",820)]
        public void examples(string boardingPass, int expectedId)
        {
            var id = Day05.Decode(boardingPass);
            Check.That(id).IsEqualTo(expectedId);
        }
        
    }
}