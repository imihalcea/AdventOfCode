using NFluent;
using NUnit.Framework;
using static _2020.Day18;
using static _2020.test.TestExtensions;

namespace _2020.test
{
    [TestFixture]
    public class Day18Test
    {
        private const string INPUT_FILE_PATH = "test/day18.txt";

        [Test]
        public void answer_part1()
        {
            var n = Answer(Part1,  Dataset(INPUT_FILE_PATH));
            Check.That(n).IsEqualTo(67800526776934);
        }
        
        [Test]
        public void answer_part2()
        {
            var n = Answer(Part2,  Dataset(INPUT_FILE_PATH));
            Check.That(n).IsEqualTo(340789638435483);
        }
        
        [Test]
        public void simple_expression()
        {
            //1 + 2 * 3 + 4 * 5 + 6
            var op1 = new Add(new Mul(new Add(new Mul(new Add(new Ident(1), new Ident(2)),new Ident(3)),new Ident(4)),new Ident(5)),new Ident(6));
            Check.That(op1.Eval()).IsEqualTo(71);
            //1 + (2 * 3) + (4 * (5 + 6))
            var op2 = new Add(new Add(new Ident(1), new Mul(new Ident(2), new Ident(3))), new Mul(new Ident(4), new Add(new Ident(5), new Ident(6))));
            Check.That(op2.Eval()).IsEqualTo(51);

        }

        [TestCase("1 + 1",2)]
        [TestCase("1 + 1 * 3",6)]
        [TestCase("1 + (2 * 3) + (4 * (5 + 6))",51)]
        [TestCase("2 * 3 + (4 * 5)",26)]
        [TestCase("5 + (8 * 3 + 9 + 3 * 4 * 3)",437)]
        [TestCase("5 * 9 * (7 * 3 * 3 + 9 * 3 + (8 + 6 * 4))",12240)]
        [TestCase("((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2",13632)]
        public void parse_and_eval_part1(string expr, int expected)
        {
            var op = Parse(expr);
            Check.That(op.Eval()).IsEqualTo(expected);
        }

        [TestCase("1 + 1",2)]
        [TestCase("1 + 1 * 3",6)]
        [TestCase("1 + (2 * 3) + (4 * (5 + 6))",51)]
        [TestCase("2 * 3 + (4 * 5)",46)]
        [TestCase("5 + (8 * 3 + 9 + 3 * 4 * 3)",1445)]
        [TestCase("5 * 9 * (7 * 3 * 3 + 9 * 3 + (8 + 6 * 4))",669060)]
        [TestCase("((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2",23340)]
        public void parse_and_eval_part2(string expr, int expected)
        {
            var op = Parse2(expr);
            Check.That(op.Eval()).IsEqualTo(expected);
        }
        
    }
}