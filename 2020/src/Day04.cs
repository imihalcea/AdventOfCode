using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace _2020
{
    public class Day04
    {
        public static int Part1(string filePath) =>
            DataSet(filePath).Count(p => p.IsComplete());
        
        public static int Part2(string filePath) =>
            DataSet(filePath).Count(p => p.IsValid());

        public static IEnumerable<PassportInfo> DataSet(string path)
        {
            var lines = File.ReadAllLines(path).Append(string.Empty).ToArray();
            var passportData = string.Empty;
            var i = 0;
            do
            {
                var line = lines[i].Trim();
                if (line == string.Empty)
                {
                    yield return new PassportInfo(passportData);
                    passportData = string.Empty;
                }
                else
                {
                    passportData = string.Concat(passportData, " ", line);
                }
                i++;
            } while (i < lines.Length);
        }
    }

    public class PassportInfo
    {
        private readonly IDictionary<string, string> _fields;
        private readonly string[] _mandatory;

        public PassportInfo(string passportData)
        {
            _fields = passportData.Trim()
                        .Split(' ')
                        .Select(info => info.Split(':'))
                        .ToDictionary(info => info[0], info => info[1]);
            
            _mandatory = new[] {"byr","iyr","eyr","hgt","hcl","ecl","pid"};
        }

        public bool IsComplete() => 
            _fields.Keys.Intersect(_mandatory).Count() == _mandatory.Length;

        private bool AllFieldsValid() =>
            _fields.All(kv => Validators(kv.Key)(kv.Value));

        public bool IsValid() => IsComplete() && AllFieldsValid();

        private static Func<string, bool> Validators(string field) =>
            field switch
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
            data => new Regex(pattern).IsMatch(data);

        private static Func<string, bool> AnyValidator(string[] set) => 
            set.Contains;
    }
}