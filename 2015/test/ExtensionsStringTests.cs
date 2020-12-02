using System.Linq;
using _2015.tools;
using NFluent;
using NUnit.Framework;

namespace _2015.test
{
    [TestFixture]
    public class ExtensionsStringTests
    {
        [TestCase("A1", new int[]{1})]
        [TestCase("{\"a\":\"1\", \"b\":-1,2\"}", new int[]{1,-1,2})]
        public void extract_integers_tests(string input, int[] expected)
        {
            Check.That(input.ExtractIntegers()).ContainsExactly(expected);
        }

        [TestCase("abcd",new int[]{0,1,2,3},new []{"a","c","d"})]
        public void except_substrings(string input, int[] exceptedInterval, string[] expected)
        {
            
            var except_substrings = exceptedInterval.TwoByTwo((i1,i2)=>(i1,i2)).ToArray();
            var result = input.SubstringsExcept(except_substrings);
            Check.That(result).ContainsExactly(expected);
        }
    }
}