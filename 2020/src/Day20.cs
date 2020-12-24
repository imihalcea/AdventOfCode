using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static _2020.Day20.Tools;

namespace _2020
{
    public class Day20
    {
        public static Tile[] Dataset(string filePath) =>
            File.ReadAllText(filePath).Split(Environment.NewLine + Environment.NewLine).Select(
                tileDef =>
                {
                    var lines = tileDef.Split(Environment.NewLine);
                    var id = int.Parse(lines[0].Substring(5, 4));
                    var pixels = lines[1..].Select(line => line.Select(c => c == '#' ? 1 : 0).ToArray()).ToArray();
                    return new Tile(id, pixels);
                }).ToArray();

        public static long Part1(Tile[] tiles)
        {
            Tile topLeft  = null;
            var commonBorders = new ConcurrentDictionary<long, HashSet<Tile>>(); 
            foreach (var t in tiles)
            foreach (var tp in t.PossibleTiles())
            foreach (var border in tp.Borders)
            {
                commonBorders.AddOrUpdate(border.Hash, _ => new HashSet<Tile>() {tp}, (_, v) => new HashSet<Tile>(v){tp});
            }

            var links = commonBorders.Where(kv => kv.Value.Count() == 2)
                .Select(kv => new {borderHash = kv.Key, tiles1 = kv.Value.First(), title2 = kv.Value.Last()});

            var image = new HashSet<Tile>();
            var tileLinks = new ConcurrentDictionary<Tile, HashSet<Tile>>();
            foreach (var tile in tiles)
            {
                tileLinks[tile] = new HashSet<Tile>();
            }
            foreach (var link in links)
            {
                var (h, t1, t2) = (link.borderHash, link.tiles1, link.title2);
                tileLinks[t1].Add(t2);
                tileLinks[t2].Add(t1);
            }

            return tileLinks.Where(kv => kv.Value.Count() == 2).Select(kv => kv.Key).Aggregate(1L, (acc, n) => acc * n.Id);
        }

        public class Link
        {
            public Tile T1 { get; }
            public Border B1 { get; }
            public Tile T2 { get; }
            public Border B2 { get; }

            public Link(Tile t1, Border b1, Tile t2, Border b2)
            {
                T1 = t1; B1 = b1; T2 = t2; B2 = b2;
            }

            public override string ToString()
            {
                return $"{T1.Id}({B1.Edge})<->{T2.Id}({B2.Edge})<";
            }
        }
        public enum Edge
        {
            Left = 0,
            Top = 1,
            Right = 2,
            Bottom = 3
        }

        public class Tile : IEquatable<Tile>
        {
            public long Id { get; }
            private readonly int[][] _pixels;
            private Tile[]? _possibleTiles =null;
            public Tile[] Links { get; }

            public Tile(long id, int[][] pixels)
            {
                Id = id;
                _pixels = pixels;
                int[][] pixelsTransposed = Transpose(_pixels);
                Links = new Tile[4];
                Borders = new[]
                {
                    new Border(id,pixelsTransposed[0], Edge.Left),
                    new Border(id,_pixels[0], Edge.Top),
                    new Border(id,pixelsTransposed[^1], Edge.Right),
                    new Border(id, _pixels[^1], Edge.Bottom)
                };
            }

            public void Link(Edge edge, Tile tile)
            {
                Links[(int) edge] = tile;
            }

            public Border[] Borders { get; }

            public Border BorderLeft => Borders[0];
            public Border BorderTop => Borders[1];
            public Border BorderRight => Borders[2];
            public Border BorderBottom => Borders[3];

            public Tile? LinkLeft => Links[0];
            public Tile? LinkRight => Links[2];
            public Tile? LinkTop => Links[1];
            public Tile? LinkBottom => Links[3];
           
            public bool IsTopLeftCorner() =>
                LinkLeft == null && LinkTop == null && LinkBottom != null && LinkRight != null;

            public bool IsTopRightCorner() =>
                LinkLeft != null && LinkTop == null && LinkBottom != null && LinkRight == null;
            
            public bool IsBottomRightCorner() =>
                LinkLeft != null && LinkTop != null && LinkBottom == null && LinkRight == null;
            
            public bool IsBottomLeftCorner() =>
                LinkLeft == null && LinkTop != null && LinkBottom == null && LinkRight != null;




            public Tile[] PossibleTiles()
            {
                //move elements around 8 axes of symmetry 
                var t0 = _pixels;    
                var t1 = Flip(t0);
                var t2 = Rotate(t1);
                var t3 = Flip(t2);
                var t4 = Rotate(t0);
                var t5 = Rotate(t4);
                var t6 = Flip(t5);
                var t7 = Rotate(t4);
                return new[] {t0, t1, t2, t3, t4, t5, t6, t7}.Select(px => new Tile(this.Id, px)).ToArray();
            }
            public override string ToString()
            {
                return $"{nameof(Id)}: {Id}, L: {LinkLeft?.Id},  T: {LinkTop?.Id}, R: {LinkRight?.Id}, B: {LinkBottom?.Id}";
            }

            public bool Equals(Tile? other)
            {
                if (ReferenceEquals(null, other)) return false;
                if (ReferenceEquals(this, other)) return true;
                return Id == other.Id;
            }

            public override bool Equals(object? obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != this.GetType()) return false;
                return Equals((Tile) obj);
            }

            public override int GetHashCode()
            {
                return Id.GetHashCode();
            }

            public static bool operator ==(Tile? left, Tile? right)
            {
                return Equals(left, right);
            }

            public static bool operator !=(Tile? left, Tile? right)
            {
                return !Equals(left, right);
            }
        }

        public class Border : IEquatable<Border>
        {
            public Edge Edge { get; }
            public long Hash { get;}
            
            public long TileId { get; }

            public Border(long tileId, int[] pixels, Edge edge)
            {
                Edge = edge;
                Hash = ComputeHash(pixels);
                TileId = tileId;
            }

            public override string ToString()
            {
                return $"{Edge}";
            }

            public bool Equals(Border? other)
            {
                if (ReferenceEquals(null, other)) return false;
                if (ReferenceEquals(this, other)) return true;
                return Hash == other.Hash;
            }

            public override bool Equals(object? obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != this.GetType()) return false;
                return Equals((Border) obj);
            }

            public override int GetHashCode() => Hash.GetHashCode();
           
            public static bool operator ==(Border? left, Border? right)
            {
                return Equals(left, right);
            }

            public static bool operator !=(Border? left, Border? right)
            {
                return !Equals(left, right);
            }
        }
        public static class Tools
        {
            public static int[][] Transpose(int[][] m)
            {
                var cols = m[0].Length;
                var t = new int[cols][];
                for (var i = 0; i < cols; i++)
                {
                    t[i] = m.Select(r => r[i]).ToArray();
                }
                return t;
            }
            
            public static int[][] Flip(int[][] m)
            {
                int[][] f = new int[m.Length][];
                int cols = m[0].Length;
                for (var i = 0; i < m.Length; i++)
                {
                    f[i] = m[i].Reverse().ToArray();
                }
                return f;
            }

            public static int[][] Rotate(int[][] m) => 
                Transpose(Flip(Transpose(m)));

            public static int[][] FlipAndRotate(int[][] m) =>
                Rotate(Flip(m));
            
            public static long ComputeHash(int[] pixels)
            {
                unchecked
                {
                    return pixels.Aggregate(19L, (current, p) => current * 31 + p);
                }
            }
        }
    }
}