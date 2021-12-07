using System;
using System.IO;
using System.Linq;

namespace _2020
{
    public class Day20m
    {
        public static Tiles Dataset(string filePath)
        {
            var data = File.ReadAllText(filePath).Split(Environment.NewLine + Environment.NewLine).Select(
                tileDef =>
                {
                    var lines = tileDef.Split(Environment.NewLine);
                    var id = int.Parse(lines[0].Substring(5, 4));
                    var pixels = lines[1..].Select(line => line.Select(c => c == '#' ? 1 : 0).ToArray()).ToArray();
                    return new Tile(id, pixels);
                }).ToDictionary(t => t.Id, t => t);
            return new Tiles(data);
        }

         public static long Part1(Tiles tiles) => 
             tiles.Corners.Aggregate(1L, (acc, n) => acc * n.Id);

    }
}