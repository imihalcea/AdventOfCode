using System;
using System.Collections.Generic;
using System.Linq;

namespace _2015
{
    public class Day14
    {
        public static int Part1(Reindeer[] dataSet) => new Competition(dataSet).MaxDistance(2503).distance;
        public static int Part2(Reindeer[] dataSet) => new Competition(dataSet).MaxPoints(2503).points;


        public class Competition
        {
            private readonly Reindeer[] _competitors;
            private Dictionary<Reindeer, int> _leaderBoard;
            public Competition(Reindeer[] competitors)
            {
                _competitors = competitors;
                _leaderBoard = _competitors.ToDictionary(c => c, _ => 0);
            }


            public (Reindeer winner, int distance) MaxDistance(int time)
            {
                while (time > 0)
                {
                    foreach (var competitor in _competitors) competitor.Race();
                    time -= 1;
                }
                return _competitors.OrderByDescending(c => c.Distance).Select(c => (c, c.Distance)).First();
            }
            
            
            public (Reindeer winner, int points) MaxPoints(int time)
            {
                while (time>0)
                {
                    foreach (var competitor in _competitors) competitor.Race();
                    
                    var leaders = _competitors.GroupBy(c => c.Distance).OrderByDescending(g => g.Key).First();
                    foreach (var leader in leaders)
                    {
                        _leaderBoard[leader] += 1;
                    }
                    time -= 1;
                }

                return _leaderBoard.OrderByDescending(kv => kv.Value).Select(kv => (kv.Key, kv.Value)).First();
            }

            public int Score(Reindeer r) => _leaderBoard[r];

        }
        
        public class Reindeer : IEquatable<Reindeer>
        {
            public enum State
            {
                Running,
                Resting
            }
            public string Name { get; }
            public int Speed { get; }
            public int RunTime { get; }
            public int RestTime { get; }

            public Reindeer(string name, int speed, int runTime, int restTime)
            {
                Name = name;
                Speed = speed;
                RunTime = runTime;
                RestTime = restTime;
                CurrentState = State.Running;
                StateTime = 0;
            }

            private int StateTime { get; set; }

            private State CurrentState { get; set; }

            public int Distance { get; set; }

            public void Race()
            {
                StateTime += 1;
                CurrentState = NextState();
            }

            private State NextState()
            {
                if (CurrentState == State.Running && StateTime == RunTime)
                {
                    Distance += Speed;
                    StateTime = 0;
                    return State.Resting;
                }
                if (CurrentState==State.Resting && StateTime == RestTime)
                {
                    StateTime = 0;
                    return State.Running;
                }
                if (CurrentState == State.Resting && StateTime < RestTime)
                {
                    return State.Resting;
                }

                Distance += Speed;
                return State.Running;
            }
            
            public bool Equals(Reindeer other)
            {
                if (ReferenceEquals(null, other)) return false;
                if (ReferenceEquals(this, other)) return true;
                return Name == other.Name;
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != this.GetType()) return false;
                return Equals((Reindeer) obj);
            }

            public override int GetHashCode()
            {
                return (Name != null ? Name.GetHashCode() : 0);
            }

            public static bool operator ==(Reindeer left, Reindeer right)
            {
                return Equals(left, right);
            }

            public static bool operator !=(Reindeer left, Reindeer right)
            {
                return !Equals(left, right);
            }
        }
    }
    
    
}