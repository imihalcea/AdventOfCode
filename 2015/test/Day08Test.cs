using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;

namespace _2015.test
{
    [TestFixture]
    public class Day8Test
    {
        
        
        [Test]
        public void answer()
        {
            var answerPart1 = Day8.Part1(DataSet1);
            var answerPart2 = Day8.Part2(DataSet1);
            Console.WriteLine(answerPart1);
            Console.WriteLine(answerPart2);
        }


        private readonly IEnumerable<string> DataSet1 = File.ReadAllLines("test/day8.txt");
        private readonly byte[] DataSet = File.ReadAllBytes("test/day8.txt");
    }
}