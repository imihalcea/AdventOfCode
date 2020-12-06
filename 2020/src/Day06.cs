using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NFluent;
using static System.Char;
using static System.Environment;

namespace _2020
{
    public static class Day06
    {

        public static int Part1() => 
            DataSet.Select(grp=>grp.Where(IsLetter).ToHashSet().Count()).Sum();

        public static int Part2() =>
            DataSet.Sum(
                grp => grp.Split(NewLine)
                    .Select(line => line.ToHashSet())
                    .Aggregate((lineA, lineB) => lineA.Intersect(lineB).ToHashSet())
                    .Count
                );


        private static IEnumerable<string> DataSet =
            File.ReadAllText(INPUT_FILE_PATH).Split(NewLine + NewLine);
        
        private const string INPUT_FILE_PATH = "test/day06.txt";
    }
}