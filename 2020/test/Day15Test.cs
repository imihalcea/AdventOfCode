using System.Collections.Generic;
using System.Linq;
using NFluent;
using NUnit.Framework;
using static _2020.Day15;
using static _2020.test.TestExtensions;

namespace _2020.test
{
    [TestFixture]
    public class Day15Test
    {
        [TestCase("0,3,6",436)]
        public void examples_part1(string input, int expected)
        {
            var r = Part1(Dataset(input));
            Check.That(r).IsEqualTo(expected);
        }
 
    }
}