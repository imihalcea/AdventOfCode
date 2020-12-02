using System;
using System.IO;
using System.Linq;
using _2015.tools;
using NFluent;
using NUnit.Framework;

namespace _2015.test
{
    [TestFixture]
    public class Day12Test
    {

        [Test]
        public void part2_array_case()
        {
            Check.That(Day12.SlicesToIgnore("[1,2,3]", '{', "red", '}')).CountIs(0);
        }

        [TestCase("[1,{\"c\":\"red\",\"b\":2},3]", 3,19)]
        [TestCase("{\"d\":\"red\",\"e\":[1,2,3,4],\"f\":5}", 0,30)]
        [TestCase("{\"d\":\"red\",\"e\":[1,2,3,4],\"f\":5}", 0,30)]
        
        public void part_2_examples(string input, int start, int end)
        {
            Check.That(Day12.SlicesToIgnore(input, '{', "red", '}')).ContainsExactly((start, end));
        }
        
        [Test]
        public void part_2_examples1()
        {
            var input = @"[1,{""c"":""red"",""b"":2},3]";
            var toignore = Day12.SlicesToIgnore(input, '{', "red", '}').ToArray();
            input.SubstringsExcept(toignore).ToList().ForEach(Console.WriteLine);
        }

        [Test]
        public void answer()
        {           

            var answerPar1 = Day12.SumIntegers(DataSet);
            var answerPar2 = Day12.Part2(DataSet);
            Console.WriteLine(answerPar1);
            Console.WriteLine(answerPar2);
        }

        public string DataSet => File.ReadAllText("test/day12.txt");
    }
}