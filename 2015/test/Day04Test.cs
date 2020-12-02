using System;
using System.Text;
using NUnit.Framework;

namespace _2015.test
{
    [TestFixture]
    public class Day4Test
    {
        [TestCase("abcdef",609043)]
        [TestCase("pqrstuv",1048970)]
        public void examples_part1(string input, int expected)
        {
            var result = Day4.Compute(input, "00000");
            Assert.AreEqual(expected,result);
        }

        [Test]
        public void answer()
        {
           var resultPart1 = Day4.Compute("yzbqklnj", "00000");
           var resultPart2 = Day4.Compute("yzbqklnj", "000000");
           Console.WriteLine(resultPart1);
           Console.WriteLine(resultPart2);
        }
    }
}