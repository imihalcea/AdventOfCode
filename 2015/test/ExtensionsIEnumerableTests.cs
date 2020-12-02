using System.Collections.Generic;
using System.Linq;
using _2015.tools;
using NFluent;
using NUnit.Framework;

namespace _2015.test
{
    [TestFixture]
    public class ExtensionsIEnumerableTests
    {
        [Test]
        public void group_by_test()
        {
            var ex1 = "a".GroupConsecutiveRepeats(c => c).ToArray();
            CheckGroup(ex1, 0, 'a', 1);
            
            var ex2 = "aa".GroupConsecutiveRepeats(c => c).ToArray();
            CheckGroup(ex2, 0, 'a', 2);
            
            var ex3 = "aaba".GroupConsecutiveRepeats(c => c).ToArray();
            CheckGroup(ex3, 0, 'a', 2);
            CheckGroup(ex3, 1, 'b', 1);

            var ex4 = "aaba".GroupConsecutiveRepeats(c => c).ToArray();
            CheckGroup(ex4, 0, 'a', 2);
            CheckGroup(ex4, 1, 'b', 1);
            CheckGroup(ex4, 2, 'a', 1);
            
            var ex5 = "aabaccbxx".GroupConsecutiveRepeats(c => c).ToArray();
            CheckGroup(ex5, 0, 'a', 2);
            CheckGroup(ex5, 1, 'b', 1);
            CheckGroup(ex5, 2, 'a', 1);
            CheckGroup(ex5, 3, 'c', 2);
            CheckGroup(ex5, 4, 'b', 1);
            CheckGroup(ex5, 5, 'x', 2);
        }

        private static void CheckGroup((char key, IEnumerable<char> values)[] groups, int checkedIndex, char expected, int expectedCount)
        {
            Check.That(groups[checkedIndex].key).IsEqualTo(expected);
            Check.That(groups[checkedIndex].values).CountIs(expectedCount);
        }
    }
}