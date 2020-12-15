using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace _2020
{
    public class Day14
    {
        public static long Part1(IEnumerable<Program> programs) =>
            programs
                .Aggregate(new Dictionary<long, long>(), Eval)
                .Sum(kv => kv.Value);

        public static long Part2(IEnumerable<Program> programs) =>
            programs
                .Aggregate(new Dictionary<long, long>(), Eval2)
                .Sum(kv => kv.Value);

        public static IList<Program> Dataset(string filePath)
        {
            var lines = File.ReadAllLines(filePath);
            var programs = new List<Program>();
            Program? current = null;
            foreach (var line in lines)
            {
                if (line.StartsWith("mask"))
                {
                   current = new Program(line.Split(" = ")[1]);
                   programs.Add(current);
                }
                else
                {
                    var p = Regex.Matches(line, "\\d+").Select(m => long.Parse(m.Value)).ToArray();
                    current?.Add(p[0],p[1]);
                }
                
            }

            return programs;
        }

        public static Dictionary<long, long> Eval2(Dictionary<long, long> mem, Program program)
        {
            foreach (var (baseAddress, value) in program.WriteOps)
            foreach (var address in Addresses(baseAddress, program.Mask.PadLeft(36, '0'), 35))
                mem[address] = value;

            return mem;
        }

        private static IEnumerable<long> Addresses(long baseAddr, string mask, int i)
        {
            if (i == -1)
                yield return 0;
            else
                foreach (var prefix in Addresses(baseAddr, mask, i - 1))
                    switch (mask[i])
                    {
                        case '0':
                            yield return (prefix << 1) + ((baseAddr >> 35 - i) & 1);
                            break;
                        case '1':
                            yield return (prefix << 1) + 1;
                            break;
                        default:
                            yield return (prefix << 1);
                            yield return (prefix << 1) + 1;
                            break;
                    }
        }

        public static Dictionary<long, long> Eval(Dictionary<long, long> mem, Program program)
        {
            var andMask = Convert.ToInt64(program.Mask.Replace('X', '1'), 2);
            var orMask = Convert.ToInt64(program.Mask.Replace('X', '0'), 2);
            foreach (var (address, value) in program.WriteOps)
            {
                mem[address] = value & andMask | orMask;
            }

            return mem;
        }

        public class Program
        {
            public List<(long, long)> WriteOps { get;}

            public Program(string mask)
            {
                Mask = mask;
                WriteOps = new List<(long, long)>();
            }


            public string Mask { get;}

            public void Add(long address, long value) => 
                WriteOps.Add((address, value));
        }
    }
}