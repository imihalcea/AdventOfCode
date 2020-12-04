using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace _2020
{
    public class Day04
    {
        public static int Part1(PassportInfo[] input) =>
            input.Count(p => p.IsComplete());
        
        public static int Part2(PassportInfo[] input) =>
            input.Count(p => p.IsValid());
    }

    public class PassportInfo
    {
        private readonly string _passportData;
        private ConcurrentDictionary<string, string> _fields;
        private string[] _mandatory;

        public PassportInfo(string passportData)
        {
            _passportData = passportData;
            _fields = new ConcurrentDictionary<string, string>();
            passportData
                .Trim()
                .Split(' ')
                .Select(info=>info.Split(':'))
                .ToList()
                .ForEach(info=> _fields.AddOrUpdate(info[0], _ => info[1], (_, __) => info[1]));
            _mandatory = new[] {"byr","iyr","eyr","hgt","hcl","ecl","pid"};//"cid"
        }

        public bool IsComplete() => 
            _fields.Keys.Intersect(_mandatory).Count() == _mandatory.Length;

        public bool AllFieldsValid() =>
            _fields.All(kv => Validators(kv.Key)(kv.Value));

        public bool IsValid() => IsComplete() && AllFieldsValid();

        private Func<string, bool> Validators(string field)
        {
            return field switch
            {
                "byr" => NumberValidator(1920, 2002),
                "iyr" => NumberValidator(2010, 2020),
                "eyr" => NumberValidator(2020, 2030),
                "hgt" => HeightValidator,
                "hcl" => PatternValidator(@"^#([a-fA-F0-9]{6}|[a-fA-F0-9]{3})$"),
                "ecl" => AnyValidator(new[] {"amb", "blu", "brn", "gry", "grn", "hzl", "oth"}),
                "pid" => PatternValidator("^[0-9]{9}$"),
                "cid" => _ => true,
                _ => __ => false
            };
        }

        private static Func<string, bool> NumberValidator(int min, int max) =>
            data => int.TryParse(data, out var byr) 
                    && byr >= min && byr <= max;

        private static bool HeightValidator(string data)
        {
            var uIn = data.Contains("in");
            var uCm = data.Contains("cm");

            var valAsStr = data.Replace("in", "").Replace("cm","");
            return (uIn || uCm)
                   && uIn
                ? NumberValidator(59, 76)(valAsStr)
                : NumberValidator(150, 193)(valAsStr);
        }
        private static Func<string, bool> PatternValidator(string pattern) =>
            data =>
            {
                var regEx = new Regex(pattern);
                return regEx.IsMatch(data);
            };

        private static Func<string, bool> AnyValidator(string[] set) => 
            data => set.Contains(data);
    }
}