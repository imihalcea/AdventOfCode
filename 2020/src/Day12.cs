using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Numerics;
using static System.Enum;
using static System.Int32;
using static _2020.Day12.ShipAction;

namespace _2020
{
    public class Day12
    {
        public static int Part1((ShipAction action, int value)[] input)
        {
            var funcs = new Dictionary<ShipAction, Func<ShipAction, int, Ship, Ship>>()
            {
                [F] = (_,v,s)=> Move(s.dir,v,s),
                [S] = Move,
                [N] = Move,
                [W] = Move,
                [E] = Move,
                [R] = Rotate,
                [L] = Rotate
            };
            var ship = input.Aggregate(new Ship(), (s, n) =>
            {
                var (action, value) = n;
                return funcs[action](action, value, s);
            });
            return Math.Abs(ship.pos.x) + Math.Abs(ship.pos.y);
        }
        
        
        public static Ship Rotate(ShipAction action, int value, Ship ship)
        {
            var angle = action==R?360-value:value;
            var rad = angle * Math.PI / 180;
            var (x, y) = Orientation[ship.dir];
            var reorientation = (
                (int) Math.Round(x * Math.Cos(rad) - y * Math.Sin(rad)),
                (int) Math.Round(y * Math.Cos(rad) + x * Math.Sin(rad)));
            var new_dir = Orientation.First(kv => kv.Value == reorientation).Key;
            return new Ship(ship.pos, new_dir);
        }

        public static Ship Move(ShipAction action, int value, Ship ship)
        { 
            var (nx, ny) = ship.pos;
            return action switch
            {
                N => new Ship((nx,ny+value),ship.dir),
                S => new Ship((nx,ny-value),ship.dir),
                W => new Ship((nx-value,ny),ship.dir),
                E => new Ship((nx+value,ny),ship.dir),
                _ => throw new NotSupportedException()
            };
        }
            
        public static (ShipAction action, int value)[] Dataset(string filePath) =>
            File.ReadLines(filePath)
                .Select(line=>(
                    Parse<ShipAction>(line.Substring(0,1)), 
                    Parse(line.Substring(1))))
                .ToArray();

        public enum ShipAction
        {
            N,S,W,E,L,R,F
        }
        
        private static Dictionary<ShipAction,(int x, int y)> Orientation = new Dictionary<ShipAction, (int x, int y)>()
        {
            [E]=(1,0),
            [W]=(-1,0),
            [N]=(0,1),
            [S]=(0,-1)
        };
        public class Ship
        {
            public ShipAction dir;
            public (int x, int y) pos;
            public Ship((int x, int y)pos=default, ShipAction dir=E)
            {
                this.dir = dir;
                this.pos = pos;
            }
        }

    }
}