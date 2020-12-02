using System;
using System.Linq;
using _2015.tools;
using NFluent;
using NUnit.Framework;
using static _2015.Day10;
namespace _2015.test
{
    [TestFixture]
    public class Day10Test
    {
        [Test]
        public void sequence_generator_test()
        {
            Check.That(LookAndSaySeq("1").Take(7)).ContainsExactly(r("11"), r("21"), r("1211"), r("111221"), r("312211"), r("13112221"), r("1113213211"));
            Check.That(LookAndSaySeq("13112221").Take(1).First()).ContainsExactly(r("1113213211"));
            Check.That(LookAndSaySeq("1113122113").Take(1).First()).ContainsExactly(r("311311222113"));
        }

        [Test]
        public void answer()
        {
            var part1 = LookAndSaySeq("1113122113").Skip(39).Take(1).Select(s => s.Count());
            var part2 = LookAndSaySeq("1113122113").Skip(49).Take(1).Select(s => s.Count());
            Console.WriteLine(part1.First());
            Console.WriteLine(part2.First());
        }

        private int[] r(string s) => 
            s.Select(c=>c-'0').ToArray();
    }
}