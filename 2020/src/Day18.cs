using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace _2020
{
    
    
    public class Day18
    {


        public static long Part1(string[] dataset) => 
            dataset
                .Select(Parse)
                .Sum(op=>op.Eval());
        
        public static long Part2(string[] dataset) => 
            dataset
                .Select(Parse2)
                .Sum(op=>op.Eval());


        public static string[] Dataset(string filePath) => 
            File.ReadAllLines(filePath);

        public static Op Parse(string expr)
        {
            var regExp = new Regex(@"\(([^()]+)\)");
            while (expr.Contains('('))
            {
                expr = regExp.Replace(expr, m => ParseBasicOp(m.Value.Replace("(","").Replace(")","")).Eval().ToString());
            }
            return ParseBasicOp(expr);
        }

        public static Op Parse2(string expr)
        {
            var regExp = new Regex(@"\(([^()]+)\)");
            while (expr.Contains('('))
            {
                expr = regExp.Replace(expr, m => ParseBasicOp2(m.Value.Replace("(","").Replace(")","")).Eval().ToString());
            }
            return ParseBasicOp2(expr);
        }

        
        private static Op ParseBasicOp2(string expr)
        {
            var parts = expr.Split(' ');
            var opSymbols = parts.Where(p => p=="+" || p=="*").ToList();
            var ops = parts.Where(p => p!="+" && p!="*").Select(d=> new Ident(long.Parse(d))).Cast<Op>().ToList();
            
            while (opSymbols.Contains("+"))
            {
                (ops, opSymbols) = ParsePrioritary(ops, opSymbols);
            }

            var basicOp = opSymbols.Zip(ops.Skip(1))
                .Aggregate(
                    ops.First(), 
                    (acc, n) => CreateOp(n.First, acc, n.Second)
                );
            return basicOp;
        }

        public static (List<Op> ops, List<string> symbols) ParsePrioritary(List<Op> ops, List<string> symbols)
        {
            var index = symbols.FindIndex(s => s == "+");
            var opSym = symbols[index];
            var left = ops[index];
            var right = ops[index + 1];
            var op = CreateOp(opSym, left, right);
            ops.Remove(left);
            ops.Remove(right);
            ops.Insert(index, op);
            symbols.RemoveAt(index);
            return (ops, symbols);
        }

        private static Op CreateOp(string symbol, Op left, Op right) =>
            symbol switch
            {
                "*" => new Mul(left, right),
                "+" => new Add(left, right),
                _ => throw new NotSupportedException()
            };

        private static Op ParseBasicOp(string expr)
        {
            
            var parts = expr.Split(' ');
            var opSymbols = parts.Where(p => p=="+" || p=="*").ToArray();
            var nums = parts.Where(p => p!="+" && p!="*").Select(d=> new Ident(long.Parse(d))).ToArray();

            var basicOp = opSymbols[1..].Zip(nums[2..])
                .Aggregate(
                    CreateOp(opSymbols[0], nums[0], nums[1]), 
                    (acc, n) => CreateOp(n.First, acc, n.Second)
                );
            return basicOp;
        }
    }

    
    
    public interface Op
    {
        long Eval();
    }

    public class Mul : Op
    {
        public Op Left { get; }
        public Op Right { get; }

        public Mul(Op left, Op right)
        {
            Left = left;
            Right = right;
        }
        public long Eval() => Left.Eval() * Right.Eval();
    }

    public class Add : Op
    {
        public Op Left { get; }
        public Op Right { get; }

        public Add(Op left, Op right)
        {
            Left = left;
            Right = right;
        }
        public long Eval() => Left.Eval() + Right.Eval();
    }

    public class Ident : Op
    {
        private readonly long _a;

        public Ident(long a)
        {
            _a = a;
        }

        public long Eval() => _a;
    }
}