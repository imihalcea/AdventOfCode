using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static System.Linq.Enumerable;

namespace _2020
{
    public class Day17
    {
        public static int Part1(int rounds, HashSet<Cell> dataSet) =>
            Range(0, rounds).Aggregate(
                    new Game(dataSet),
                    (game, _) => game.Evolve())
                .CountActive();

        public static int Part2(int rounds, HashSet<Cell> dataSet) =>
            Range(0, rounds).Aggregate(
                    new Game(dataSet),
                    (game, _) => game.Evolve())
                .CountActive();

        public static HashSet<Cell> Dataset(string filePath, int dimension=3)
        {
            var allLines = File.ReadAllLines(filePath);
            return 
                (from y in Range(0, allLines.Length)
                from x in Range(0, allLines[0].Length)
                where allLines[y][x]=='#'
                select (x, y, z:0, w:0)).Aggregate(
                new HashSet<Cell>(),
                (acc, ci) =>
                {
                    var (x, y, z, w) = ci;
                    var coords = dimension == 4 ? new[] {x, y, z, w} : new[] {x, y, z};
                    acc.Add(new Cell(coords));
                    return acc;
                });
       }
    }

    public class Game
    {
        private readonly HashSet<Cell> _activeCells;
        private readonly Dictionary<Cell, HashSet<Cell>> _memory;
        private static readonly int[] NeighborsCoefs =  { -1, 0, 1 };

        public Game(HashSet<Cell> activeCells)
        {
            _activeCells = activeCells;
            _memory = new Dictionary<Cell, HashSet<Cell>>();
        }

        private Game(HashSet<Cell> activeCells, Dictionary<Cell, HashSet<Cell>> memory)
        {
            _activeCells = activeCells;
            _memory = memory;
        }

        public Game Evolve()
        {
            var nextGenCandidates = _activeCells.SelectMany(NeighborsOf).ToHashSet();
            var nextGen = new HashSet<Cell>();
            foreach (var candidate in nextGenCandidates)
            {
                var activeNeighbors = ActiveNeighborsCount(candidate);
                var isActive = _activeCells.Contains(candidate);
                if (candidate.Evolve(isActive, activeNeighbors))
                {
                    nextGen.Add(candidate);
                }
            }
            return new Game(nextGen,_memory);
        }

        public int CountActive() => _activeCells.Count;

        public int ActiveNeighborsCount(Cell cell) => 
            NeighborsOf(cell).Count(n => _activeCells.Contains(n));

        public HashSet<Cell> NeighborsOf(Cell cell)
        {
            if (!_memory.ContainsKey(cell))
            {
                _memory[cell] = cell.Dimension == 3 ? Neighbors3d(cell) : Neighbors4d(cell);
            }
            return _memory[cell];

            

            HashSet<Cell> Neighbors3d(Cell x)
            {
                var coefs = (
                    from cx in NeighborsCoefs
                    from cy in NeighborsCoefs
                    from cz in NeighborsCoefs
                    let cs = new []{cx,cy,cz}
                    where cs.Any(c => c != 0)
                    select cs).ToArray();

                return coefs
                    .Select(c => x.Coords.Zip(c).Select(p => p.First + p.Second).ToArray())
                    .Select(coords => new Cell(coords)).ToHashSet();
            }

            HashSet<Cell> Neighbors4d(Cell x)
            {
                var coefs = (
                    from cx in NeighborsCoefs
                    from cy in NeighborsCoefs
                    from cz in NeighborsCoefs
                    from cw in NeighborsCoefs
                    let cs = new []{cx,cy,cz, cw}
                    where cs.Any(c=>c!=0)
                    select new []{cx, cy, cz, cw}).ToArray();

                return coefs
                    .Select(c => x.Coords.Zip(c).Select(p => p.First + p.Second).ToArray())
                    .Select(coords => new Cell(coords)).ToHashSet();
            }
        }
    }

    public class Cell : IEquatable<Cell>
    {
        public int[] Coords { get; }
        

        public Cell(int[] coords)
        {
            Dimension = coords.Length;
            Coords = coords;
        }

        public int Dimension { get; }

        public bool Evolve(bool isActive, int adjActive) =>
            isActive && (adjActive == 2 || adjActive == 3)
            || !isActive && adjActive == 3;


        public bool Equals(Cell? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Coords.SequenceEqual(other.Coords);
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Cell) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 19;
                foreach (var c in Coords)
                {
                    hash = hash * 31 + c;
                }

                return hash;
            }
        }

        public static bool operator ==(Cell? left, Cell? right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Cell? left, Cell? right)
        {
            return !Equals(left, right);
        }
        public override string ToString()
        {
            return $"({string.Join(",",Coords)})";
        }
    }
}