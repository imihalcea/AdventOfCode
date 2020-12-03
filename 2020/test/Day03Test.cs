using System.Data;
using System.IO;
using System.Linq;
using NUnit.Framework;
using static _2020.Day03;
using static _2020.test.TestExtensions;

namespace _2020.test
{
    [TestFixture]
    public class Day03Test
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
        
        
        
        public char[][] DataSet => 
            File.ReadAllLines("test/day03.txt")
                .Select(l=>l.ToCharArray()).ToArray();
    }
}