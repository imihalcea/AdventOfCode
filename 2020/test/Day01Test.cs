using System.IO;
using System.Linq;
using NFluent;
using NUnit.Framework;
using static _2020.Day01;
using static _2020.test.TestExtensions;

namespace _2020.test
{
    [TestFixture]
    public class Day01Test
    {
        [Test]
        public void answer_part1()
        {
            Answer(Part1,DataSet);
        }
        
        [Test]
        public void answer_part2()
        {
            Answer(Part2,DataSet);
        }

        
        [TestCase("abcd",2,"ab","ac","ad","bc","bd","cd")]
        [TestCase("abcd",3,"abc","abd","acd","bcd")]
        public void test(string input, int k, params string[] expected)
        {
            var set = input.ToArray().Combinations(k);
            Check.That(set.Select(c=>new string(c))).ContainsExactly(expected);
        }

        public int[] DataSet => 
            File.ReadAllLines("test/day01.txt")
                .Select(int.Parse).ToArray();
    }
    
    
}