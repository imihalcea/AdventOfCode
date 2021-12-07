using System;
using System.Collections.Generic;
using System.Linq;
using static _2020.Tools;

namespace _2020
{
    public class Tile 
    {
        private int[][] _pixels;
        private HashSet<Tile> _neighbors;
        private Dictionary<long, Border> _borders;
        private int _currentOrientation;
        private HashSet<Link> _links;

        public Tile(long id, int[][] pixels)
        {
            Id = id;
            _currentOrientation = 0;
            _neighbors = new HashSet<Tile>();
            _links = new HashSet<Link>();
            SetupTile(pixels);
        }

        private void SetupTile(int[][] pixels)
        {
            _pixels = pixels;
            int[][] pixelsTransposed = Transpose(_pixels);
            AllBorders = new[]
            {
                new Border(this, pixelsTransposed[0], Edge.Left),
                new Border(this, _pixels[0], Edge.Top),
                new Border(this, pixelsTransposed[^1], Edge.Right),
                new Border(this, _pixels[^1], Edge.Bottom)
            };
            _borders = AllBorders.ToDictionary(b => b.Hash, b => b);
        }

        public bool ChangeOrientation(Tile variant)
        {
            if (Id != variant.Id) throw new Exception();
            _links.Clear();
            SetupTile(variant._pixels);
            return true;
        }

        public bool ChangeOrientation()
        {
            if (_currentOrientation < 7)
            {
               return ChangeOrientation(Variants[++_currentOrientation]);
            }
            return false;
        }

        private Func<int[][],int[][]>[] Orientations =>
            new Func<int[][],int[][]>[]
            {
                None, Rotate, Rotate, Rotate, Flip, Rotate, Rotate, Rotate
            };
        
        
        public Tile[] Variants =>
            Orientations.Skip(1)
                .Aggregate(new List<int[][]>() {_pixels}, (acc, o) =>
                {
                    var px = o(acc.Last());
                    acc.Add(px);
                    return acc;
                })
                .Select(px => new Tile(Id, px)).ToArray();
            

        public Border[] AllBorders { get; set; }

        public Border BorderRight => AllBorders[2];
        public Border BorderBottom => AllBorders[3];

        public bool HasBorder(long hash) =>
            AllBorders.Any(b => b.Hash == hash);

        public long Id { get; }

        public void AddNeighbor(Tile tile)
        {
            _neighbors.Add(tile);
        }
        public bool IsCorner() => _neighbors.Count == 2;

        public bool IsInPlace => _neighbors.Count == _links.Count;
        public Tile[] Neighbors => _neighbors.ToArray();

        public Tile? NeighborRight => _links.SingleOrDefault(l => l.Border2.Edge == Edge.Left)?.Border2.Tile;
        public Tile? NeighborBottom => _links.SingleOrDefault(l => l.Border2.Edge == Edge.Top)?.Border2.Tile;

        public void AddLink(Border t1Border, Border t2Border)
        {
            var link = new Link(t1Border, t2Border);
            _links.Add(link);
        }

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}";
        }
    }


    public class Link : IEquatable<Link>
    { 
        public Link(Border b1, Border b2)
        {
            if (!IsValid(b1, b2)) throw new Exception();
            Border1 = b1;
            Border2 = b2;
        }

        public Border Border2 { get; }

        public Border Border1 { get; }

        public static bool IsValid(Border b1, Border b2) =>
            b1.Tile != b2.Tile &&
            (b1.Edge, b2.Edge) switch
            {
                (Edge.Right, Edge.Left) => true,
                (Edge.Left, Edge.Right) => true,
                (Edge.Bottom, Edge.Top) => true,
                (Edge.Top, Edge.Bottom) => true,
                _ => false
            };

        public bool Equals(Link? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Border2.Hash.Equals(other.Border2.Hash) && Border1.Hash.Equals(other.Border1.Hash) && 
                   Border2.Edge.Equals(other.Border2.Edge) && Border1.Edge.Equals(other.Border1.Edge);
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Link) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Border2.Hash, Border1.Hash, Border1.Edge, Border2.Edge) ;
        }

        public static bool operator ==(Link? left, Link? right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Link? left, Link? right)
        {
            return !Equals(left, right);
        }
    }
}