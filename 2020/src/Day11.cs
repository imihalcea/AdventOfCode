using System.Collections.Generic;

namespace _2020
{
    public class Day11
    {
        public static Seats Dataset(string path)
        {
            
        } 
    }

    public class Seats
    {
        private List<Seat> _seats;

        public Seats()
        {
            _seats = new List<Seat>();
        }


        public void AddSeat(char c, int x, int y)
        {
            if (c == 'L')
                _seats.Add(new Seat(x,y, State.Empty));
        }
            
    }
    
    
    public class Seat
    {
        public int X { get; }
        public int Y { get; }
        public State SeatState { get; }

        public Seat(int x, int y, State seatState)
        {
            X = x; Y = y; SeatState = seatState;
        }

        public Seat Evolve(int occupiedAdjSeats) =>
            occupiedAdjSeats switch
            {
                {} when occupiedAdjSeats == 0 && IsEmpty => new Seat(X,Y,State.Occupied),
                {} when occupiedAdjSeats >= 4 && IsOccupied => new Seat(X,Y,State.Empty),
                _ => this
            };
        private bool IsEmpty => SeatState == State.Empty;
        private bool IsOccupied => SeatState == State.Occupied;
    }
    public enum State { Occupied, Empty }
}