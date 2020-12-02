using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace _2015
{
    public class Day7
    {

        

        public class Interpreter
        {
            private readonly ConcurrentDictionary<string, ushort> _mem;

            public Func<string, ushort> Read =>
                x => Char.IsLetter(x[0]) ? _mem[x] : ushort.Parse(x);    

            public Interpreter()
            {
                _mem = new ConcurrentDictionary<string, ushort>();
            }

            public void InterpretBruteForce(List<Instruction> instructions)
            {
                var assignments = instructions.Where(i => i.Operation == Op.Assign).ToList();
                assignments.ForEach(a=>TryEval(a));

                while (_mem.Keys.Count() != instructions.Count)
                {
                    foreach (var instruction in instructions.Where(IsUnEvaluated))
                    {
                        TryEval(instruction);
                    }
                }
            }

            public bool IsUnEvaluated(Instruction instruction) =>
                !_mem.ContainsKey(instruction.Target);

            private bool CanEvaluate(Instruction instruction)
                => instruction.TriggerVariables.All(triggers => _mem.ContainsKey(triggers)); 

            private bool TryEval(Instruction instruction)
            {
                if (!CanEvaluate(instruction)) return false;
                var evaluator = Evaluator(instruction);
                var result = evaluator(instruction.Triggers);
                _mem[instruction.Target] = result;
                return true;
            }

            private Func<string[], ushort> Evaluator(Instruction instruction) =>
                instruction.Operation switch
                {
                    Op.And => and,
                    Op.Assign => assign,
                    Op.Or => or,
                    Op.LShift => lshift,
                    Op.RShift => rshift,
                    Op.Not => not,
                    _=> throw new NotSupportedException()
                };
            
            
            private Func<string[], ushort> assign => x => Read(x[0]);
            private Func<string[], ushort> not => x => (ushort)~Read(x[0]);
            private Func<string[], ushort> and => x => (ushort) (Read(x[0]) & Read(x[1]));
            private Func<string[], ushort> or => x => (ushort) (Read(x[0]) | Read(x[1]));
            private Func<string[], ushort> lshift => x => (ushort) (Read(x[0]) << Read(x[1]));
            private Func<string[], ushort> rshift => x => (ushort) (Read(x[0]) >> Read(x[1]));
        }
        
        public class Instruction
        {
            private readonly string[] _parts;

            public Instruction(string[] parts)
            {
                _parts = parts;
            }

            public string Target => _parts.Last();

            public Op Operation
            {
                get
                {
                    if (_parts[0] == "NOT") return Op.Not;
                    return _parts[1] switch
                    {
                        "->" => Op.Assign,
                        "AND" => Op.And,
                        "OR" => Op.Or,
                        "LSHIFT" => Op.LShift,
                        "RSHIFT" => Op.RShift,
                        _ => throw new NotSupportedException()
                    };
                }
            }

            public string[] Triggers =>
                Operation switch
                {
                    Op.Assign => new []{_parts[0]},
                    Op.Not => new []{_parts[1]},
                    Op.And => new []{_parts[0], _parts[2]},
                    Op.Or => new []{_parts[0], _parts[2]},
                    Op.LShift => new []{_parts[0], _parts[2]},
                    Op.RShift => new []{_parts[0], _parts[2]},
                    _ => throw new NotSupportedException()
                };

            public IEnumerable<string> TriggerVariables => 
                Triggers.Where(t => Char.IsLetter(t[0]));
        }
        
        public enum Op
        {
            Assign,
            Not,
            And,
            Or,
            LShift,
            RShift
        }
    }

    
}