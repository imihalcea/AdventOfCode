using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using static _2020.Day12.DirAction;

namespace _2020
{
    public class Day12
    {
        public static int Part1((DirAction action, int value)[] seats)
        {
            return 0;
        }

 

        public static (DirAction action, int value)[] Dataset(string path)
        {
            return File.ReadLines(path)
                .Select(line=>(line.Substring(0,1), line.Substring(1)))
                .Select(p=>(mapDirAction(p.Item1), mapValue(p.Item2)))
                .ToArray();

            int mapValue(string str)
            {
                if (int.TryParse(str, out var value))
                    return value;
                throw new NotSupportedException();
            }
            
            DirAction mapDirAction(string str)
            {
                if(Enum.TryParse<DirAction>(str, out var action))
                    return action;
                throw new NotSupportedException();
            }
        }

        public class Ship
        {
            private DirAction _facing;
            public Ship(DirAction facing = E)
            {
                _facing = facing;
            }


            public void Turn(DirAction action, int value)
            {
                var angle = value * Math.PI / 180 * Orientation(_facing, action);
                var facingVector = DirToVector(_facing);
                var newFacingVector = new int[]
                {
                    (int) Math.Round(facingVector[0] * Math.Cos(angle) - facingVector[1] * Math.Sin(angle)),
                    (int) Math.Round(facingVector[1] * Math.Cos(angle) - facingVector[0] * Math.Sin(angle))
                };
                _facing = VectorToDir(newFacingVector);
            }

            public void Move(DirAction action, int value)
            {
                  
                switch (action)
                {
                    case F:
                        
                }
            }

            private static int Orientation(DirAction facing, DirAction turn) =>
                (facing, turn) switch
                {
                    (E,R) => -1,
                    (E,L) => +1,
                    (W,L) => -1,
                    (W,R) => +1,
                    (N,R) => +1,
                    (N,L) => -1,
                    (S,L) => -1,
                    (S,R) => +1,
                    _ => throw new NotSupportedException()
                };

            private static int[] DirToVector(DirAction facing) =>
                facing switch
                {
                    E=>new[]{1,0},
                    W=>new[]{-1,0},
                    N=>new[]{0,1},
                    S=>new[]{0,-1},
                    _ =>throw new NotSupportedException()
                };

            private static DirAction VectorToDir(int[] vector) =>
                (vector[0], vector[1]) switch
                {
                    (1, 0) => E,
                    (-1, 0) => W,
                    (0, 1) => N,
                    (0, -1) => S,
                    _ => throw new NotSupportedException()
                };
            
        }
        
        public enum DirAction
        {
            N,S,W,E,L,R,F
        }
    }
}