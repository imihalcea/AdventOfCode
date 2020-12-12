using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static _2020.State;

namespace _2020
{
    public class Day11
    {
        public static int Part2(Seats seats)
        {
            var hasEvolved = true;
            while (hasEvolved) 
                hasEvolved = seats.Evolve2(5);
            return seats.CountOccupied();
        }
        public static int Part1(Seats seats)
        {
            var hasEvolved = true;
            while (hasEvolved) 
                hasEvolved = seats.Evolve1(4);
            return seats.CountOccupied();
        }
        public static Seats Dataset(string path)
        {
            var lines = File.ReadAllLines(path);
            var seats = new Seats(lines[0].Length-1, lines.Length-1);
            for (var y = 0; y < lines.Length; y++)
            {
                var line = lines[y].ToCharArray();
                for (var x = 0; x < line.Length; x++)
                {
                    seats.AddSeat(line[x],x,y);
                }
            }
            return seats;
        } 
    }

    public class Seats
    {
        public int Xm { get; }
        public int Ym { get; }
        
        private List<State> _states;
        private readonly int[][] _coordsToIndex;
        private readonly (int, int)[] _indexToCoords;
        public Seats(int xm, int ym)
        {
            Xm = xm;
            Ym = ym;
            _states = new List<State>();
            _coordsToIndex = new int[Xm+1][];
            for (int x = 0; x <= xm; x++)
            {
                _coordsToIndex[x] = new int[ym+1];
            }
            _indexToCoords = new (int, int)[(Ym+1)*(Xm+1)];
        }

        public Seats AddSeat(char c, int x, int y)
        {
            _states.Add(c == 'L' ? Empty : Floor);
            var index = _states.Count - 1;
            _coordsToIndex[x][y] = index;
            _indexToCoords[index] = (x, y);
            return this;
        }

        private State GetState((int x, int y) p)
        {
            try
            {
                var seat = _states[CoordsToIndex(p)];
                return seat;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        private int CoordsToIndex((int x, int y) p) => _coordsToIndex[p.x][p.y];

        private (int x, int y) IndexToCoords(int index) => _indexToCoords[index];
        

        public Func<int, bool> Evolve1 => limit => Evolve(limit, OccupiedAdj);
        public Func<int, bool> Evolve2 => limit => Evolve(limit, OccupiedSeen);

        public bool Evolve(int limit, Func<int, int> f)
        {
            var nextStates = _states
                .Select((state,index)=>(state,index))
                .Select(item => EvolveState(item.state,f(item.index), limit))
                .ToList();
            var hasEvolved = !_states.SequenceEqual(nextStates);
            _states = nextStates;
            return hasEvolved;
        }

        private State EvolveState(State currentState, int occupied, int limit) =>
            (currentState, occupied) switch
            {
                var (cs, o) when (cs, o) == (Empty, 0) => Occupied,
                var (cs, o) when cs == Occupied && o >= limit => Empty,
                _ => currentState
            };


        public int CountOccupied() => _states.Count(s => s==Occupied);
        
        private int OccupiedAdj(int index) => 
            Adj(IndexToCoords(index)).Where(IsValid).Count(point => GetState(point) == Occupied);


        private int OccupiedSeen(int index) => 
            Directions.Sum(d => Look(d, IndexToCoords(index)));

        private int Look(Func<(int, int), (int, int)> direction, (int x, int y) p)
        {
            while (true)
            {
                p = direction(p);
                if (!IsValid(p)) return 0;
                var state = GetState(p);
                if (state != Floor )
                    return state==Occupied?1:0;
            }
        }

        private bool IsValid((int x, int y) p) => (p.x >= 0 && p.x <= Xm) && (p.y >= 0 && p.y <= Ym);
        private static Func<(int, int), (int, int)>[] Directions =>
            new Func<(int, int), (int, int)>[]
            {
               p => (p.Item1- 1, p.Item2),
               p => (p.Item1 + 1, p.Item2),
               p => (p.Item1, p.Item2 - 1),
               p => (p.Item1, p.Item2 + 1),
               p => (p.Item1 - 1, p.Item2 - 1),
               p => (p.Item1 + 1, p.Item2 - 1),
               p => (p.Item1 + 1, p.Item2 + 1),
               p => (p.Item1 - 1, p.Item2 + 1),
            };

        private static (int, int)[] Adj((int x, int y) p) =>
            Directions.Select(d => d(p)).ToArray();
    }
    

    public enum State {None, Occupied, Empty, Floor }
}