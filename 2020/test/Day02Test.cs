using System.IO;
using System.Linq;
using NUnit.Framework;
using static _2020.Day02;
using static _2020.test.TestExtensions;
namespace _2020.test
{
    [TestFixture]
    public class Day02Test
    {
        private const string INPUT_FILE_PATH = "test/day02.txt";

        [Test]
        public void answer_part1()
        {
            Answer(Part1,INPUT_FILE_PATH);
        }
        
        [Test]
        public void answer_part2()
        {
            Answer(Part2,INPUT_FILE_PATH);
        }
    }
}