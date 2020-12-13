using System;
using System.IO;
using NFluent;
using NUnit.Framework;
using static _2020.test.TestExtensions;
using static _2020.Day12;
using static _2020.Day12.ShipAction;

namespace _2020.test
{
    [TestFixture]
    public class Day12Test
    {
        private const string INPUT_FILE_PATH_EX = "test/day12ex.txt";
        private const string INPUT_FILE_PATH = "test/day12.txt";

        [Test]
        public void answer_part1()
        {
            var n = Answer(Part1,Dataset(INPUT_FILE_PATH));
            Check.That(n).IsEqualTo(845);
        }
        
        /*[Test]
        public void answer_part2()
        {
            var n = Answer(Part2,Dataset(INPUT_FILE_PATH));
            Check.That(n).IsEqualTo(2397);
        }*/
        
        [Test]
        public void examples_part1()
        {
            var n = Answer(Part1,Dataset(INPUT_FILE_PATH_EX));
            Check.That(n).IsEqualTo(25);
        }

        /*[Test]
        public void examples_part2()
        {
            var n = Answer(Part2,Dataset(INPUT_FILE_PATH_EX));
            Check.That(n).IsEqualTo(286);
        }*/

        
        [TestCase(E,R,90,S)]
        [TestCase(E,R,180,W)]
        [TestCase(E,R,270,N)]
        [TestCase(E,L,90,N)]
        [TestCase(E,L,180,W)]
        [TestCase(E,L,270,S)]
        [TestCase(W,R,90,N)]
        [TestCase(W,R,180,E)]
        [TestCase(W,R,270,S)]
        [TestCase(W,L,90,S)]
        [TestCase(W,L,180,E)]
        [TestCase(W,L,270,N)]
        [TestCase(N,R,90,E)]
        [TestCase(N,R,180,S)]
        [TestCase(N,R,270,W)]
        [TestCase(N,L,90,W)]
        [TestCase(N,L,180,S)]
        [TestCase(N,L,270,E)]
        [TestCase(S,R,90,W)]
        [TestCase(S,R,180,N)]
        [TestCase(S,R,270,E)]
        [TestCase(S,L,90,E)]
        [TestCase(S,L,180,N)]
        [TestCase(S,L,270,W)]
        public void turn_tests(ShipAction initialFacing, ShipAction action, int value, ShipAction expected)
        {
            var ship = new Ship((0,0),initialFacing);
            var r  = Rotate(action, value, ship);
            Check.That(r.dir).IsEqualTo(expected);

        }
    }
}