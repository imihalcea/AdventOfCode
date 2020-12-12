using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _2020
{
    public class Day11
    {
        public static int Part2(Seats seats)
        {
            int i = 0;
            while (i<1000)
            {
                Console.Write(i%10==0?$"\n{i};":$"{i};");
                var hasEvolved = seats.Evolve2(5);
                i++;
                if (!hasEvolved)
                {
                    return seats.CountOccupied();
                }
            }
            return 0;
        }
        public static int Part1(Seats seats)
        {
            int i = 0;
            var hasEvolved = seats.Evolve1(4);
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
        
        private readonly List<Seat?> _flattenSeats;
        public Seats(int xm, int ym)
        {
            Xm = xm;
            Ym = ym;
            _flattenSeats = new List<Seat?>();
        }

        public Seats AddSeat(char c, int x, int y)
        {
            _flattenSeats.Add(c=='L'?new Seat(x, y):default(Seat));
            return this;
        }
        public Seat? GetSeat((int x, int y) p)
        {
            try
            {
                var seat = _flattenSeats[p.y * Ym + p.x];
                if(seat!=null && (seat.X!=p.x || seat.Y != p.y)) throw new NotSupportedException();
                return seat;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public Func<int, bool> Evolve1 => limit => Evolve(limit, OccupiedAdj);
        public Func<int, bool> Evolve2 => limit => Evolve(limit, OccupiedSeen);

        public bool Evolve(int limit, Func<Seat, int> f) =>
            _flattenSeats.Where(s=>s!=null).Any(s => s!.Evolve(f(s), limit));

        public int CountOccupied() => _flattenSeats.Count(s => s!=null && s.IsOccupied);
        
        public int OccupiedAdj(Seat seat) => Adj(seat.X, seat.Y).Count(point =>
        {
            var s = GetSeat(point);
            return s!=null && s.IsOccupied;
        });

        public int OccupiedSeen(Seat seat)
        {
            return Directions.Sum(d => Look(d, seat.X, seat.Y));
        }

        private int Look(Func<int, int, (int, int)> direction, int x, int y)
        {
            while (true)
            {
                (x,y) = direction(x, y);
                if (!IsValid(x,y)) return 0;
                if (GetSeat((x, y)) is { } s)
                    return s.IsOccupied?1:0;
            }
        }

        private bool IsValid(int x, int y) => (x >= 0 && x < Xm) && (y >= 0 && y < Ym);
        private static Func<int, int, (int, int)>[] Directions =>
            new Func<int, int, (int, int)>[]
            {
               (x,y) => (x - 1, y),
               (x,y) => (x + 1, y),
               (x,y) => (x, y - 1),
               (x,y) => (x, y + 1),
               (x,y) => (x - 1, y - 1),
               (x,y) => (x + 1, y - 1),
               (x,y) => (x + 1, y + 1),
               (x,y) => (x - 1, y + 1),
            };

        private (int, int)[] Adj(int x, int y) =>
            Directions.Select(d => d(x, y)).ToArray();
    }
    
    public class Seat
    {
        public int X { get; }
        public int Y { get; }
        private State SeatState { get; set; }

        public Seat(int x, int y)
        {
            X = x; Y = y; SeatState = State.Empty;
        }

        public bool Evolve(int occupied, int limit)
        {
            var oldState = this.SeatState;
            var newState = occupied switch
            {
                {} when occupied == 0 && IsEmpty => State.Occupied,
                {} when occupied >= limit && IsOccupied => State.Empty,
                _ => oldState
            };
            this.SeatState = newState;
            return newState != oldState;
        }

        public bool IsEmpty => SeatState == State.Empty;
        public bool IsOccupied => SeatState == State.Occupied;
    }
    public enum State { Occupied, Empty }
}