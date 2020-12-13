using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static System.Enum;
using static System.Int32;
using static _2020.Day12.ShipAction;

namespace _2020
{
    public class Day12
    {
        public static int Part1((ShipAction action, int value)[] input)
        {
            var funcs = new Dictionary<ShipAction, Func<int, Ship, Ship>>()
            {
                [F] = (v,s)=>MoveShip(s.dir,v,s),
                [S] = (v,s)=>MoveShip((0,-1),v,s),
                [N] = (v,s)=>MoveShip((0,1),v,s),
                [W] = (v,s)=>MoveShip((-1,0),v,s),
                [E] = (v,s)=>MoveShip((1,0),v,s),
                [R] = (a,s)=>Rotate(360-a,s),
                [L] = (a,s)=>Rotate(a,s)
            };
            var ship = input.Aggregate(new Ship(), (s, next) =>
            {
                var (action, value) = next;
                return funcs[action](value, s);
            });
            return Math.Abs(ship.pos.x) + Math.Abs(ship.pos.y);
        }
        
        public static int Part2((ShipAction action, int value)[] input)
        {
            var funcs = new Dictionary<ShipAction, Func<int, Ship, Ship>>()
            {
                [F] = (v,s)=>MoveShip(s.dir,v,s),
                [S] = (v,s)=>MoveWaypoint((0,-1),v,s),
                [N] = (v,s)=>MoveWaypoint((0,1),v,s),
                [W] = (v,s)=>MoveWaypoint((-1,0),v,s),
                [E] = (v,s)=>MoveWaypoint((1,0),v,s),
                [R] = (a,s)=>Rotate(360-a,s),
                [L] = (a,s)=>Rotate(a,s)
            };
            var ship = input.Aggregate(new Ship((0,0),(1,0),(10,1)), (s, next) =>
            {
                var (action, value) = next;
                return funcs[action](value, s);
            });
            return Math.Abs(ship.pos.x) + Math.Abs(ship.pos.y);
        }

        private static Ship Rotate(int angle, Ship ship)
        {
            return new Ship(ship.pos, Rotate(angle, ship.dir), Rotate(angle,ship.waypoint));
        }

        private static (int x, int y)? Rotate(int angle, (int x,int y)? p)
        {
            if (p is null) return p;
            var (x, y) = p.Value;
            var rad = angle * Math.PI / 180;
            (x, y) = (
                (int) Math.Round(x * Math.Cos(rad) - y * Math.Sin(rad)),
                (int) Math.Round(y * Math.Cos(rad) + x * Math.Sin(rad)));
            return (x,y);
        }

        private static Ship MoveShip((int x, int y) dir, int value, Ship ship)
        {
            int dx,dy;
            if (ship.waypoint == null)
            {
                (dx, dy) = (dir.x*value, dir.y*value);
            }
            else
            {
                var (wpx, wpy) = ship.waypoint.Value;
                (dx, dy) = (wpx*value, wpy*value);
            }
            var (nx, ny) = ship.pos;
            return new Ship((nx+dx,ny+dy), ship.dir, ship.waypoint);
        }
        
        private static Ship MoveWaypoint((int x, int y) dir, int value, Ship ship)
        {
            if (ship.waypoint == null) return ship;
            var (wpx, wpy) = ship.waypoint.Value;
            var (dx, dy) = (dir.x*value, dir.y*value);
            return new Ship(ship.pos, ship.dir, (wpx+dx,wpy+dy));
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
        
        public class Ship
        {
            public (int x, int y) dir;
            public (int x, int y) pos;
            public (int x, int y)? waypoint;

            public Ship((int x, int y)pos=default, (int x, int y)? dir=null, (int x, int y)? waypoint=null)
            {
                this.dir = dir ?? (1,0);
                this.pos = pos;
                this.waypoint = waypoint;
            }

            public override string ToString()
            {
                return $"{nameof(dir)}: {dir}, {nameof(pos)}: {pos}, {nameof(waypoint)}: {waypoint}";
            }
        }

    }
}