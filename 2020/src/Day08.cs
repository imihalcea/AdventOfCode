using System;
using System.Net.Sockets;

namespace _2020
{
    public class Day08
    {
        private static int Part1()
        {
            (string opName, int arg)[] program = LoadProgram();
            var environment = new Env();
            Interpreter.Eval(environment, program);
            return environment.Accumulator;
        }

        private static (string opName, int arg)[] LoadProgram()
        {
            throw new NotImplementedException();
        }


        delegate int Op(Env env, int currentInstruction,int arg);
        
        static class Interpreter
        {
            public static void Eval(Env environment, (string opName, int argValue)[] program)
            {
                var marked = new bool[program.Length];
                for (var i = 0; i < program.Length; i++)
                {
                    if(marked[i]) break;
                    Op(program[i].opName)(environment, i, program[i].argValue);
                }
            }

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
            
            
            
        }
    }
}