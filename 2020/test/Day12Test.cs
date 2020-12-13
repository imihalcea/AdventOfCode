using System;
using MathNet.Numerics.LinearAlgebra;
using NUnit.Framework;

namespace _2020.test
{
    [TestFixture]
    public class Day12Test
    {
        private const string INPUT_FILE_PATH_EX = "test/day12ex.txt";
        private const string INPUT_FILE_PATH = "test/day12.txt";

        
        [Test]
        public void examples_part1()
        {
            /*
            var n = Answer(Part1,Dataset(INPUT_FILE_PATH_EX));
            Check.That(n).IsEqualTo(25);
            */
            var z = Vector<float>.Build.DenseOfArray(new float[] {0f, 0f});
            var l = Vector<float>.Build.DenseOfArray(new float[] {0f, -1f});
            Console.WriteLine($"{Math.Round(Math.Cos(-90))}, {Math.Round(Math.Sin(-90))}");
            Console.WriteLine($"{Math.Round(Math.Cos(-180))}, {Math.Round(Math.Sin(-180))}");
        }
    }
}