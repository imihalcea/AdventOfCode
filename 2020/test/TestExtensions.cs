using System;
using System.Diagnostics;

namespace _2020.test
{
    public static class TestExtensions
    {
        public static void Answer<Y>(Func<Y> f)
        {
            var sw = Stopwatch.StartNew();
            var y = f();
            sw.Stop();
            Console.WriteLine($"Response: {y} in {sw.ElapsedMilliseconds} ms");
        }
        public static Y Answer<X, Y>(Func<X, Y> f, X x)
        {
            var sw = Stopwatch.StartNew();
            var y = f(x);
            sw.Stop();
            Console.WriteLine($"Response: {y} in {sw.ElapsedMilliseconds} ms");
            return y;
        }
        public static Y Answer<A,X, Y>(Func<A,X, Y> f, A a,  X x)
        {
            var sw = Stopwatch.StartNew();
            var y = f(a,x);
            sw.Stop();
            Console.WriteLine($"Response: {y} in {sw.ElapsedMilliseconds} ms");
            return y;
        }
    }
}