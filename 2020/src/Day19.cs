using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static System.Environment;
using static System.String;

namespace _2020
{
    public class Day19
    {
        public static int CountValidMessages((IEnumerable<IRule> rules, string[] messages) dataset)
        {
            var (rules, messages) = dataset;
            var r0 = rules.First();
            return messages.Count(m =>
            {
                var (isMatch, remainder) = r0.Eval(m);
                return isMatch && remainder.Length==0;
            });
        }
        
        public static (IEnumerable<IRule> rules, string[] messages) Dataset(string filePath)
        {
            var parts = File.ReadAllText(filePath).Split(NewLine + NewLine);
            var (strRules, messages) = (
                parts[0].Split(NewLine).ToArray(), 
                parts[1].Split(NewLine).ToArray()
                );
            var rules = ParseRules(strRules).OrderBy(r=>r.Id);
            return (rules, messages);
        }
       static Dictionary<int, IRule> mem = new();
        public static IEnumerable<IRule> ParseRules(string[] str_rules)
        {
            return str_rules
                .Select(s => s.Split(":")[0])
                .Select(int.Parse)
                .Select(GetRule);

            IRule GetRule(int id)
            {
                if (mem.ContainsKey(id)) return mem[id];
                var my_rule = str_rules.First(sr => sr.StartsWith($"{id}:"));
                if (my_rule.Contains("\""))
                {
                    var idx = my_rule.IndexOf("\"", StringComparison.Ordinal);
                    var rule = new CharCheck(id, my_rule[idx + 1]);
                    mem[id] = rule;
                }
                else
                {
                    var parts = my_rule.Split(':')[1].Split('|');
                    var (andPart, orPart) = (parts[0].Trim(), parts.Length==2?parts[1].Trim():"");
                    var and = andPart.Split(' ').Select(int.Parse).Select(GetRule);
                    var or = !IsNullOrEmpty(orPart)?
                        orPart.Split(' ').Select(int.Parse).Select(GetRule):
                        Array.Empty<IRule>();
                    
                    var rule = new Rule(id, and, or);
                    mem[id] = rule;
                }

                return mem[id];
            }
            
        }
    }


    public interface IRule
    {
        (bool isMatch,string remainder) Eval(string message);
        int Id { get; }
        
        
    }

    public class CharCheck : IRule, IEquatable<CharCheck>
    {
        private readonly char _c;

        public CharCheck(int id , char c)
        {
            _c = c;
            Id = id;
        }

        public (bool, string) Eval(string message) => 
            (message[0] == _c, message.Length > 0 ? message.Substring(1):Empty);

        public int Id { get; }

        public override string ToString()
        {
            return $"{Id} -> 'c'=={_c} ";
        }

        public bool Equals(CharCheck? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _c == other._c;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((CharCheck) obj);
        }

        public override int GetHashCode()
        {
            return _c.GetHashCode();
        }

        public static bool operator ==(CharCheck? left, CharCheck? right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(CharCheck? left, CharCheck? right)
        {
            return !Equals(left, right);
        }
    } 

    public class Rule : IEquatable<Rule>, IRule
    {
        public (bool,string) Eval(string message)
        {
            Console.WriteLine($"{ToString()} {message}");
            (bool isMatch, string message) result;
            if (IsNullOrEmpty(message))
            {
                if ((this.Id == 31 || this.Id == 42))
                {
                    result = (true, message);
                }
                else
                {
                    result = (false, message);
                }
            }
            else
            {
                var andResult = EvalMany(_and, message);
                var orResult = EvalMany(_or, message);
                result = andResult.isMatch 
                    ? andResult : orResult; 
            }
            Console.WriteLine($"{ToString()} {message} {result.isMatch}");
            return result;
        }

        private (bool isMatch,string remaidner) EvalMany(IEnumerable<IRule> rules, string message)
        {
            if (!rules.Any()) return (false, message);
            foreach (var rule in rules)
            {
                var result = rule.Eval(message);
                if (!result.isMatch) return (false, message);
                message = result.remainder;
            }
            return (true, message);
        }

        public int Id { get; }
        private readonly IEnumerable<IRule> _and;
        private readonly IEnumerable<IRule> _or;

        public Rule(int id, IEnumerable<IRule> and, IEnumerable<IRule> or)
        {
            Id = id;
            _and = and;
            _or = or;
        }

        public override string ToString()
        {
            return $"{Id} -> and({Join(",",_and.Select(r=>r.Id))}) or({Join(",",_or.Select(r=>r.Id))})";
        }

        public bool Equals(Rule? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id;
            
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Rule) obj);
        }

        public override int GetHashCode()
        {
            return Id;
        }

        public static bool operator ==(Rule? left, Rule? right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Rule? left, Rule? right)
        {
            return !Equals(left, right);
        }
    }
}