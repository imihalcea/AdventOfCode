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
        private const string INPUT_FILE_PATH = "test/day01.txt";

        [Test]
        public void answer_part1()
        {
           var r =  Answer(Part1,INPUT_FILE_PATH);
           Check.That(r).IsEqualTo(1018336);
        }
        
        
        [Test]
        public void answer_part2()
        {
            var r =  Answer(Part2,INPUT_FILE_PATH);
            Check.That(r).IsEqualTo(288756720);
        }
        
        [Test]
        public void answer_part1_combinations()
        {
            var r =  Answer(Part1_comb,INPUT_FILE_PATH);
            Check.That(r).IsEqualTo(1018336);
        }
        
        [Test]
        public void answer_part2_combinations()
        {
            var r = Answer(Part2_comb,INPUT_FILE_PATH);
            Check.That(r).IsEqualTo(288756720);
        }

        
        [TestCase("abcd",2,"ab","ac","ad","bc","bd","cd")]
        [TestCase("abcd",3,"abc","abd","acd","bcd")]
        public void test(string input, int k, params string[] expected)
        {
            var set = input.ToArray().Combinations(k);
            Check.That(set.Select(c=>new string(c))).ContainsExactly(expected);
        }
    }
    
    
}