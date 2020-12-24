using NFluent;
using NUnit.Framework;
using static  _2020.Day20;
using static _2020.test.TestExtensions;
namespace _2020.test
{
    public class Day20Test
    {
        
        private const string INPUT_FILE_PATH_EX = "test/day20ex.txt";
        private const string INPUT_FILE_PATH = "test/day20.txt";

        [Test]
        public void answer_part1()
        {
            var n = Answer(Part1, Dataset(INPUT_FILE_PATH));
            Check.That(n).IsEqualTo(22878471088273);
        }
        
        [Test]
        public void examples_part1()
        {
            var n = Answer(Part1,Dataset(INPUT_FILE_PATH_EX));
            Check.That(n).IsEqualTo(20899048083289);
        }
        
        [TestCase(new[]{1,0}, new[]{0,1}, false)]
        [TestCase(new[]{1,0,0,1,1,0,1,1,0,1}, new[]{1,0,0,1,0,1,1,1,0,1}, false)]
        [TestCase(new[]{1,0}, new[]{1,0}, true)]
        [TestCase(new[]{1,0,0,1,0,1,1,1,0,1}, new[]{1,0,0,1,0,1,1,1,0,1}, true)]
        public void HashTest(int[] p1, int[] p2, bool expected)
        {
            var h1 =  Tools.ComputeHash(p1);
            var h2 = Tools.ComputeHash(p2);

            Check.That(h1 == h2).IsEqualTo(expected);
        }

        [Test]
        public void transpose_tests()
        {
            var pixels = new int[3][];
            pixels[0] = new[] {1, 2};
            pixels[1] = new[] {3, 4};
            pixels[2] = new[] {5, 6};
            var result = Tools.Transpose(pixels);
            Check.That(result[0]).ContainsExactly(1, 3, 5);
            Check.That(result[1]).ContainsExactly(2, 4, 6);
        }
        
        [Test]
        public void flip_tests()
        {
            var pixels = new int[3][];
            pixels[0] = new[] {1, 2, 3, 4 };
            pixels[1] = new[] {5, 6, 7, 8};
            pixels[2] = new[] {9, 10, 11, 12};
            var result = Tools.Flip(pixels);
            Check.That(result[0]).ContainsExactly(4, 3, 2, 1);
            Check.That(result[1]).ContainsExactly(8, 7, 6, 5);
            Check.That(result[2]).ContainsExactly(12, 11, 10, 9);
        }

        [Test]
        public void rotate_test()
        {
            var pixels = new int[3][];
            pixels[0] = new[] {1, 2, 3, 4 };
            pixels[1] = new[] {5, 6, 7, 8};
            pixels[2] = new[] {9, 10, 11, 12};
            var result = Tools.Rotate(pixels);
            Check.That(result[0]).ContainsExactly(9, 10, 11, 12);
            Check.That(result[1]).ContainsExactly(5, 6, 7, 8);
            Check.That(result[2]).ContainsExactly(1, 2, 3, 4);
        }
        
        [Test]
        public void flip_rotate_test()
        {
            var pixels = new int[3][];
            pixels[0] = new[] {1, 2, 3, 4 };
            pixels[1] = new[] {5, 6, 7, 8};
            pixels[2] = new[] {9, 10, 11, 12};
            var result = Tools.FlipAndRotate(pixels);
            Check.That(result[0]).ContainsExactly(12, 11, 10, 9);
            Check.That(result[1]).ContainsExactly(8,7,6,5);
            Check.That(result[2]).ContainsExactly(4,3,2,1);
        }
        
    }
}