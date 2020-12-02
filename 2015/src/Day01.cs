using System;
using System.Linq;

namespace _2015
{
    public class Day1
    {
        public static int Floor(string instructions) => 
            instructions.Aggregate(0, (acc, ins) => NextFloor(ins, acc));

        private static int NextFloor(char instruction, int floor) =>
            instruction switch
            {
                '(' => floor + 1,
                ')' => floor - 1,
                _ => floor
            };

        public static int? Basement(string instructions)
        {
            var position = 0;
            var floor = 0;
            foreach (var instruction in instructions)
            {
                floor = NextFloor(instruction, floor);
                position++;
                if (floor == -1)
                    return position;
            }
            return null;
        }
    }
}