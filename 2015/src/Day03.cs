using System;
using System.Collections.Generic;
using System.Linq;

namespace _2015
{
    public class Day3
    {
        public enum Dir
        {
            Start,
            Up,
            Down,
            Left,
            Right
        }

        public static int ComputePart1(IList<Dir> directions) => 
            VisitFromOrigin(directions).Distinct().Count();

        public static int ComputePart2(IEnumerable<Dir> directions)
        {
            var (santaDirs, robotDirs) = Partition_i(directions, idx => idx % 2 == 0);
            var santaVisits = VisitFromOrigin(santaDirs);
            var robotVisits = VisitFromOrigin(robotDirs);
            return santaVisits.Union(robotVisits).Distinct().Count();
        }
        private static IEnumerable<(int, int)> VisitFromOrigin(IList<Dir> directions)
        {
            return GetVisitedCoords(directions).Append((0,0));
        }

        private static (IList<Dir>, IList<Dir>) Partition_i(IEnumerable<Dir> source, Predicate<int> predicate)
        {
            var partition1 = new List<Dir>();
            var partition0 = new List<Dir>();

            var enumerable = source as Dir[] ?? source.ToArray();
            for (var i = 0; i < enumerable.Count(); i++)
            {
                var dir = enumerable[i];
                if (predicate(i)) 
                    partition1.Add(dir);
                else
                    partition0.Add(dir);
            }
            return (partition0, partition1);
        } 

        static IEnumerable<(int x, int y)> GetVisitedCoords(IList<Dir> directions)
        {
            var (x,y) = (0, 0);
            foreach (var direction in directions)
            {
                yield return direction switch
                {
                    Dir.Up => (x, ++y),
                    Dir.Down => (x, --y),
                    Dir.Left => (--x, y),
                    Dir.Right => (++x, y),
                    _ => throw new NotSupportedException()
                };
            }
        }
    }
}