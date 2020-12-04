using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NFluent;
using NUnit.Framework;
using static _2020.Day04;
using static _2020.test.TestExtensions;
namespace _2020.test
{
    [TestFixture]
    public class Day04Test
    {
        [Test]
        public void answer_part1()
        {
            var passportInfos = DataSet().ToArray();
            Answer(Part1,passportInfos);
        }
        
        [Test]
        public void answer_part2()
        {
            Answer(Part2,DataSet().ToArray());
        }

        [TestCase("ecl:gry pid:860033327 eyr:2020 hcl:#fffffd byr:1937 iyr:2017 cid:147 hgt:183cm",true)]
        [TestCase("iyr:2013 ecl:amb cid:350 eyr:2023 pid:028048884 hcl:#cfa07d byr:1929",false)]
        [TestCase("hcl:#ae17e1 iyr:2013 eyr:2024 ecl:brn pid:760753108 byr:1931 hgt:179cm",true)]
        [TestCase("hcl:#cfa07d eyr:2025 pid:166559648 iyr:2011 ecl:brn hgt:59in",false)]
        public void puzzle_examples(string input, bool expected)
        {
            var passport = new PassportInfo(input);
            Check.That(passport.IsComplete()).IsEqualTo(expected);
        }

        public IEnumerable<PassportInfo> DataSet()
        {
            var lines = File.ReadAllLines("test/day04.txt");
            var passportData = string.Empty;
            foreach (var line in lines)
            {
                if (line.Trim() == string.Empty)
                {
                    yield return new PassportInfo(passportData);
                    passportData = string.Empty;
                }
                passportData = string.Concat(passportData, " ", line.Trim());
            }
        }
    }
}