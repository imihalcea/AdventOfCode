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
        
        [Test]
        public void answer_part1()
        {
            var n = Answer(Part1,(Dataset("18,8,0,5,4,1,20"),2020));
            Check.That(n).IsEqualTo(253);
        }
        
        [Test]
        public void answer_part2()
        {
            var n = Answer(Part1,(Dataset("18,8,0,5,4,1,20"),30000000));
            Check.That(n).IsEqualTo(13710);
        }
        
        
        [TestCase("0,3,6",436)]
        [TestCase("1,3,2",1)]
        [TestCase("2,1,3",10)]
        [TestCase("3,1,2",1836)]
        public void examples_part1(string input, int expected)
        {
            var r = Part1((Dataset(input), 2020));
            Check.That(r).IsEqualTo(expected);
        }
 
    }
}