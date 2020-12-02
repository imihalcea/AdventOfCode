using System;
using System.Collections.Generic;
using System.Linq;

namespace _2015
{
    public class Day6
    {
        public static int Part1(int width, List<Instruction> instructions)
        {
            var grid = new Grid(width);
            instructions.ForEach(instruction => grid.Apply(instruction));
            return grid.CountLit();
        }

        public static int Part2(int width, List<Instruction> instructions)
        {
            var grid = new Grid(width);
            instructions.ForEach(instruction => grid.Apply(instruction));
            return grid.TotalBrightness();
        }


        public class Grid
        {
            private Light[,] _lights;

            public Grid(int width)
            {
                _lights = new Light[width, width];
                for (var x = 0; x < width; x++)
                for (var y = 0; y < width; y++)
                    _lights[x, y] = new Light();
            }

            public void Apply(Instruction instruction)
            {
                for (var x = instruction.C1.x; x <= instruction.C2.x; x++)
                for (var y = instruction.C1.y; y <= instruction.C2.y; y++)
                {
                    var light = _lights[x, y];
                    switch (instruction.Op, instruction.Arg)
                    {
                        case (Op.Turn, Arg.On):
                            light.On();
                            break;
                        case (Op.Turn, Arg.Off):
                            light.Off();
                            break;
                        case (Op.Toggle, null):
                            light.Toggle();
                            break;
                    }
                }

            }

            private IEnumerable<Light> Flatten()
            {
                for (int row = 0; row < _lights.GetLength(0); row++)
                for (int col = 0; col < _lights.GetLength(1); col++)
                    yield return _lights[row, col];
            }

            public int CountLit() =>
                Flatten().Count(l => l.State == State.On);

            public int TotalBrightness() =>
                Flatten().Sum(l => l.Brightness);

        }

        public class Light
        {
            public State State { get; private set; }

            public int Brightness { get; private set; }

            public Light()
            {
                State = State.Off;
                Brightness = 0;
            }

            public void On()
            {
                State = State.On;
                Brightness += 1;
            }

            public void Off()
            {
                State = State.Off;
                if (Brightness != 0)
                    Brightness -= 1;
            }

            public void Toggle()
            {
                State = (State) ((int) State * -1);
                Brightness += 2;
            }

        }

        public enum State
        {
            On = 1,
            Off = -1
        }

        public enum Op
        {
            Turn,
            Toggle
        }

        public enum Arg
        {
            On,
            Off
        }

        public class Instruction
        {
            public Op Op { get; }
            public Arg? Arg { get; }
            public (int x, int y) C1 { get; }
            public (int x, int y) C2 { get; }

            Instruction(Op op, Arg? arg, (int, int) c1, (int, int) c2)
            {
                Op = op;
                Arg = arg;
                C1 = c1;
                C2 = c2;
            }

            public static Instruction FromString(string str)
            {
                var parts = str.Split(" ");
                var op = Enum.Parse<Op>(parts[0], true);
                if (parts.Length == 5)
                {
                    var arg = Enum.Parse<Arg>(parts[1], true);
                    var c1 = parts[2].Split(",").Select(int.Parse).ToArray();
                    var c2 = parts[4].Split(",").Select(int.Parse).ToArray();
                    return new Instruction(op, arg, (c1[0], c1[1]), (c2[0], c2[1]));
                }

                if (parts.Length == 4)
                {
                    var c1 = parts[1].Split(",").Select(int.Parse).ToArray();
                    var c2 = parts[3].Split(",").Select(int.Parse).ToArray();
                    return new Instruction(op, null, (c1[0], c1[1]), (c2[0], c2[1]));
                }

                throw new NotSupportedException("unknown instruction format");
            }
        }
    }
}