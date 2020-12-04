using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _2020
{
    public class Day03
    {
        public static int Part1(string filePath) => 
            CountTrees(DataSet(filePath), (3, 1));

        public static long Part2(string filePath) =>
           new[] {(1,1),(3,1),(5,1),(7,1),(1,2)}
               .Select(slope => CountTrees(DataSet(filePath), slope))
               .Aggregate(1L,(acc,n)=>acc*n);

        private static  char[][] DataSet(string filePath) => 
            File.ReadAllLines(filePath)
                .Select(l=>l.ToCharArray()).ToArray();
        
       private static int CountTrees(char[][] input, (int, int) slope)
        {
            var inputSize = (input[0].Length, input.Length);
            return PointsOnSlope(slope, inputSize).Count(p => input[p.y][p.x] == '#');
        }
       
        private static IEnumerable<(int x, int y)> PointsOnSlope((int x, int y) slope, (int mx, int my) inputSize)
        {
            var (xMax, yMax) = inputSize;
            var (sx, sy) = slope;
            var (px, py) = (sx, sy);
            while (py < yMax)
            {
                yield return (px % xMax, py);
                (px, py) = (px + sx, py + sy);
            }
        }
    }
}