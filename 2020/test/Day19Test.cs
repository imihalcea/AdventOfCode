using System.Collections.Generic;
using System.Linq;
using NFluent;
using NUnit.Framework;
using static _2020.Day19;
using static _2020.test.TestExtensions;

namespace _2020.test
{
    [TestFixture]
    public class Day19Test
    {
        private (IEnumerable<IRule> rules, string[] messages) DatasetExamples2 = Dataset(INPUT_FILE_PATH_EX_2);

        private const string INPUT_FILE_PATH_EX = "test/day19ex.txt";
        private const string INPUT_FILE_PATH = "test/day19.txt";
        private const string INPUT_FILE_PATH_EX_2 = "test/day19ex2.txt";
        private const string INPUT_FILE_PATH_2 = "test/day19part2.txt";

        [Test]
        public void answer_part2()
        {
            var n =  Answer(CountValidMessages,  Dataset(INPUT_FILE_PATH_2));
            Check.That(n).IsEqualTo(429);
        }
        
        [TestCase("bbabbbbaabaabba",true)]
        [TestCase("babbbbaabbbbbabbbbbbaabaaabaaa",true)]
        [TestCase("aaabbbbbbaaaabaababaabababbabaaabbababababaaa",true)]
        [TestCase("bbbbbbbaaaabbbbaaabbabaaa",true)]
        [TestCase("bbbababbbbaaaaaaaabbababaaababaabab",true)]
        [TestCase("ababaaaaaabaaab",true)]
        [TestCase("ababaaaaabbbaba",true)]
        [TestCase("baabbaaaabbaaaababbaababb",true)]
        [TestCase("abbbbabbbbaaaababbbbbbaaaababb",true)]
        [TestCase("aaaaabbaabaaaaababaa",true)]
        [TestCase("aaaabbaabbaaaaaaabbbabbbaaabbaabaaa",true)]
        [TestCase("aabbbbbaabbbaaaaaabbbbbababaaaaabbaaabba",true)]
        [TestCase("abbbbbabbbaaaababbaabbbbabababbbabbbbbbabaaaa",false)]
        [TestCase("babaaabbbaaabaababbaabababaaab",false)]
        public void examples_part2(string message, bool expected)
        {
           var (rules, _) =  DatasetExamples2;
            var result = CountValidMessages((rules,new []{message}))==1;
            Check.That(result).IsEqualTo(expected);
        }

        [Test]
        public void answer_par1()
        {
            var n = Answer(CountValidMessages,  Dataset(INPUT_FILE_PATH));
            Check.That(n).IsEqualTo(210);
        }

        [TestCase("ababbb",true)]
        [TestCase("abbbab",true)]
        [TestCase("bababa",false)]
        [TestCase("aaabbb",false)]
        [TestCase("aaaabbb",false)]
        public void examples_part1(string message, bool expected)
        {
            var results = TestExamples(Dataset(INPUT_FILE_PATH_EX));
            Check.That(results).Contains((expected, message));
        }
        
        public (bool, string)[] TestExamples((IEnumerable<IRule> rules, string[] messages) dataset)
        {
            var (rules, messages) = dataset;
            var r0 = rules.First();
            return messages.Select(m =>
            {
                var (isMatch, remainder) = r0.Eval(m);
                return (isMatch && remainder.Length == 0, m);
            }).ToArray();
        }
    }
}