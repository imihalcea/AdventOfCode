using System;

namespace _2020
{
    public enum Edge
    {
        Left = 0,
        Top = 1,
        Right = 2,
        Bottom = 3
    }

    public class Border : IEquatable<Border>
    {
        public Edge Edge { get; }
        public long Hash { get; }

        public Tile Tile { get; }

        public Border(Tile tile, int[] pixels, Edge edge)
        {
            Edge = edge;
            Hash = Tools.ComputeHash(pixels);
            Tile = tile;
        }

        public override string ToString()
        {
            return $"{Edge}({Tile.Id})";
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
}