using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;

namespace _2020
{
    public class Day08
    {
        private const string INPUT_FILE_PATH = "test/day08.txt";
        public static int Part1()
        {
            (string opName, int arg)[] program = LoadProgram();
            var environment = Interpreter.Eval(program);
            return environment.Accumulator;
        }
        
        public static int Part2()
        {
            (string opName, int arg)[] program = LoadProgram();
            return (Interpreter.EvalAndPatch(program)?.Accumulator).GetValueOrDefault(-1);
        }

        private static (string opName, int arg)[] LoadProgram() =>
            File.ReadAllLines(INPUT_FILE_PATH).Select(l =>
            {
                var parts = l.Split(' ');
                return (parts[0], int.Parse(parts[1]));
            }).ToArray();

       
        delegate int Op(Env env, int currentInstruction,int arg);
        
        static class Interpreter
        {
            public static Env Eval((string opName, int argValue)[] program)
            {
                var environment = new Env();
                var marked = new bool[program.Length];
                var n = 0;
                while (n<program.Length && !marked[n])
                { 
                   marked[n] = true;
                   n = Op(program[n].opName)(environment, n, program[n].argValue);
                }

                environment.Terminated = n == program.Length;
                return environment;
            }
            
            public static Env? EvalAndPatch((string opName, int argValue)[] program) =>
                PatchedPrograms(program)
                    .Select(Eval)
                    .FirstOrDefault(env => env.Terminated);

            private static IEnumerable<(string opName, int argValue)[]> PatchedPrograms((string opName, int argValue)[] originalProgram) =>
                Enumerable
                    .Range(0, originalProgram.Length)
                    .Where(i => originalProgram[i].opName != "acc")
                    .Select(i =>
                    {
                        var patchedProgram = new (string opName, int argValue)[originalProgram.Length];
                        Array.Copy(originalProgram,patchedProgram,originalProgram.Length); 
                        
                        var patchOp = originalProgram[i].opName switch
                        {
                            "nop" => "jmp",
                            "jmp" => "nop",
                            _ => throw new NotSupportedException()
                        };
                        patchedProgram[i] = (patchOp, patchedProgram[i].argValue);
                        return patchedProgram;
                    });

            private static Op Op(string opName) =>
                opName switch
                {
                    "acc" => acc,
                    "nop" => nop,
                    "jmp" => jmp,
                    _ => throw new NotSupportedException()
                };

            static int acc(Env env, int currentLine, int arg)
            {
                env.Accumulate(arg);
                return currentLine + 1;
            }

            static int nop(Env env, int currentLine, int arg) => currentLine + 1;

            static int jmp(Env env, int currentLine, int arg) => currentLine + arg;
        }
        
        class Env
        {
            public int Accumulator { get; private set; } = 0;
            public void Accumulate(int n)
            {
                Accumulator += n;
            }

           public bool Terminated { get; set; }
         }
    }
}