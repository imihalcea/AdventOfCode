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