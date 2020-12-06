using System;
using System.Linq;
using NFluent;
using NUnit.Framework;
using static System.Environment;
using static System.Int32;
using static _2015.Day14;

namespace _2015.test
{
    [TestFixture]
    public class Day14Test
    {
        [Test]
        public void answer_part1()
        {           

            var answer = Part1(Dataset);
            Console.WriteLine(answer);
        }
        
        [Test]
        public void answer_part2()
        {           

            var answer = Part2(Dataset);
            Console.WriteLine(answer);
        }

        [Test]
        public void points_count()
        {
            var r1 = new Reindeer("Comet",14,10,127);
            var r2 = new Reindeer("Dancer",16,11,162);
            var competition = new Competition(new []{r1,r2});
            var result = competition.MaxPoints(1000);
            Check.That(competition.Score(r1)).IsEqualTo(312);
            Check.That(competition.Score(r2)).IsEqualTo(689);
            Check.That(result.winner).IsEqualTo(r2);
        }
        
        [Test]
        public void distance_count()
        {
            var r1 = new Reindeer("Comet",14,10,127);
            var r2 = new Reindeer("Dancer",16,11,162);
            var competition = new Competition(new []{r1,r2});
            var result = competition.MaxDistance(1000);
            Check.That(r1.Distance).IsEqualTo(1120);
            Check.That(r2.Distance).IsEqualTo(1056);
            Check.That(result.winner).IsEqualTo(r1);
        }
        
        
        private static Reindeer[] Dataset => 
            Input.Split(NewLine).Select(l =>
        {
            var arr = l.Split(' ');
            return new Reindeer(arr[0], Parse(arr[3]), Parse(arr[6]), Parse(arr[13]));
        }).ToArray();
        
        
        private static string Input = @"Vixen can fly 19 km/s for 7 seconds, but then must rest for 124 seconds.
Rudolph can fly 3 km/s for 15 seconds, but then must rest for 28 seconds.
Donner can fly 19 km/s for 9 seconds, but then must rest for 164 seconds.
Blitzen can fly 19 km/s for 9 seconds, but then must rest for 158 seconds.
Comet can fly 13 km/s for 7 seconds, but then must rest for 82 seconds.
Cupid can fly 25 km/s for 6 seconds, but then must rest for 145 seconds.
Dasher can fly 14 km/s for 3 seconds, but then must rest for 38 seconds.
Dancer can fly 3 km/s for 16 seconds, but then must rest for 37 seconds.
Prancer can fly 25 km/s for 6 seconds, but then must rest for 143 seconds.";

    }
}