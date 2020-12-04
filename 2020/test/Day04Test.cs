using NFluent;
using NUnit.Framework;
using static _2020.Day04;
using static _2020.test.TestExtensions;
namespace _2020.test
{
    [TestFixture]
    public class Day04Test
    {
        private const string INPUT_FILE_PATH = "test/day04.txt";

        [Test]
        public void answer_part1()
        {
            Answer(Part1,INPUT_FILE_PATH);
        }
        
        [Test]
        public void answer_part2()
        {
            Answer(Part2,INPUT_FILE_PATH);
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
    }
}