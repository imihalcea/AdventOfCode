using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using static System.Environment;
using static System.String;

namespace _2020
{
    public class Day04
    {
        public static int Part1(string filePath) =>
            DataSet(filePath).Count(p => p.IsComplete());
        
        public static int Part2(string filePath) =>
            DataSet(filePath).Count(p => p.IsValid());

        private static IEnumerable<PassportInfo> DataSet(string path) =>
            File.ReadAllText(path).Split(NewLine+NewLine)
                .Select(block => Join(' ',block.Split(NewLine)))
                .Select(passportData => new PassportInfo(passportData));
    }

    public class PassportInfo
    {
        public bool IsComplete() => 
            _fields.Keys.Intersect(Mandatory).Count() == Mandatory.Length;

        private bool AllValid() =>
            _fields.All(kv => FieldValidators.ContainsKey(kv.Key) && FieldValidators[kv.Key](kv.Value));

        public bool IsValid() => IsComplete() && AllValid();

        private static Func<string, bool> NumberValidator(int min, int max) =>
            data => int.TryParse(data, out var n) && n >= min && n <= max;

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
            data => new Regex(pattern).IsMatch(data);

        public PassportInfo(string passportData)
        {
            _fields = passportData.Trim()
                .Split(' ')
                .Select(info => info.Split(':'))
                .ToDictionary(info => info[0], info => info[1]);
        }
        
        private readonly IDictionary<string, string> _fields;

        private readonly string[] Mandatory = FieldValidators.Keys.Take(7).ToArray();

        private static readonly Dictionary<string,Func<string,bool>> FieldValidators = new Dictionary<string, Func<string, bool>>()
        {
            {"byr", NumberValidator(1920, 2002)},
            {"iyr", NumberValidator(2010, 2020)},
            {"eyr", NumberValidator(2020, 2030)},
            {"hgt", HeightValidator},
            {"hcl", PatternValidator(@"^#([a-fA-F0-9]{6}|[a-fA-F0-9]{3})$")},
            {"ecl", PatternValidator("^(amb|blu|brn|gry|grn|hzl|oth)$")},
            {"pid", PatternValidator("^[0-9]{9}$")},
            {"cid", _=>true}
        };
    }
}