using System.IO;
using System.Linq;
using NUnit.Framework;
using static _2020.Day02;
using static _2020.test.TestExtensions;
namespace _2020.test
{
    [TestFixture]
    public class Day02Test
    {

        [Test]
        public void answer_part1()
        {
            Answer(Part1,DataSet());
        }
        
        [Test]
        public void answer_part2()
        {
            Answer(Part2,DataSet());
        }
        public Entry[] DataSet()
        {
            return File.ReadAllLines("test/day02.txt")
                        .Select(l=>l.Split(' '))
                        .Select(ToEntry)
                        .ToArray();
            
            Entry ToEntry(string[] line)
            {
                var min_max = line[0].Split('-').Select(int.Parse).ToArray();
                var c = line[1].First();
                var pw = line.Last();
                return new Entry(min_max[0], min_max[1],c,pw);
            }
        }
    }
}